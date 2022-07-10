using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Last_Assignment.Data.Migrations
{
    public partial class CustomerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerActivities_Customers_Id",
                table: "CustomerActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customers222");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers222",
                table: "Customers222",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerActivities_Customers222_Id",
                table: "CustomerActivities",
                column: "Id",
                principalTable: "Customers222",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerActivities_Customers222_Id",
                table: "CustomerActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers222",
                table: "Customers222");

            migrationBuilder.RenameTable(
                name: "Customers222",
                newName: "Customers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

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
