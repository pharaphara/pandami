using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandami.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RealisationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HeureDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HeureFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnCoursRealisation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SurPlace = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinFeatHelper = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClotureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SommePrevoir = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SommeAvancee = table.Column<float>(type: "real", nullable: false),
                    SommeRembourseeDate = table.Column<float>(type: "real", nullable: false),
                    AnnulationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Membres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Naissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inscription = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    Mdp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repondre",
                columns: table => new
                {
                    IdMembre = table.Column<int>(type: "int", nullable: false),
                    IdFeat = table.Column<int>(type: "int", nullable: false),
                    AcceptationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DesistementDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repondre", x => new { x.IdMembre, x.IdFeat });
                    table.ForeignKey(
                        name: "FK_Repondre_Feats_IdFeat",
                        column: x => x.IdFeat,
                        principalTable: "Feats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repondre_Membres_IdMembre",
                        column: x => x.IdMembre,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repondre_IdFeat",
                table: "Repondre",
                column: "IdFeat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repondre");

            migrationBuilder.DropTable(
                name: "Feats");

            migrationBuilder.DropTable(
                name: "Membres");
        }
    }
}
