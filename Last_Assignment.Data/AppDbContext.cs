using Last_Assignment.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Last_Assignment.Data
{
    //identity üyelik tabloları

    public class AppDbContext : IdentityDbContext<UserApp, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  // startup ta doldurulacak. Bu options a ıdentitybase  miras alınan ctor a gönderiyoruz
        {

        }

        // Product---> Customer ve CustomerActivity  ve UserRefreshToken üyelik sistemiyle ilgili olmayan dbset ler
        //public DbSet<Product> Products { get; set; }


        // Kullanıcı ile ilgili tüm dbset ler IdentityDbContext... ile miras olarak geliyor


        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerActivity> CustomerActivities { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        // Tabloların colonlarının yapıları ne olacak ,required , null,stringlength max  olacak mı vs diye gibi ayarlar için bunu ekliyoruz ....
        // burayı şişirmemek için  Configurations dosyası  kısmına gidiyoruz,Entity lerle ilgili herşeyi buraya yazmak istemiyoruz
        //OnModelCreating metodunu şişirmemek için

        //Seperation of Concern prensibi ...

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);


            //builder.Entity<Customer>(entity =>
            //{
            //    entity.HasMany(cust => cust.CustomerActivities)
            //    .WithOne(custAct => custAct.Customer)
            //    .HasForeignKey(custact => custact.Id)
            //    .IsRequired();
            //});

            //builder.Entity<CustomerActivity>(entity =>
            //    {
            //        entity.HasOne(custact => custact.Customer)
            //        .WithMany(cust => cust.CustomerActivities)
            //        .HasForeignKey(custact => custact.Id)
            //        .IsRequired();
            //    });


            //builder.UseSerialColumns(); ?? !! PSQL videosu

            base.OnModelCreating(builder);
        }
    }
}
