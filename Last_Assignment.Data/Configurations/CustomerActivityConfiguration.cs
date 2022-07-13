using Last_Assignment.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Last_Assignment.Data.Configurations
{
    public class CustomerActivityConfiguration : IEntityTypeConfiguration<CustomerActivity>
    {
        public void Configure(EntityTypeBuilder<CustomerActivity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Service).HasMaxLength(200);
            builder.Property(x => x.Amount).HasColumnType("decimal(18,2)");

            builder.HasOne(custact => custact.Customer)
                .WithMany(cust => cust.CustomerActivities)
                .HasForeignKey(custact => custact.CustomerId)
                .IsRequired();
        }
    }
}
