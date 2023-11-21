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
	public class PrestadorRepository : IPrestadorRepository
	{
		protected readonly FiscalDbContext Db;

		protected readonly DbSet<Prestador> DbSet;
		
		public PrestadorRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.Prestadores;
		}	
		public async Task Add(Prestador prestador)
		{
			await Task.Run(() =>
			{
				Db.Add(prestador);
				Db.SaveChangesAsync();
			});
		}

		public async Task<Prestador> GetByCnpj(Cnpj cnpj)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.Cnpj == cnpj);
		}

		public async Task<Prestador> GetByDocTomadorEstrangeiro(string docTomadorEstrangeiro)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.DocTomadorEstrangeiro == docTomadorEstrangeiro);
		}

		public async Task<Prestador> GetByInscricaoEstadual(string inscricaoEstadual)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.InscricaoEstadual == inscricaoEstadual);
		}

		public async Task<Prestador> GetByInscricaoMunicipal(string inscricaoMunicipal)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.InscricaoMunicipal == inscricaoMunicipal);
		}

		public async Task<Prestador> GetByNomeFantasia(string nomeFantasia)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.NomeFantasia == nomeFantasia);
		}

		public async Task<IEnumerable<Prestador>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(Prestador prestador)
		{
			Db.Remove(prestador);
		}

		public void Update(Prestador prestador)
		{
			Db.Update(prestador);
		}

		public void Dispose()
		{
			Db.Dispose();
		}
	}
}
