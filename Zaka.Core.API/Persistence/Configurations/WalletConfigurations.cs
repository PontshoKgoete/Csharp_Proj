using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zaka.Core.API.Domain.Entities;

namespace Zaka.Core.API.Persistence.Configurations
{
    public class WalletConfigurations : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.Property(w => w.WalletNumber).HasMaxLength(20).IsRequired();
        }
    }
}