using Last_Assignment.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Last_Assignment.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(); // default olara 1 er 1 er artacak veya .UseIdentityColumn(1,2)
            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Surname).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasMany(cust => cust.CustomerActivities)
                .WithOne(custAct => custAct.Customer)
                .HasForeignKey(custact => custact.Id)
                .IsRequired();



        }
    }
}
