using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Services
{
	public interface IDataService
	{
		string GetConnectionString();
		void OpenConn();
		void BeginConn();
		void StopConn();
		void CloseConn();
		void BeginTransaction();
		IDbTransaction IfHaveTransactionReturn();
		void CommitTransaction();
		void RollBackTransaction();
		void AddParameter(string variable, object data);
		void AddParameter(string variable, object data, DbType dbType);
		void AddParameter(string variable, object data, DbType dbType, ParameterDirection parameterDirection);
		void DataCommit(string query);
		void DataCommit(string query, CommandType cmdType);
		IDataReader GetDataReader(string query);
		IDataReader GetDataReader(string query, CommandType cmdType);
		object ReturnScalerData(string query);
		object ReturnScalerData(string query, CommandType cmdType);
		bool ReturnBoolData(string query);
		T ReturnData<T>(string query);
		IEnumerable<T> ReturnDataAsIE<T>(string query);
		IEnumerable<T> ReturnDataAsIE<T>(string query, CommandType cmdtType);
	}
}

