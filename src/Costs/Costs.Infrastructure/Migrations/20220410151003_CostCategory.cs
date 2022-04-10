using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Costs.Infrastructure.Migrations
{
    public partial class CostCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCategories_CostCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CostCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostCategories_ParentId",
                table: "CostCategories",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostCategories");
        }
    }
}
