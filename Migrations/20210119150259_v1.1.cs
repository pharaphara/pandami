using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandami.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repondre_Feats_IdFeat",
                table: "Repondre");

            migrationBuilder.DropForeignKey(
                name: "FK_Repondre_Membres_IdMembre",
                table: "Repondre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repondre",
                table: "Repondre");

            migrationBuilder.RenameTable(
                name: "Repondre",
                newName: "Repondres");

            migrationBuilder.RenameIndex(
                name: "IX_Repondre_IdFeat",
                table: "Repondres",
                newName: "IX_Repondres_IdFeat");

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Membres",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Membres",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Inscription",
                table: "Membres",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Membres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SexeId",
                table: "Membres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Feats",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repondres",
                table: "Repondres",
                columns: new[] { "IdMembre", "IdFeat" });

            migrationBuilder.CreateTable(
                name: "CategorieAide",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categorie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieAide", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JourDeLaSemaines",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomDuJour = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourDeLaSemaines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materiel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomMateriel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomSexe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suspensions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DebutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MembreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suspensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suspensions_Membres_MembreId",
                        column: x => x.MembreId,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Disponibilites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DebutHeure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinHeure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValiditeDebutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValiditeFinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JourId = table.Column<short>(type: "smallint", nullable: true),
                    MembreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disponibilites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disponibilites_JourDeLaSemaines_JourId",
                        column: x => x.JourId,
                        principalTable: "JourDeLaSemaines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disponibilites_Membres_MembreId",
                        column: x => x.MembreId,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypeAides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomAide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategorieAideId = table.Column<int>(type: "int", nullable: true),
                    MaterielId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeAides_CategorieAide_CategorieAideId",
                        column: x => x.CategorieAideId,
                        principalTable: "CategorieAide",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypeAides_Materiel_MaterielId",
                        column: x => x.MaterielId,
                        principalTable: "Materiel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Membres_SexeId",
                table: "Membres",
                column: "SexeId");

            migrationBuilder.CreateIndex(
                name: "IX_Feats_TypeId",
                table: "Feats",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Disponibilites_JourId",
                table: "Disponibilites",
                column: "JourId");

            migrationBuilder.CreateIndex(
                name: "IX_Disponibilites_MembreId",
                table: "Disponibilites",
                column: "MembreId");

            migrationBuilder.CreateIndex(
                name: "IX_Suspensions_MembreId",
                table: "Suspensions",
                column: "MembreId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeAides_CategorieAideId",
                table: "TypeAides",
                column: "CategorieAideId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeAides_MaterielId",
                table: "TypeAides",
                column: "MaterielId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feats_TypeAides_TypeId",
                table: "Feats",
                column: "TypeId",
                principalTable: "TypeAides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Membres_Sexes_SexeId",
                table: "Membres",
                column: "SexeId",
                principalTable: "Sexes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Repondres_Feats_IdFeat",
                table: "Repondres",
                column: "IdFeat",
                principalTable: "Feats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repondres_Membres_IdMembre",
                table: "Repondres",
                column: "IdMembre",
                principalTable: "Membres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feats_TypeAides_TypeId",
                table: "Feats");

            migrationBuilder.DropForeignKey(
                name: "FK_Membres_Sexes_SexeId",
                table: "Membres");

            migrationBuilder.DropForeignKey(
                name: "FK_Repondres_Feats_IdFeat",
                table: "Repondres");

            migrationBuilder.DropForeignKey(
                name: "FK_Repondres_Membres_IdMembre",
                table: "Repondres");

            migrationBuilder.DropTable(
                name: "Disponibilites");

            migrationBuilder.DropTable(
                name: "Sexes");

            migrationBuilder.DropTable(
                name: "Suspensions");

            migrationBuilder.DropTable(
                name: "TypeAides");

            migrationBuilder.DropTable(
                name: "JourDeLaSemaines");

            migrationBuilder.DropTable(
                name: "CategorieAide");

            migrationBuilder.DropTable(
                name: "Materiel");

            migrationBuilder.DropIndex(
                name: "IX_Membres_SexeId",
                table: "Membres");

            migrationBuilder.DropIndex(
                name: "IX_Feats_TypeId",
                table: "Feats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repondres",
                table: "Repondres");

            migrationBuilder.DropColumn(
                name: "SexeId",
                table: "Membres");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Feats");

            migrationBuilder.RenameTable(
                name: "Repondres",
                newName: "Repondre");

            migrationBuilder.RenameIndex(
                name: "IX_Repondres_IdFeat",
                table: "Repondre",
                newName: "IX_Repondre_IdFeat");

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Membres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Membres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Inscription",
                table: "Membres",
                type: "date",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Membres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repondre",
                table: "Repondre",
                columns: new[] { "IdMembre", "IdFeat" });

            migrationBuilder.AddForeignKey(
                name: "FK_Repondre_Feats_IdFeat",
                table: "Repondre",
                column: "IdFeat",
                principalTable: "Feats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repondre_Membres_IdMembre",
                table: "Repondre",
                column: "IdMembre",
                principalTable: "Membres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
