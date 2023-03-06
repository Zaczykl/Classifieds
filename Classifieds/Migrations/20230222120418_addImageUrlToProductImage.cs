using Microsoft.EntityFrameworkCore.Migrations;

namespace Classifieds.Migrations
{
    public partial class addImageUrlToProductImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "ProductImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "ProductImages");
        }
    }
}
