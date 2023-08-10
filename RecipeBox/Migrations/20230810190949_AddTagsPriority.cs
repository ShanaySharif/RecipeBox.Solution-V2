using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBox.Migrations
{
    public partial class AddTagsPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Tags");
        }
    }
}
