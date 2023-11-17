using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudSuite.Infrastructure.Mapping.EFCore
{
    public class DarfEFCoreMapping : IEntityTypeConfiguration<Darf>
    {
        public void Configure(EntityTypeBuilder<Darf> builder)
        {
            throw new NotImplementedException();
        }
    }
}