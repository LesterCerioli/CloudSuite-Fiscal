using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudSuite.Infrastructure.Mapping.EFCore
{
    public class IdeCancelamentoEFCoreMapping : IEntityTypeConfiguration<IdeCancelamento>
    {
        public void Configure(EntityTypeBuilder<IdeCancelamento> builder)
        {
            throw new NotImplementedException();
        }
    }
}