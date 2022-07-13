using Last_Assignment.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Last_Assignment.Data.Seeds
{
    public class SeedUsers : IEntityTypeConfiguration<UserApp>
    {
        public void Configure(EntityTypeBuilder<UserApp> builder)
        {
            UserApp user = new UserApp()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                City = "İstanbul"

            };

            PasswordHasher<UserApp> passwordHasher = new PasswordHasher<UserApp>();
            passwordHasher.HashPassword(user, "Password12*");

            builder.HasData(user);

            UserApp userEditor = new UserApp()
            {
                Id = "1662a5ca-531f-4f49-90d5-6a708c8d5c8c",
                UserName = "Editor",
                Email = "editor@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                City = "İstanbul"
            };

            PasswordHasher<UserApp> passwordHasherEditor = new PasswordHasher<UserApp>();
            passwordHasher.HashPassword(user, "Password12*");

            builder.HasData(userEditor);
        }
    }
}
