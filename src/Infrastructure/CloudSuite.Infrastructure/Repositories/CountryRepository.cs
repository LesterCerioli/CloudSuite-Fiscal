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
	public class CountryRepository : ICountryRepository
	{
		protected readonly FiscalDbContext Db;

		protected readonly DbSet<Country> DbSet;
		
		public CountryRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.Countries;
		}
		public async Task Add(Country country)
		{
			await Task.Run(() =>
			{
				Db.Add(country);
				Db.SaveChangesAsync();
			});

		}

		public async Task<Country> GetbyCountryName(string countryName)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.CountryName == countryName);
		}

		public async Task<IEnumerable<Country>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(Country country)
		{
			DbSet.Remove(country);
		}

		public void Update(Country country)
		{
			DbSet.Update(country);
		}

		public void Dispose()
		{
			Db.Dispose();
		}
	}
}
