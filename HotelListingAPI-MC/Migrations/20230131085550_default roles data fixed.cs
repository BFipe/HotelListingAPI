using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListingAPI_MC.Migrations
{
    public partial class defaultrolesdatafixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2164cbdd-1724-4106-b165-595a318dea8e", "2a46ee1d-0f9a-41fc-af8d-ae6e244ddc1b", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93e32a81-051a-415b-b368-de08449d6e0f", "891c684d-9c4d-48dc-9c09-8113a1124844", "Manager", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e27315a0-2ae6-416e-a607-203fe18d705c", "dd1c4153-0c20-4c98-b4cd-8994c73a867c", "User", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2164cbdd-1724-4106-b165-595a318dea8e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93e32a81-051a-415b-b368-de08449d6e0f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e27315a0-2ae6-416e-a607-203fe18d705c");
        }
    }
}
