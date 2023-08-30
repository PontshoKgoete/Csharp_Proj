using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zaka.Core.API.Domain.Entities;

namespace Zaka.Core.API.Persistence.Configurations
{
    public class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(t => t.VoucherNumber).HasMaxLength(20).IsRequired();
            builder.Property(t => t.Amount).IsRequired();
            builder.Property(t => t.FromWallet).HasMaxLength(20);
            builder.Property(t => t.FromPhone).HasMaxLength(20);
            builder.Property(t => t.ToWallet).HasMaxLength(20);
            builder.Property(t => t.ToPhone).HasMaxLength(20);
            builder.Property(t => t.TransactionStatus).HasMaxLength(100).IsRequired();
        }
    }
}