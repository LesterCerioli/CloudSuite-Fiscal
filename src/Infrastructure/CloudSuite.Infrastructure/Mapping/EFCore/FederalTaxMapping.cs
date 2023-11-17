using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudSuite.Infrastructure.Mapping.EFCore
{
    public class FederalTaxMapping : IEntityTypeConfiguration<FederalTax>
    {
        public void Configure(EntityTypeBuilder<FederalTax> builder)
        {
            throw new NotImplementedException();
        }
    }
}