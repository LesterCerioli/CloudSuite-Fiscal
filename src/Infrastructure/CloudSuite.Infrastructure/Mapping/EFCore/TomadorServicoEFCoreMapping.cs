using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudSuite.Infrastructure.Mapping.EFCore
{
    public class TomadorServicoEFCoreMapping : IEntityTypeConfiguration<TomadorServico>
    {
        public void Configure(EntityTypeBuilder<TomadorServico> builder)
        {
            throw new NotImplementedException();
        }
    }
}