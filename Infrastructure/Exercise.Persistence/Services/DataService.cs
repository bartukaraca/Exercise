﻿using Dapper;
using Exercise.Application.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Persistence.Services
{
		public class DataService : IDataService
		{

			protected IDbConnection db;
			protected IDbTransaction transaction;
			protected DynamicParameters param;
			protected bool isBeginedConnection = false;

			public DataService()
			{
				//db = new SqlConnection(GetConnectionString());
				db = new NpgsqlConnection(GetConnectionString());
				param = new DynamicParameters();
			}

			public string GetConnectionString()
			{
				//var configuration = new ConfigurationBuilder()
				//    .SetBasePath(Directory.GetCurrentDirectory())
				//    .AddJsonFile("appsettings.json")
				//    .Build();
				//var connectionString = configuration.GetConnectionString(connectionStringName);

				return "User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=ETicaretAPIDb;";

			}

			#region Function
			public void OpenConn()
			{
				db.Close();
				if (db.State == ConnectionState.Closed)
					db.Open();
			}

			public void BeginConn()
			{
				if (db.State == ConnectionState.Closed)
				{
					db.Open();
					isBeginedConnection = true;
				}
			}

			public void StopConn()
			{
				if (db.State == ConnectionState.Open)
				{
					db.Close();
					isBeginedConnection = false;
				}
			}

			public void CloseConn()
			{
				if (db.State == ConnectionState.Open)
					db.Close();
			}

			public void BeginTransaction()
			{
				if (!isBeginedConnection)
					OpenConn();

				transaction = db.BeginTransaction();
			}

			public IDbTransaction IfHaveTransactionReturn()
			{
				if (transaction != null)
					return transaction;
				else
					return null;
			}

			public void CommitTransaction()
			{
				transaction.Commit();
				transaction = null;

				if (!isBeginedConnection)
					CloseConn();
			}

			public void RollBackTransaction()
			{
				if (transaction != null)
				{
					transaction.Rollback();
					transaction = null;
				}
			}
			#endregion

			#region DynamicObject
			public void AddParameter(string variable, object data)
			{
				AddParameter(variable, data, DbType.String);
			}

			public void AddParameter(string variable, object data, DbType dbType)
			{
				AddParameter(variable, data, dbType, ParameterDirection.Input);
			}

			public void AddParameter(string variable, object data, DbType dbType, ParameterDirection parameterDirection)
			{
				param.Add(variable, data, dbType, parameterDirection);
			}

			public void DataCommit(string query)
			{
				DataCommit(query, CommandType.Text);
			}

			public void DataCommit(string query, CommandType cmdType)
			{
				OpenConn();

				if (param.ParameterNames.Any())
					DataCommitWithParam(query, cmdType);
				else
					DataCommiNottWithParam(query, cmdType);

				if (IfHaveTransactionReturn() == null && isBeginedConnection == false)
					CloseConn();

				param = new DynamicParameters();
			}

			private void DataCommiNottWithParam(string query, CommandType cmdType)
			{
				db.Execute(query, commandType: cmdType, transaction: IfHaveTransactionReturn());
			}

			private void DataCommitWithParam(string query, CommandType cmdType)
			{
				db.Execute(query, param, commandType: cmdType, transaction: IfHaveTransactionReturn());
			}

			public IDataReader GetDataReader(string query)
			{
				return GetDataReader(query, CommandType.Text);
			}

			public IDataReader GetDataReader(string query, CommandType cmdType)
			{
				OpenConn();
				IDataReader dr;

				if (param.ParameterNames.Any())
					dr = GetDataReaderWithParam(query, cmdType);
				else
					dr = GetDataReaderNotWithParam(query, cmdType);

				param = new DynamicParameters();
				return dr;
			}

			private IDataReader GetDataReaderNotWithParam(string query, CommandType cmdType)
			{
				return db.ExecuteReader(query, commandType: cmdType);
			}

			private IDataReader GetDataReaderWithParam(string query, CommandType cmdType)
			{
				return db.ExecuteReader(query, param, commandType: cmdType);
			}

			public object ReturnScalerData(string query)
			{
				return ReturnScalerData(query, CommandType.Text);
			}

			public object ReturnScalerData(string query, CommandType cmdType)
			{
				object obj;
				OpenConn();

				if (param.ParameterNames.Any())
					obj = ReturnScalerDataWithParam(query, cmdType);
				else
					obj = ReturnScalerDataNotWithParam(query, cmdType);

				if (IfHaveTransactionReturn() == null && isBeginedConnection == false)
					CloseConn();

				param = new DynamicParameters();
				return obj;
			}

			private object ReturnScalerDataNotWithParam(string query, CommandType cmdType)
			{
				object _value = db.ExecuteScalar(query, commandType: cmdType, transaction: IfHaveTransactionReturn());
				return _value;
			}

			private object ReturnScalerDataWithParam(string query, CommandType cmdType)
			{
				object _value = db.ExecuteScalar(query, param, commandType: cmdType, transaction: IfHaveTransactionReturn());
				return _value;
			}

			public bool ReturnBoolData(string query)
			{
				return db.ExecuteScalar<bool>(query);
			}
			#endregion

			#region DataFetch

			public T ReturnData<T>(string query)
			{
				return ReturnData<T>(query, CommandType.Text);
			}

			public T ReturnData<T>(string query, CommandType cmdtType)
			{
				OpenConn();

				T ls = db.QueryFirstOrDefault<T>(query, param: param, transaction: IfHaveTransactionReturn(), commandType: cmdtType);

				param = new DynamicParameters();
				if (IfHaveTransactionReturn() == null && isBeginedConnection == false)

					CloseConn();
				return ls;
			}


			public IEnumerable<T> ReturnDataAsIE<T>(string query)
			{
				return ReturnDataAsIE<T>(query, CommandType.Text);
			}

			public IEnumerable<T> ReturnDataAsIE<T>(string query, CommandType cmdtType)
			{
				OpenConn();

				IEnumerable<T> ls = db.Query<T>(query, param: param, transaction: IfHaveTransactionReturn(), commandType: cmdtType);

				param = new DynamicParameters();
				if (IfHaveTransactionReturn() == null && isBeginedConnection == false)

					CloseConn();
				return ls;
			}


			#endregion DataFetch
		}
	}

