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
	public class CityRepository : ICityRepository
	{
		protected readonly FiscalDbContext Db;
		protected readonly DbSet<City> DbSet;
		
		public CityRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.Cities;
		}
		public async Task Add(City city)
		{
			await Task.Run(() =>
			{
				DbSet.Add(city);
				Db.SaveChangesAsync();
			});
		}

		public async Task<City> GetByCityName(string cityName)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.CityName == cityName);
		}

		public async Task<IEnumerable<City>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(City city)
		{
			DbSet.Remove(city);
		}

		public void Update(City city)
		{
			DbSet.Update(city);
		}

		public void Dispose()
		{
			Db.Dispose();
		}
	}
}
