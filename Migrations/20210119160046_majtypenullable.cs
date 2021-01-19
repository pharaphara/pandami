using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandami.Migrations
{
    public partial class majtypenullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeAides_CategorieAide_CategorieAideId",
                table: "TypeAides");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeAides_Materiel_MaterielId",
                table: "TypeAides");

            migrationBuilder.DropIndex(
                name: "IX_TypeAides_MaterielId",
                table: "TypeAides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materiel",
                table: "Materiel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategorieAide",
                table: "CategorieAide");

            migrationBuilder.DropColumn(
                name: "MaterielId",
                table: "TypeAides");

            migrationBuilder.RenameTable(
                name: "Materiel",
                newName: "Materiels");

            migrationBuilder.RenameTable(
                name: "CategorieAide",
                newName: "CategorieAides");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinDate",
                table: "Suspensions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DesistementDate",
                table: "Repondres",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "AdresseId",
                table: "Membres",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SurPlace",
                table: "Feats",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<float>(
                name: "SommeRembourseeDate",
                table: "Feats",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SommePrevoir",
                table: "Feats",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<float>(
                name: "SommeAvancee",
                table: "Feats",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinFeatHelper",
                table: "Feats",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnCoursRealisation",
                table: "Feats",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClotureDate",
                table: "Feats",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AnnulationDate",
                table: "Feats",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AcceptationDate",
                table: "Feats",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "AdresseId",
                table: "Feats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateurId",
                table: "Feats",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValiditeFinDate",
                table: "Disponibilites",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "TypeAideId",
                table: "Materiels",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materiels",
                table: "Materiels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategorieAides",
                table: "CategorieAides",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDeVoie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomDeVoie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodePostale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    latitude = table.Column<float>(type: "real", nullable: false),
                    longitude = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreferenceAide",
                columns: table => new
                {
                    IdMembre = table.Column<int>(type: "int", nullable: false),
                    IdTypeAide = table.Column<int>(type: "int", nullable: false),
                    ValiditeDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValiditeFin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenceAide", x => new { x.IdMembre, x.IdTypeAide });
                    table.ForeignKey(
                        name: "FK_PreferenceAide_Membres_IdMembre",
                        column: x => x.IdMembre,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenceAide_TypeAides_IdTypeAide",
                        column: x => x.IdTypeAide,
                        principalTable: "TypeAides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Membres_AdresseId",
                table: "Membres",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Feats_AdresseId",
                table: "Feats",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Feats_CreateurId",
                table: "Feats",
                column: "CreateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Materiels_TypeAideId",
                table: "Materiels",
                column: "TypeAideId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceAide_IdTypeAide",
                table: "PreferenceAide",
                column: "IdTypeAide");

            migrationBuilder.AddForeignKey(
                name: "FK_Feats_Adresses_AdresseId",
                table: "Feats",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feats_Membres_CreateurId",
                table: "Feats",
                column: "CreateurId",
                principalTable: "Membres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materiels_TypeAides_TypeAideId",
                table: "Materiels",
                column: "TypeAideId",
                principalTable: "TypeAides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Membres_Adresses_AdresseId",
                table: "Membres",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeAides_CategorieAides_CategorieAideId",
                table: "TypeAides",
                column: "CategorieAideId",
                principalTable: "CategorieAides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feats_Adresses_AdresseId",
                table: "Feats");

            migrationBuilder.DropForeignKey(
                name: "FK_Feats_Membres_CreateurId",
                table: "Feats");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiels_TypeAides_TypeAideId",
                table: "Materiels");

            migrationBuilder.DropForeignKey(
                name: "FK_Membres_Adresses_AdresseId",
                table: "Membres");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeAides_CategorieAides_CategorieAideId",
                table: "TypeAides");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "PreferenceAide");

            migrationBuilder.DropIndex(
                name: "IX_Membres_AdresseId",
                table: "Membres");

            migrationBuilder.DropIndex(
                name: "IX_Feats_AdresseId",
                table: "Feats");

            migrationBuilder.DropIndex(
                name: "IX_Feats_CreateurId",
                table: "Feats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materiels",
                table: "Materiels");

            migrationBuilder.DropIndex(
                name: "IX_Materiels_TypeAideId",
                table: "Materiels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategorieAides",
                table: "CategorieAides");

            migrationBuilder.DropColumn(
                name: "AdresseId",
                table: "Membres");

            migrationBuilder.DropColumn(
                name: "AdresseId",
                table: "Feats");

            migrationBuilder.DropColumn(
                name: "CreateurId",
                table: "Feats");

            migrationBuilder.DropColumn(
                name: "TypeAideId",
                table: "Materiels");

            migrationBuilder.RenameTable(
                name: "Materiels",
                newName: "Materiel");

            migrationBuilder.RenameTable(
                name: "CategorieAides",
                newName: "CategorieAide");

            migrationBuilder.AddColumn<int>(
                name: "MaterielId",
                table: "TypeAides",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinDate",
                table: "Suspensions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DesistementDate",
                table: "Repondres",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SurPlace",
                table: "Feats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "SommeRembourseeDate",
                table: "Feats",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SommePrevoir",
                table: "Feats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "SommeAvancee",
                table: "Feats",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinFeatHelper",
                table: "Feats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnCoursRealisation",
                table: "Feats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClotureDate",
                table: "Feats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AnnulationDate",
                table: "Feats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AcceptationDate",
                table: "Feats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValiditeFinDate",
                table: "Disponibilites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materiel",
                table: "Materiel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategorieAide",
                table: "CategorieAide",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TypeAides_MaterielId",
                table: "TypeAides",
                column: "MaterielId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeAides_CategorieAide_CategorieAideId",
                table: "TypeAides",
                column: "CategorieAideId",
                principalTable: "CategorieAide",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeAides_Materiel_MaterielId",
                table: "TypeAides",
                column: "MaterielId",
                principalTable: "Materiel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
