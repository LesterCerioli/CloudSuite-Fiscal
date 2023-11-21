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
	public class DeclaracaoIRRepository : IDeclaracaoIRRepository
	{
		protected readonly FiscalDbContext Db;
		protected readonly DbSet<DeclaracaoIR> DbSet;
		
		public DeclaracaoIRRepository(FiscalDbContext context)
		{
			Db = context;
			DbSet = context.DeclaracaoIRs;

		}
		public async Task Add(DeclaracaoIR declaracaoIR)
		{
			await Task.Run(() =>
			{
				Db.Add(declaracaoIR);
				Db.SaveChangesAsync();
			});
		}

		public async Task<DeclaracaoIR> GetByAlimony(decimal alimony)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.Alimony == alimony);
		}

		public async Task<DeclaracaoIR> GetByCnpj(Cnpj cnpj)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.Cnpj == cnpj);
		}

		public async Task<DeclaracaoIR> GetByCpf(Cpf cpf)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.Cpf == cpf);
		}

		public async Task<DeclaracaoIR> GetByDeclaracaoNumero(string declaracaoNumero)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.DeclaracaoNumero == declaracaoNumero);
		}

		public async Task<DeclaracaoIR> GetByPaidValueToBusiness(decimal paidValueToBusiness)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.PaidValueToBusiness == paidValueToBusiness);
		}

		public async Task<DeclaracaoIR> GetByProfitsDividends(decimal profitsDividends)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.ProfitsDividends == profitsDividends);
		}

		public async Task<DeclaracaoIR> GetByTotalIncome(decimal totalIncome)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.TotalIncome == totalIncome);
		}

		public void Remove(DeclaracaoIR declaracaoIR)
		{
			Db.Remove(declaracaoIR);
		}

		public void Update(DeclaracaoIR declaracaoIR)
		{
			Db.Update(declaracaoIR);
		}

		public void Dispose()
		{
			Db.Dispose();	
		}
	}
}
