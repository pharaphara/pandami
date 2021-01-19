using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandami.Migrations
{
    public partial class toutesLesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiels_TypeAides_TypeAideId",
                table: "Materiels");

            migrationBuilder.DropIndex(
                name: "IX_Materiels_TypeAideId",
                table: "Materiels");

            migrationBuilder.DropColumn(
                name: "TypeAideId",
                table: "Materiels");

            migrationBuilder.AddColumn<int>(
                name: "MaterielId",
                table: "TypeAides",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeLitige",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeLitige", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Litige",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClotureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeLitigeId = table.Column<int>(type: "int", nullable: true),
                    FeatId = table.Column<int>(type: "int", nullable: true),
                    MembreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Litige", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Litige_Feats_FeatId",
                        column: x => x.FeatId,
                        principalTable: "Feats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Litige_Membres_MembreId",
                        column: x => x.MembreId,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Litige_TypeLitige_TypeLitigeId",
                        column: x => x.TypeLitigeId,
                        principalTable: "TypeLitige",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypeAides_MaterielId",
                table: "TypeAides",
                column: "MaterielId");

            migrationBuilder.CreateIndex(
                name: "IX_Litige_FeatId",
                table: "Litige",
                column: "FeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Litige_MembreId",
                table: "Litige",
                column: "MembreId");

            migrationBuilder.CreateIndex(
                name: "IX_Litige_TypeLitigeId",
                table: "Litige",
                column: "TypeLitigeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeAides_Materiels_MaterielId",
                table: "TypeAides",
                column: "MaterielId",
                principalTable: "Materiels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeAides_Materiels_MaterielId",
                table: "TypeAides");

            migrationBuilder.DropTable(
                name: "Litige");

            migrationBuilder.DropTable(
                name: "TypeLitige");

            migrationBuilder.DropIndex(
                name: "IX_TypeAides_MaterielId",
                table: "TypeAides");

            migrationBuilder.DropColumn(
                name: "MaterielId",
                table: "TypeAides");

            migrationBuilder.AddColumn<int>(
                name: "TypeAideId",
                table: "Materiels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materiels_TypeAideId",
                table: "Materiels",
                column: "TypeAideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materiels_TypeAides_TypeAideId",
                table: "Materiels",
                column: "TypeAideId",
                principalTable: "TypeAides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
