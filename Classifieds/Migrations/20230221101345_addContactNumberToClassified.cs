using Microsoft.EntityFrameworkCore.Migrations;

namespace Classifieds.Migrations
{
    public partial class addContactNumberToClassified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Classifieds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Classifieds");
        }
    }
}
