using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageApi_Task1.Migrations
{
    public partial class addImageTable_metatag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Metatag",
                table: "Iamges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metatag",
                table: "Iamges");
        }
    }
}
