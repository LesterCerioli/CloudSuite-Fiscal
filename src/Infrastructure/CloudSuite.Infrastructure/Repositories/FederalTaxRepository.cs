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
	public class FederalTaxRepository : IFederalTaxRepository
	{
		protected readonly FiscalDbContext Db;
		protected readonly DbSet<FederalTax> DbSet;
		
		public FederalTaxRepository(FiscalDbContext context) 
		{
			Db = context;
			DbSet = context.FederalTaxes;
		}
		
		public async Task Add(FederalTax federalTax)
		{
			await Task.Run(() =>
			{
				Db.Add(federalTax);
				Db.SaveChangesAsync();
			});
		}

		public async Task<FederalTax> GetByVCOFINS(decimal vCOFINS)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.VCOFINS == vCOFINS);
		}

		public async Task<FederalTax> GetByVCSLL(decimal vCSLL)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.VCSLL == vCSLL);
		}

		public async Task<FederalTax> GetByVINSS(decimal vINSS)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.VINSS == vINSS);
		}

		public async Task<FederalTax> GetByVIR(decimal vIR)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.VIR == vIR);
		}

		public async Task<FederalTax> GetByVPIS(decimal vPIS)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.VPIS == vPIS);
		}

		public async Task<IEnumerable<FederalTax>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(FederalTax federalTax)
		{
			DbSet.Remove(federalTax);
		}

		public void Update(FederalTax federalTax)
		{
			DbSet.Update(federalTax);
		}

		public void Dispose()
		{
			Db.Dispose();
		}
	}
}
