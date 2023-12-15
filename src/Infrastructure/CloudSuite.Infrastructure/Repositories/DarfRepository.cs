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
	public class DarfRepository : IDarfRepository
	{
		protected readonly FiscalDbContext Db;

		protected readonly DbSet<Darf> DbSet;
		
		public DarfRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.Darfs;
		}
		public async Task Add(Darf darf)
		{
			await Task.Run(() =>
			{
				Db.Add(darf);
				Db.SaveChangesAsync();
			});
		}

		public async Task<Darf> GetByDocumentNumber(string documentNumber)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.DocumentNumber == documentNumber);
		}

		public async Task<Darf> GetByDueDate(DateTime? duedate)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.DueDate == duedate);
		}

		public async Task<Darf> GetByReferenceMonth(string referenceMonth)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.ReferenceMonth == referenceMonth);
		}

		public async Task<Darf> GetByValidationDate(DateTime? validationDate)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.ValidationDate == validationDate);
		}

		public async Task<IEnumerable<Darf>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(Darf darf)
		{
			Db.Remove(darf);
		}

		public void Update(Darf darf)
		{
			Db.Update(darf);
		}

		public void Dispose()
		{
			Db.Dispose();
		}
	}
}
