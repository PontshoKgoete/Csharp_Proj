using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zaka.Core.API.Domain.Entities;

namespace Zaka.Core.API.Persistence.Configurations
{
    public class AccountConfigurations : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(a => a.CellphoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(a => a.FirstName).HasMaxLength(255).IsRequired();
            builder.Property(a => a.LastName).HasMaxLength(255).IsRequired();
            builder.Property(a => a.Username).HasMaxLength(255).IsRequired();
            builder.Property(a => a.SecretKey).HasMaxLength(255);
        }
    }
}