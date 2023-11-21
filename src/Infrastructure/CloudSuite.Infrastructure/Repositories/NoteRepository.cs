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
	public class NoteRepository : INoteRepository
	{
		protected readonly FiscalDbContext Db;

		protected readonly DbSet<Note> DbSet;
		
		public NoteRepository(FiscalDbContext context) 
		{
			Db = context;
			DbSet = context.Notes;
		}
		
		public async Task Add(Note note)
		{
			await Task.Run(() =>
			{
				Db.Add(note);
				Db.SaveChangesAsync();
			});
		}

		public async Task<Note> GetByCnpj(Cnpj cnpj)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.Cnpj == cnpj);
		}

		public async Task<Note> GetByEmissionDate(DateTime? date)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.EmissionDate == date);
		}

		public async Task<Note> GetByNoteNumber(string noteNumber)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.NoteNumber == noteNumber);
		}

		public async Task<Note> GetByValue(decimal value)
		{
			return await DbSet.FirstOrDefaultAsync(c => c.Value == value);
		}

		public async Task<IEnumerable<Note>> GetList()
		{
			return DbSet.ToList();
		}

		public void Remove(Note note)
		{
			Db.Remove(note);	
		}

		public void Update(Note note)
		{
			Db.Update(note);
		}

		public void Dispose()
		{
			Db.Dispose();
		}
	}
}
