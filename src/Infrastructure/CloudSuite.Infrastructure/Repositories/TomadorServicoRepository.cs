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
	public class TomadorServicoRepository : ITomadorServicoRepository
	{
		protected readonly FiscalDbContext Db;
		protected readonly DbSet<TomadorServico> DbSet;


		public TomadorServicoRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.TomadorServicos;
		}

		public async Task Add(TomadorServico tomadorServico)
		{
			await Task.Run(() =>
			{
				Db.Add(tomadorServico);
				Db.SaveChangesAsync();
			});
		}

		public async Task<TomadorServico> GetByCnpj(Cnpj cnpj)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.Cnpj == cnpj);
		}

		public async Task<TomadorServico> GetBySocialReason(string socialReason)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.SocialReason == socialReason);
		}

		public async Task<IEnumerable<TomadorServico>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(TomadorServico tomadorServico)
		{
			Db.Remove(tomadorServico);
		}

		public void Update(TomadorServico tomadorServico)
		{
			Db.Update(tomadorServico);
		}

		public void Dispose()
		{
			Db.Dispose();
		}
			
	}
}
