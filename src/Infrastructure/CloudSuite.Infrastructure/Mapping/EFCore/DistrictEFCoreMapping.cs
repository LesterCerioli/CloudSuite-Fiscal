using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudSuite.Infrastructure.Mapping.EFCore
{
    public class DistrictEFCoreMapping : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            throw new NotImplementedException();
        }
    }
}