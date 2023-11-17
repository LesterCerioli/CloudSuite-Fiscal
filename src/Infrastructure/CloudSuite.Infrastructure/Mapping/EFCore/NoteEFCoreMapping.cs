using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudSuite.Infrastructure.Mapping.EFCore
{
    public class NoteEFCoreMapping : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            throw new NotImplementedException();
        }
    }
}