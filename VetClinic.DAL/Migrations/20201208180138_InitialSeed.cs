using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VetClinic.DAL.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Appointments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AnimalTypes",
                columns: new[] { "Id", "AnimalTypeName" },
                values: new object[,]
                {
                    { 1, "Pes dvorovuy" },
                    { 2, "Kit Domashniy" },
                    { 3, "Slon" },
                    { 4, "Zhuraph" },
                    { 5, "Zolota Rubka" },
                    { 6, "Homyak" },
                    { 7, "Monster" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "PositionName", "Salary" },
                values: new object[,]
                {
                    { 1, "Вirector", 100000m },
                    { 2, "Сleaner", 10000m },
                    { 3, "Veterinarian", 500m },
                    { 4, "Stylist", 1000m }
                });

            migrationBuilder.InsertData(
                table: "Procedures",
                columns: new[] { "Id", "Description", "IsSelectable", "Price", "ProcedureName" },
                values: new object[,]
                {
                    { 4, "Bad operation", false, 240m, "Сastration" },
                    { 3, "Pet inspection", true, 50m, "Examination of animal" },
                    { 5, "Doctor Consultation about care and maintenance of the animal", false, 100m, "Consultation" },
                    { 1, "Best for your pet", true, 1000m, "SPA procedures" },
                    { 2, "Paw fracture", false, 2000m, "Operation" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "ServiceName" },
                values: new object[,]
                {
                    { 1, "Inspection" },
                    { 2, "Makeup" },
                    { 3, "Urgently" },
                    { 4, "Castration" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { 4, "Completed" },
                    { 3, "Processing" },
                    { 1, "Approved" },
                    { 2, "Disapproved" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 6, 0, "790361bc-062c-4df1-a609-5b3a8eca0f6f", "WernudubIvan@gmail.com", false, false, null, null, null, null, "0982123654", false, null, false, "Wernudub Ivan" },
                    { 1, 0, "cc30471f-eaa9-4cf5-a142-cf658f94b730", "KolesoAnastasiya@gmail.com", false, false, null, null, null, null, "0984112333", false, null, false, "Koleso Anastasiya" },
                    { 2, 0, "fc4f1c94-b1bf-4d46-8838-0a0ad66520ca", "NazarenkoOleh@gmail.com", false, false, null, null, null, null, "0954453374", false, null, false, "Nazarenko Oleh" },
                    { 3, 0, "a5b49172-bc19-4b73-a214-1eefb989077a", "NoorkovaShuba@gmail.com", false, false, null, null, null, null, "0934453214", false, null, false, "Noorkova Shuba" },
                    { 4, 0, "50481f48-16e1-4310-a110-8f3d8d66f14d", "VozniyAndriy@gmail.com", false, false, null, null, null, null, "0931412622", false, null, false, "Vozniy Andriy" },
                    { 5, 0, "26b03458-47b1-46fb-954d-534acb88df2b", "KosovichMaruna@gmail.com", false, false, null, null, null, null, "0681236324", false, null, false, "Kosovich Maruna" },
                    { 7, 0, "40a73d4c-bd54-494b-88a2-e02c183d5bbc", "MukolenkoNadiya@gmail.com", false, false, null, null, null, null, "0982131254", false, null, false, "Mukolenko Nadiya" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 4 },
                    { 2, 5 },
                    { 3, 6 },
                    { 4, 7 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Biography", "Education", "Experience", "Photo", "PositionId", "UserId" },
                values: new object[,]
                {
                    { 1, null, "Gas and Oil", "2", null, 1, 1 },
                    { 2, null, "IfMedical", "7", null, 3, 2 },
                    { 3, null, "SelfEducation", "3", null, 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Age", "AnimalTypeId", "ClientId", "Name", "Photo" },
                values: new object[,]
                {
                    { 2, 3, 2, 1, "Ruzhuk", null },
                    { 6, 1, 6, 1, "Krug", null },
                    { 8, 2, 2, 1, "Robin", null },
                    { 3, 5, 1, 2, "Sirko", null },
                    { 1, 342, 7, 3, "Pushok", null },
                    { 7, 1, 5, 3, "Dzvin", null },
                    { 4, 12, 3, 4, "Biznes", null },
                    { 5, 4, 4, 4, "Hmarochos", null }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AnimalId", "AppointmentDate", "Complaints", "DoctorId", "ServiceId", "StatusId", "TreatmentDescription" },
                values: new object[,]
                {
                    { 2, 2, new DateTime(2020, 4, 23, 9, 30, 0, 0, DateTimeKind.Unspecified), "Bad wool", 3, 2, 3, "Use animal shampoo" },
                    { 6, 6, new DateTime(2019, 2, 2, 9, 30, 0, 0, DateTimeKind.Unspecified), "Vaccination", 2, 1, 3, "" },
                    { 8, 8, new DateTime(2020, 3, 30, 10, 30, 0, 0, DateTimeKind.Unspecified), "Paw fracture", 2, 3, 1, "daily bandage change" },
                    { 3, 3, new DateTime(2020, 1, 12, 10, 30, 0, 0, DateTimeKind.Unspecified), "Required castration", 2, 4, 4, "wear a collar for 12 days" },
                    { 1, 1, new DateTime(2020, 5, 1, 8, 30, 0, 0, DateTimeKind.Unspecified), "Temperature", 1, 1, 2, "Drink more wather" },
                    { 7, 7, new DateTime(2020, 12, 12, 12, 30, 0, 0, DateTimeKind.Unspecified), "Sore spine", 2, 1, 2, "daily walk" },
                    { 4, 4, new DateTime(2019, 5, 14, 11, 30, 0, 0, DateTimeKind.Unspecified), "Spa procedure", 3, 2, 1, "Spa every weak" },
                    { 5, 5, new DateTime(2019, 11, 15, 12, 30, 0, 0, DateTimeKind.Unspecified), "Need consultation", 3, 1, 4, "Pay more attention" }
                });

            migrationBuilder.InsertData(
                table: "AppointmentProcedures",
                columns: new[] { "Id", "AppointmentId", "ProcedureId" },
                values: new object[,]
                {
                    { 2, 2, 2 },
                    { 6, 6, 3 },
                    { 9, 8, 4 },
                    { 3, 3, 3 },
                    { 1, 1, 1 },
                    { 10, 1, 3 },
                    { 7, 7, 5 },
                    { 8, 7, 4 },
                    { 4, 4, 4 },
                    { 5, 5, 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppointmentProcedures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppointmentProcedures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppointmentProcedures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppointmentProcedures",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AppointmentProcedures",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AppointmentProcedures",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AppointmentProcedures",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AppointmentProcedures",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AppointmentProcedures",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AppointmentProcedures",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Procedures",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AnimalTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
