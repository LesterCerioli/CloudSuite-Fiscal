using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudSuite.Infrastructure.Mapping.EFCore
{
    public class DASEFCoremapping : IEntityTypeConfiguration<DAS>
    {
        public void Configure(EntityTypeBuilder<DAS> builder)
        {
            throw new NotImplementedException();
        }
    }
}