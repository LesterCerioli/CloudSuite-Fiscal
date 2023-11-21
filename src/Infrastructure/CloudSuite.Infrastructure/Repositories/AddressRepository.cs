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
	public class AddressRepository : IAddressRepository
	{
		protected readonly FiscalDbContext Db;
		protected readonly DbSet<Address> DbSet;
		
		public AddressRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.Addresses;

		}
		
		public async Task Add(Address address)
		{
			await Task.Run(() =>
			{
				DbSet.Add(address);
				Db.SaveChangesAsync();
			});

		}

		public async Task<Address> GetByAddressLine1(string addressLine1)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.AddressLine1 == addressLine1);
		}

		public async Task<IEnumerable<Address>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(Address address)
		{
			DbSet.Remove(address);
		}

		public void Update(Address address)
		{
			DbSet.Update(address);
		}

		public void Dispose()
		{
			Db.Dispose();
		}
	}
}
