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
	public class DASRepository : IDASRepository
	{
		protected readonly FiscalDbContext Db;
		protected readonly DbSet<DAS> DbSet;
		
		public DASRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.DASes;

		}
		public async Task Add(DAS das)
		{
			await Task.Run(() =>
			{
				Db.Add(das);
				Db.SaveChangesAsync();
			});
		}

		public async Task<DAS> GetByDocumentNumber(string documentNumber)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.DocumentNumber == documentNumber);
		}

		public async Task<DAS> GetByDueDate(DateTime? dueDate)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.DueDate == dueDate);
		}

		public async Task<DAS> GetByReferenceMonth(string referenceMonth)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.ReferenceMonth == referenceMonth);
		}

		public async Task<DAS> GetByReferenceYear(string referenceYear)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.ReferenceYear == referenceYear);
		}

		public async Task<IEnumerable<DAS>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(DAS das)
		{
			Db.Remove(das);
		}

		public void Update(DAS das)
		{
			Db.Update(das);
		}

		public void Dispose()
		{
			Db.Dispose();
		}
    }
}
