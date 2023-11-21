using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudSuite.Infrastructure.Mapping.EFCore
{
    public class CancelOrderEFCoremapping : IEntityTypeConfiguration<CancelOrder>
    {
        public void Configure(EntityTypeBuilder<CancelOrder> builder)
        {
            throw new NotImplementedException();
        }
    }
}