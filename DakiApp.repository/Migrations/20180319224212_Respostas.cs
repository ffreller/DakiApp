using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DakiApp.repository.Migrations
{
    public partial class Respostas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alternativas_Respostas_RespostaId",
                table: "Alternativas");

            migrationBuilder.DropIndex(
                name: "IX_Alternativas_RespostaId",
                table: "Alternativas");

            migrationBuilder.DropColumn(
                name: "RespostaId",
                table: "Alternativas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RespostaId",
                table: "Alternativas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Alternativas_RespostaId",
                table: "Alternativas",
                column: "RespostaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternativas_Respostas_RespostaId",
                table: "Alternativas",
                column: "RespostaId",
                principalTable: "Respostas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
