using CloudSuite.Infrastructure.Context;
using CloudSuite.Modules.Domain.Contracts;
using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Infrastructure.Repositories
{
	public class StateRepository : IStateRepository
	{
		protected readonly FiscalDbContext Db;
		protected readonly DbSet<State> DbSet;
		
		public StateRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.States;
		}
		public async Task Add(State state)
		{
			await Task.Run(() =>
			{
				Db.Add(state);
				Db.SaveChangesAsync();
			});

		}

		public async Task<State> GetByStateName(string stateName)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.StateName == stateName);
		}

		public async Task<State> GetByUF(string uf)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.UF == uf);
		}

		public async Task<IEnumerable<State>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(State state)
		{
			DbSet.Remove(state);
		}

		public void Update(State state)
		{
			DbSet.Update(state);
		}

		public void Dispose()
		{
			Db.Dispose();
		}
	}
}
