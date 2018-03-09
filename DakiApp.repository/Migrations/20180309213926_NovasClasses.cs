using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DakiApp.repository.Migrations
{
    public partial class NovasClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RespostasDomainid",
                table: "Alternativas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Anúncios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Contato = table.Column<string>(maxLength: 100, nullable: false),
                    Data = table.Column<string>(maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 500, nullable: false),
                    Endereco = table.Column<string>(maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(maxLength: 60, nullable: false),
                    Titulo = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anúncios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Permissões",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissões", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Respostas",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlternativaId = table.Column<int>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    PerguntaId = table.Column<int>(nullable: false),
                    Resposta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respostas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Respostas_Questionarios_AlternativaId",
                        column: x => x.AlternativaId,
                        principalTable: "Questionarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Respostas_Perguntas_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Perguntas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPermissoes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    PermissaoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPermissoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_UsuarioPermissoes_Permissões_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "Permissões",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPermissoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alternativas_RespostasDomainid",
                table: "Alternativas",
                column: "RespostasDomainid");

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_AlternativaId",
                table: "Respostas",
                column: "AlternativaId");

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_PerguntaId",
                table: "Respostas",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPermissoes_PermissaoId",
                table: "UsuarioPermissoes",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPermissoes_UsuarioId",
                table: "UsuarioPermissoes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternativas_Respostas_RespostasDomainid",
                table: "Alternativas",
                column: "RespostasDomainid",
                principalTable: "Respostas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alternativas_Respostas_RespostasDomainid",
                table: "Alternativas");

            migrationBuilder.DropTable(
                name: "Anúncios");

            migrationBuilder.DropTable(
                name: "Respostas");

            migrationBuilder.DropTable(
                name: "UsuarioPermissoes");

            migrationBuilder.DropTable(
                name: "Permissões");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Alternativas_RespostasDomainid",
                table: "Alternativas");

            migrationBuilder.DropColumn(
                name: "RespostasDomainid",
                table: "Alternativas");
        }
    }
}
