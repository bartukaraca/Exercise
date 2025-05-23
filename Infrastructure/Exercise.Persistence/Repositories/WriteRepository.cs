﻿using Exercise.Application.Repositories;
using Exercise.Domain.Entities.Common;
using Exercise.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Persistence.Repositories
{
	public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntitiy
	{
		readonly private ExerciseDbContext _context;

		public WriteRepository(ExerciseDbContext context)
		{
			_context = context;
		}

		public DbSet<T> Table => _context.Set<T>();

		public async Task<bool> AddAsync(T model)
		{
			EntityEntry<T> entityEntry = await Table.AddAsync(model);
			return entityEntry.State == EntityState.Added;
		}

		public async Task<bool> AddRangeAsync(List<T> datas)
		{
			await Table.AddRangeAsync(datas);
			return true;
		}

		public bool Remove(T model)
		{
			EntityEntry<T> entityEntry = Table.Remove(model);
			return entityEntry.State == EntityState.Deleted;
		}
		public bool RemoveRange(List<T> datas)
		{
			Table.RemoveRange(datas);
			return true;
		}
		public async Task<bool> RemoveAsync(string id)
		{
			T model = await Table.FirstOrDefaultAsync(data => data.Id == int.Parse(id));
			return Remove(model);
		}
		public bool UpdateAsync(T model)
		{
			EntityEntry entityEntry = Table.Update(model);
			return entityEntry.State == EntityState.Modified;
		}
		public async Task<int> SaveAsync()
		=> await _context.SaveChangesAsync();

	}
}
