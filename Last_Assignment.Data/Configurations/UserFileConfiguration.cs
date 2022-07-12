using Last_Assignment.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Last_Assignment.Data.Configurations
{
    public class UserFileConfiguration : IEntityTypeConfiguration<UserFile>
    {
        public void Configure(EntityTypeBuilder<UserFile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.ToTable("UserFile");

            builder.Ignore(c=>c.CreatedDate); // NotMapped
            

            //builder.HasMany(cust => cust.CustomerActivities)
            //    .WithOne(custAct => custAct.Customer)
            //    .HasForeignKey(custact => custact.CustomerId)
            //    .IsRequired();



        }
    }
}
