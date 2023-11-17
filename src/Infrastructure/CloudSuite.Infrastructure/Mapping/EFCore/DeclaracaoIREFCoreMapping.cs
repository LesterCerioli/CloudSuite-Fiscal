using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudSuite.Infrastructure.Mapping.EFCore
{
    public class DeclaracaoIREFCoreMapping : IEntityTypeConfiguration<DeclaracaoIR>
    {
        public void Configure(EntityTypeBuilder<DeclaracaoIR> builder)
        {
            throw new NotImplementedException();
        }
    }
}