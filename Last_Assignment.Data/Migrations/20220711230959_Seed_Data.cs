using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Last_Assignment.Data.Migrations
{
    public partial class Seed_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c7b013f0-5201-4317-abd8-c211f91b7330", "2", "Editor", "Editor Resource" },
                    { "fab4fac1-c546-41de-aebc-a14da6895711", "1", "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1662a5ca-531f-4f49-90d5-6a708c8d5c8c", 0, "İstanbul", "1b068919-b2b7-4125-90ac-b4b4a3d8a7ff", "editor@gmail.com", false, false, null, null, null, null, "1234567890", false, "122a46a0-ce85-4320-a156-4726e0ed9303", false, "Editor" },
                    { "b74ddd14-6340-4840-95c2-db12554843e5", 0, "İstanbul", "c3fa599c-d391-4cb6-8315-58dcd56fe624", "admin@gmail.com", false, false, null, null, null, null, "1234567890", false, "2efac6bb-c96a-4c04-a10a-cada8d581b0c", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c7b013f0-5201-4317-abd8-c211f91b7330", "1662a5ca-531f-4f49-90d5-6a708c8d5c8c" },
                    { "fab4fac1-c546-41de-aebc-a14da6895711", "b74ddd14-6340-4840-95c2-db12554843e5" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c7b013f0-5201-4317-abd8-c211f91b7330", "1662a5ca-531f-4f49-90d5-6a708c8d5c8c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fab4fac1-c546-41de-aebc-a14da6895711", "b74ddd14-6340-4840-95c2-db12554843e5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1662a5ca-531f-4f49-90d5-6a708c8d5c8c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5");
        }
    }
}
