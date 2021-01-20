using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandami.Migrations
{
    public partial class AjoutboolFeat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EchangeMonetaire",
                table: "Feats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EchangeMonetaire",
                table: "Feats");
        }
    }
}
