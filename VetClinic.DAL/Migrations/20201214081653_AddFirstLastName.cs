using Microsoft.EntityFrameworkCore.Migrations;

namespace VetClinic.DAL.Migrations
{
    public partial class AddFirstLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "107323be-54f5-4672-9f1e-e235a1eaa0fb", "422ac964-feec-48ef-a743-10231b7af208" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4f386150-96f6-4006-945c-4467944e5937", "8961be47-0b57-4917-aa8f-d74e6c298fb4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9d7ad1eb-9b79-4c65-82a5-942076602926", "479a16fb-ac64-4f8a-903e-42d3de782ae4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1df64a3c-d00f-4feb-92b8-9353c889e69f", "126b0e6f-a7b7-42e3-a139-326c7ba5897b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "98ac1f92-1cd8-485a-a7f7-918d34702e53", "2723e207-e1ce-4fd9-8b4a-5fee3de3639d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bd69e55e-90d9-41bb-81ef-47e3afdd78d3", "0895f2f1-d245-4a9b-9b43-917da1c7e045" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3c00af88-29cb-4396-823d-1472018fb800", "9b068ef6-fea7-4def-a0cf-94d1459e821a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "199d8f6c-f299-4ce5-a80a-27d2e74a1457", "feb42154-f76e-43e9-831e-9ef53ab4a35b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "99bffdba-2d91-4eae-84d6-1a7e9b13d3d7", "3d511176-f5f4-428a-a874-a2833d2ba4d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "07c750ca-53f9-431b-ba81-d33dcf38d19b", "adc6034b-0c0a-46a7-944f-3158ad6ead8b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "732d617f-9ec7-4e33-b631-df9c4fb7f3c4", "40f85397-45b6-432d-a711-93df2c1cce5e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5ab93c35-b2be-4bb9-b58c-fdf70621dbfc", "9a84f6b5-5d50-4736-86ce-79f9aadbdce2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e54ff86e-7866-4993-87fb-d1a7285ee8e4", "d780d6ad-0156-4529-a3ca-2a47f70b72b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "457202b1-bc96-465d-9c27-763f304a7aa6", "2db11faa-0364-4600-b2ca-eb72f1672300" });
        }
    }
}
