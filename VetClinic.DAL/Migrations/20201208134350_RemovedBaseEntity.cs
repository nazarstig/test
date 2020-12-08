using Microsoft.EntityFrameworkCore.Migrations;

namespace VetClinic.DAL.Migrations
{
    public partial class RemovedBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentProcedures_AppointmentProceduresId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_AppointmentProcedures_AppointmentProceduresId",
                table: "Procedures");

            migrationBuilder.DropIndex(
                name: "IX_Procedures_AppointmentProceduresId",
                table: "Procedures");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppointmentProceduresId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentProceduresId",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "AppointmentProceduresId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Vasya",
                table: "Animals");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "AppointmentProcedures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcedureId",
                table: "AppointmentProcedures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentProcedures_AppointmentId",
                table: "AppointmentProcedures",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentProcedures_ProcedureId",
                table: "AppointmentProcedures",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentProcedures_Appointments_AppointmentId",
                table: "AppointmentProcedures",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentProcedures_Procedures_ProcedureId",
                table: "AppointmentProcedures",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentProcedures_Appointments_AppointmentId",
                table: "AppointmentProcedures");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentProcedures_Procedures_ProcedureId",
                table: "AppointmentProcedures");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentProcedures_AppointmentId",
                table: "AppointmentProcedures");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentProcedures_ProcedureId",
                table: "AppointmentProcedures");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "AppointmentProcedures");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "AppointmentProcedures");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentProceduresId",
                table: "Procedures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentProceduresId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Vasya",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_AppointmentProceduresId",
                table: "Procedures",
                column: "AppointmentProceduresId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentProceduresId",
                table: "Appointments",
                column: "AppointmentProceduresId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentProcedures_AppointmentProceduresId",
                table: "Appointments",
                column: "AppointmentProceduresId",
                principalTable: "AppointmentProcedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_AppointmentProcedures_AppointmentProceduresId",
                table: "Procedures",
                column: "AppointmentProceduresId",
                principalTable: "AppointmentProcedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
