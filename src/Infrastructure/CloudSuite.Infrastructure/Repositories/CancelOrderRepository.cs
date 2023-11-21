using CloudSuite.Infrastructure.Context;
using CloudSuite.Modules.Common.ValueObjects;
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
	public class CancelOrderRepository : ICancelOrderRepository
	{
		protected readonly FiscalDbContext Db;
		protected readonly DbSet<CancelOrder> DbSet;
			 
		
		public CancelOrderRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.CancelOrders;
		}
		
		public async Task Add(CancelOrder cancelOrder)
		{
			await Task.Run(() =>
			{
				Db.Add(cancelOrder);
				Db.SaveChangesAsync();
			});
		}

		public async Task<CancelOrder> GetbyCnpj(Cnpj cnpj)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.Cnpj == cnpj); 
		}

		public async Task<CancelOrder> GetByRequestDate(DateTimeOffset requestDate)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.RequestDate == requestDate);
		}

		public async Task<IEnumerable<CancelOrder>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(CancelOrder cancelOrder)
		{
			Db.Remove(cancelOrder);
		}

		public void Update(CancelOrder cancelOrder)
		{
			Db.Update(cancelOrder);
		}

		public void Dispose()
		{
			Db.Dispose();
		}
	}
}
