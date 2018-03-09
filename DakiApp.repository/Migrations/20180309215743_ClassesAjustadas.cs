using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DakiApp.repository.Migrations
{
    public partial class ClassesAjustadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alternativas_Respostas_RespostasDomainid",
                table: "Alternativas");

            migrationBuilder.DropIndex(
                name: "IX_Alternativas_RespostasDomainid",
                table: "Alternativas");

            migrationBuilder.DropColumn(
                name: "RespostasDomainid",
                table: "Alternativas");

            migrationBuilder.AddColumn<int>(
                name: "RespostaId",
                table: "Usuarios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Anúncios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RespostaId",
                table: "Usuarios",
                column: "RespostaId");

            migrationBuilder.CreateIndex(
                name: "IX_Anúncios_UsuarioId",
                table: "Anúncios",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anúncios_Usuarios_UsuarioId",
                table: "Anúncios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Respostas_RespostaId",
                table: "Usuarios",
                column: "RespostaId",
                principalTable: "Respostas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anúncios_Usuarios_UsuarioId",
                table: "Anúncios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Respostas_RespostaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_RespostaId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Anúncios_UsuarioId",
                table: "Anúncios");

            migrationBuilder.DropColumn(
                name: "RespostaId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Anúncios");

            migrationBuilder.AddColumn<int>(
                name: "RespostasDomainid",
                table: "Alternativas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alternativas_RespostasDomainid",
                table: "Alternativas",
                column: "RespostasDomainid");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternativas_Respostas_RespostasDomainid",
                table: "Alternativas",
                column: "RespostasDomainid",
                principalTable: "Respostas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
