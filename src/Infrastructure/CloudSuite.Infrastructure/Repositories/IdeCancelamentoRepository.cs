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
	public class IdeCancelamentoRepository : IIdeCancelamentoRepository
	{
		protected readonly FiscalDbContext Db;
		protected readonly DbSet<IdeCancelamento> DbSet;
		
		public IdeCancelamentoRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.IdeCancelamentos;
		}
		public async Task Add(IdeCancelamento ideCancelamento)
		{
			await Task.Run(async () =>
			{
				Db.Add(ideCancelamento);
				Db.SaveChangesAsync();
			});
		}

		
		public async Task<IdeCancelamento> GetByCancelReason(string cancelReason)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.CancelReason == cancelReason);
		}

		public async Task<IdeCancelamento> GetByTimeDate(DateTimeOffset timeDate)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.TimeDate == timeDate);
		}

		public async Task<IEnumerable<IdeCancelamento>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(IdeCancelamento ideCancelamento)
		{
			Db.Remove(ideCancelamento);
		}

		public void Update(IdeCancelamento ideCancelamento)
		{
			Db.Update(ideCancelamento);	
		}

		public void Dispose()
		{
			Db.Dispose();
		}
	}
}
