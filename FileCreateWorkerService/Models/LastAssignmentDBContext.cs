using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FileCreateWorkerService.Models
{
    /*DBFirst için
    // Scaffold-DbContext “Server=localhost;Database=LastAssignmentDB;Port=5432;User Id=postgres;Password=1234” Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models 
    */
    public partial class LastAssignmentDBContext : DbContext
    {
        public LastAssignmentDBContext()
        {
        }

        public LastAssignmentDBContext(DbContextOptions<LastAssignmentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerActivity> CustomerActivities { get; set; } = null!;
        public virtual DbSet<UserFile> UserFiles { get; set; } = null!;
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; } = null!;

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("'-infinity'::timestamp without time zone");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("'-infinity'::timestamp without time zone");
            });

            modelBuilder.Entity<CustomerActivity>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "IX_CustomerActivities_CustomerId");

                entity.Property(e => e.Amount).HasPrecision(18, 2);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("'-infinity'::timestamp without time zone");

                entity.Property(e => e.Service).HasMaxLength(200);

                entity.Property(e => e.ServiceDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("'-infinity'::timestamp without time zone");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerActivities)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<UserFile>(entity =>
            {
                entity.ToTable("UserFile");
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Code).HasMaxLength(200);

                entity.Property(e => e.Expiration).HasColumnType("timestamp without time zone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
