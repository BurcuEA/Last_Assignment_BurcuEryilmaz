using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Last_Assignment.Data.Migrations
{
    public partial class CustomerId_ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerActivities_Customers_Id",
                table: "CustomerActivities");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CustomerActivities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CustomerActivities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerActivities_CustomerId",
                table: "CustomerActivities",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerActivities_Customers_CustomerId",
                table: "CustomerActivities",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerActivities_Customers_CustomerId",
                table: "CustomerActivities");

            migrationBuilder.DropIndex(
                name: "IX_CustomerActivities_CustomerId",
                table: "CustomerActivities");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerActivities");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CustomerActivities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerActivities_Customers_Id",
                table: "CustomerActivities",
                column: "Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
