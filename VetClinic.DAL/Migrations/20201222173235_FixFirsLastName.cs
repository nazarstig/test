using Microsoft.EntityFrameworkCore.Migrations;

namespace VetClinic.DAL.Migrations
{
    public partial class FixFirsLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TreatmentDescription",
                table: "Appointments",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Appointments",
                nullable: false,
                defaultValue: 3,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Complaints",
                table: "Appointments",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "542636ae-953e-4e04-a7de-7533f25af173",
                column: "ConcurrencyStamp",
                value: "18c6532e-c616-43a7-afd5-26148df56fd9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9149c77e-5c15-416a-9bed-e361330feb92",
                column: "ConcurrencyStamp",
                value: "b2a1118b-f047-4043-8653-bb972883d51c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ced370e3-1401-4190-9960-ab5bf41f162e",
                column: "ConcurrencyStamp",
                value: "fcaff323-37cc-45af-b1f5-1b8ac40fe8ea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e65dd8e1-34f8-4e9d-b535-3b03dde2500e",
                column: "ConcurrencyStamp",
                value: "f5a2e403-6b71-4693-b59b-56039dc68970");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "68d7ae39-6ceb-47da-b9de-cef947806a20", "813294b1-6e92-4d91-ac30-430fb9700ed2", "Anastasiya", "Koleso" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "3f33bedf-5ae9-42ff-81c7-4841d5693913", "dcfa2570-6a74-4f69-82cd-a99f64d40268", "Oleh", "Nazarenko" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "a5099ec1-398f-4804-a582-5d4e28fde87c", "3b861ddf-baaa-4639-93c6-f39fa01629e2", "Shuba", "Noorkova" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "d3c3ae7e-15df-44b6-b6f4-d3b556efeb27", "5f71babf-6105-4bee-b71b-cdfd0cff41bd", "Andriy", "Vozniy" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "629b93b5-9a68-4766-8c20-f3d44c1576c2", "bea29d0f-3673-480d-910f-79fd6f17002d", "Maruna", "Kosovich" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "7199b91e-da8c-4134-8f3a-cf0fc2b5e2a5", "16638d3d-eccf-44e9-9945-f99aa427e5d5", "Ivan", "Wernudub" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "b8196e39-928e-4559-9f87-2a44f7167d38", "3ec18b0d-af19-4ed8-bea4-7c3299bacaec", "Nadiya", "Mukolenko" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TreatmentDescription",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Complaints",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "542636ae-953e-4e04-a7de-7533f25af173",
                column: "ConcurrencyStamp",
                value: "0fdde684-8fff-43a3-8f29-fa3c417f2e99");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9149c77e-5c15-416a-9bed-e361330feb92",
                column: "ConcurrencyStamp",
                value: "b3218d73-dce5-4082-8e35-2b93c268ff90");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ced370e3-1401-4190-9960-ab5bf41f162e",
                column: "ConcurrencyStamp",
                value: "ebe8bf9c-86dd-4c92-9582-74b667a867a8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e65dd8e1-34f8-4e9d-b535-3b03dde2500e",
                column: "ConcurrencyStamp",
                value: "efc3aeed-6267-49bb-b28a-bc6215c8b97a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "0a0ff5c7-813b-4b82-9025-27c744feab01", "5f06386c-ff3b-47e8-8235-b1cbe88b8c91", null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "56184e50-dc1e-4fde-be73-03e765af11e3", "8a74c4a3-cf05-41d1-86f0-a9d857ccf526", null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "e9cd709f-1237-4cf5-83df-30259405c658", "944a4ec1-6204-4977-8b21-4f583e089a58", null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "7ef436e8-2ee7-4d43-9f78-9168b351490a", "b1fa9873-1201-4149-9783-0fe6a20578a0", null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "e31961c7-cb87-49e8-9a87-f99558ad7e11", "1f2e1b93-65ee-4bd2-886c-d2bf11666152", null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "e1225141-a969-448a-8ee2-9b17a2f3005a", "4ff494b0-bebc-43c0-a408-382ddf6c9b05", null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "FirstName", "LastName" },
                values: new object[] { "ddf5515d-0024-43e3-96f2-340387ba9a9a", "0bb7f018-73f2-4e94-beb8-4db71532fbdb", null, null });
        }
    }
}
