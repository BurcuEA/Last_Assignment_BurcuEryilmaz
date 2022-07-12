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
        public DbSet<UserFile> UserFiles { get; set; }

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
            //this.SeedUsers(builder);
            //this.SeedRoles(builder);
            //this.SeedUserRoles(builder);
        }


        //private void SeedUsers(ModelBuilder builder)
        //{
        //    UserApp user = new UserApp()
        //    {
        //        Id = "b74ddd14-6340-4840-95c2-db12554843e5",
        //        UserName = "Admin",
        //        Email = "admin@gmail.com",
        //        LockoutEnabled = false,
        //        PhoneNumber = "1234567890",
        //        City="İstanbul"
        //    };

        //    PasswordHasher<UserApp> passwordHasher = new PasswordHasher<UserApp>();
        //    passwordHasher.HashPassword(user, "Admin*123");

        //    builder.Entity<UserApp>().HasData(user);


        //    UserApp userEditor = new UserApp()
        //    {
        //        Id = "1662a5ca-531f-4f49-90d5-6a708c8d5c8c",
        //        UserName = "Editor",
        //        Email = "editor@gmail.com",
        //        LockoutEnabled = false,
        //        PhoneNumber = "1234567890",
        //        City = "İstanbul"
        //    };

        //    PasswordHasher<UserApp> passwordHasherEditor = new PasswordHasher<UserApp>();
        //    passwordHasherEditor.HashPassword(userEditor, "Editor*123");

        //    builder.Entity<UserApp>().HasData(userEditor);
        //}

        //private void SeedRoles(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityRole>().HasData(
        //        new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
        //        new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "Editor", ConcurrencyStamp = "2", NormalizedName = "Editor Resource" }
        //        );
        //}

        //private void SeedUserRoles(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityUserRole<string>>().HasData(
        //        new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" },
        //        new IdentityUserRole<string>() { RoleId = "c7b013f0-5201-4317-abd8-c211f91b7330", UserId = "1662a5ca-531f-4f49-90d5-6a708c8d5c8c" }
        //        );
        //}
    



}
}
