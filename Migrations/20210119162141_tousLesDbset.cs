using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandami.Migrations
{
    public partial class tousLesDbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Litige_Feats_FeatId",
                table: "Litige");

            migrationBuilder.DropForeignKey(
                name: "FK_Litige_Membres_MembreId",
                table: "Litige");

            migrationBuilder.DropForeignKey(
                name: "FK_Litige_TypeLitige_TypeLitigeId",
                table: "Litige");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceAide_Membres_IdMembre",
                table: "PreferenceAide");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceAide_TypeAides_IdTypeAide",
                table: "PreferenceAide");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeLitige",
                table: "TypeLitige");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreferenceAide",
                table: "PreferenceAide");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Litige",
                table: "Litige");

            migrationBuilder.RenameTable(
                name: "TypeLitige",
                newName: "TypeLitiges");

            migrationBuilder.RenameTable(
                name: "PreferenceAide",
                newName: "PreferenceAides");

            migrationBuilder.RenameTable(
                name: "Litige",
                newName: "Litiges");

            migrationBuilder.RenameIndex(
                name: "IX_PreferenceAide_IdTypeAide",
                table: "PreferenceAides",
                newName: "IX_PreferenceAides_IdTypeAide");

            migrationBuilder.RenameIndex(
                name: "IX_Litige_TypeLitigeId",
                table: "Litiges",
                newName: "IX_Litiges_TypeLitigeId");

            migrationBuilder.RenameIndex(
                name: "IX_Litige_MembreId",
                table: "Litiges",
                newName: "IX_Litiges_MembreId");

            migrationBuilder.RenameIndex(
                name: "IX_Litige_FeatId",
                table: "Litiges",
                newName: "IX_Litiges_FeatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeLitiges",
                table: "TypeLitiges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreferenceAides",
                table: "PreferenceAides",
                columns: new[] { "IdMembre", "IdTypeAide" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Litiges",
                table: "Litiges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Litiges_Feats_FeatId",
                table: "Litiges",
                column: "FeatId",
                principalTable: "Feats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Litiges_Membres_MembreId",
                table: "Litiges",
                column: "MembreId",
                principalTable: "Membres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Litiges_TypeLitiges_TypeLitigeId",
                table: "Litiges",
                column: "TypeLitigeId",
                principalTable: "TypeLitiges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceAides_Membres_IdMembre",
                table: "PreferenceAides",
                column: "IdMembre",
                principalTable: "Membres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceAides_TypeAides_IdTypeAide",
                table: "PreferenceAides",
                column: "IdTypeAide",
                principalTable: "TypeAides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Litiges_Feats_FeatId",
                table: "Litiges");

            migrationBuilder.DropForeignKey(
                name: "FK_Litiges_Membres_MembreId",
                table: "Litiges");

            migrationBuilder.DropForeignKey(
                name: "FK_Litiges_TypeLitiges_TypeLitigeId",
                table: "Litiges");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceAides_Membres_IdMembre",
                table: "PreferenceAides");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceAides_TypeAides_IdTypeAide",
                table: "PreferenceAides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeLitiges",
                table: "TypeLitiges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreferenceAides",
                table: "PreferenceAides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Litiges",
                table: "Litiges");

            migrationBuilder.RenameTable(
                name: "TypeLitiges",
                newName: "TypeLitige");

            migrationBuilder.RenameTable(
                name: "PreferenceAides",
                newName: "PreferenceAide");

            migrationBuilder.RenameTable(
                name: "Litiges",
                newName: "Litige");

            migrationBuilder.RenameIndex(
                name: "IX_PreferenceAides_IdTypeAide",
                table: "PreferenceAide",
                newName: "IX_PreferenceAide_IdTypeAide");

            migrationBuilder.RenameIndex(
                name: "IX_Litiges_TypeLitigeId",
                table: "Litige",
                newName: "IX_Litige_TypeLitigeId");

            migrationBuilder.RenameIndex(
                name: "IX_Litiges_MembreId",
                table: "Litige",
                newName: "IX_Litige_MembreId");

            migrationBuilder.RenameIndex(
                name: "IX_Litiges_FeatId",
                table: "Litige",
                newName: "IX_Litige_FeatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeLitige",
                table: "TypeLitige",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreferenceAide",
                table: "PreferenceAide",
                columns: new[] { "IdMembre", "IdTypeAide" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Litige",
                table: "Litige",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Litige_Feats_FeatId",
                table: "Litige",
                column: "FeatId",
                principalTable: "Feats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Litige_Membres_MembreId",
                table: "Litige",
                column: "MembreId",
                principalTable: "Membres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Litige_TypeLitige_TypeLitigeId",
                table: "Litige",
                column: "TypeLitigeId",
                principalTable: "TypeLitige",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceAide_Membres_IdMembre",
                table: "PreferenceAide",
                column: "IdMembre",
                principalTable: "Membres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceAide_TypeAides_IdTypeAide",
                table: "PreferenceAide",
                column: "IdTypeAide",
                principalTable: "TypeAides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
