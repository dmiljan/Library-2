using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class RenamedColumnNameInBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pages_number",
                table: "Books",
                newName: "PagesNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PagesNumber",
                table: "Books",
                newName: "Pages_number");
        }
    }
}
