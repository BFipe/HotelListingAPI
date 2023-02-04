using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListingAPI_MC.Migrations
{
    public partial class normalisedNamerolepropertyconfigured : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3fd5dfac-c6da-4dde-b759-979a670dd6a0", "634e843b-8c96-4d4d-aac1-74d631cf3978", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4be8c734-c6c6-44ff-a906-cd92fb93a088", "0a2621c1-a7fb-437b-aa78-8a1f358e44ae", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3bdfdcf-5927-4b79-b4f2-fff833a78633", "a051d4b3-556a-450d-90da-1581236602dd", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fd5dfac-c6da-4dde-b759-979a670dd6a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4be8c734-c6c6-44ff-a906-cd92fb93a088");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3bdfdcf-5927-4b79-b4f2-fff833a78633");

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
    }
}
