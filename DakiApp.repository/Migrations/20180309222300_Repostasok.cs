using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DakiApp.repository.Migrations
{
    public partial class Repostasok : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respostas_Questionarios_AlternativaId",
                table: "Respostas");

            migrationBuilder.AlterColumn<int>(
                name: "AlternativaId",
                table: "Respostas",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Respostas_Alternativas_AlternativaId",
                table: "Respostas",
                column: "AlternativaId",
                principalTable: "Alternativas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respostas_Alternativas_AlternativaId",
                table: "Respostas");

            migrationBuilder.AlterColumn<int>(
                name: "AlternativaId",
                table: "Respostas",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Respostas_Questionarios_AlternativaId",
                table: "Respostas",
                column: "AlternativaId",
                principalTable: "Questionarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
