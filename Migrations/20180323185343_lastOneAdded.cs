using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FinalProject.Migrations
{
    public partial class lastOneAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Psychologists_PsychologistID",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Results_ResultID",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Sessions_SessionID",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Users_UserID",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Appointment_AppointmentID",
                table: "Histories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_UserID",
                table: "Appointments",
                newName: "IX_Appointments_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_SessionID",
                table: "Appointments",
                newName: "IX_Appointments_SessionID");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_ResultID",
                table: "Appointments",
                newName: "IX_Appointments_ResultID");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_PsychologistID",
                table: "Appointments",
                newName: "IX_Appointments_PsychologistID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Psychologists_PsychologistID",
                table: "Appointments",
                column: "PsychologistID",
                principalTable: "Psychologists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Results_ResultID",
                table: "Appointments",
                column: "ResultID",
                principalTable: "Results",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Sessions_SessionID",
                table: "Appointments",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_UserID",
                table: "Appointments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Appointments_AppointmentID",
                table: "Histories",
                column: "AppointmentID",
                principalTable: "Appointments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Psychologists_PsychologistID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Results_ResultID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Sessions_SessionID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_UserID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Appointments_AppointmentID",
                table: "Histories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_UserID",
                table: "Appointment",
                newName: "IX_Appointment_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_SessionID",
                table: "Appointment",
                newName: "IX_Appointment_SessionID");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ResultID",
                table: "Appointment",
                newName: "IX_Appointment_ResultID");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PsychologistID",
                table: "Appointment",
                newName: "IX_Appointment_PsychologistID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Psychologists_PsychologistID",
                table: "Appointment",
                column: "PsychologistID",
                principalTable: "Psychologists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Results_ResultID",
                table: "Appointment",
                column: "ResultID",
                principalTable: "Results",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Sessions_SessionID",
                table: "Appointment",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Users_UserID",
                table: "Appointment",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Appointment_AppointmentID",
                table: "Histories",
                column: "AppointmentID",
                principalTable: "Appointment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
