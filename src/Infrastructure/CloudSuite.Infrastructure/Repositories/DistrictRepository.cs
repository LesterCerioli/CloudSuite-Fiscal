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
	public class DistrictRepository : IDistrictRepository
	{
		protected readonly FiscalDbContext Db;
		protected readonly DbSet<District> DbSet;
		
		public DistrictRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.Districts;
		}
		
		public async Task Add(District district)
		{
			Db.Add(district);
			Db.SaveChangesAsync();
		}

		public async Task<District> GetByName(string name)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.Name == name);
		}

		public async Task<IEnumerable<District>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(District district)
		{
			DbSet.Remove(district);
		}

		public void Update(District district)
		{
			DbSet.Update(district);	
		}

		public void Dispose()
		{
			Db.Dispose();
		}
	}
}
