using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.SqlServer.Types;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotNet8NewFeatures.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Path = table.Column<SqlHierarchyId>(type: "hierarchyid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Departments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name", "ParentId", "Path" },
                values: new object[,]
                {
                    { 1, "Root", null, Microsoft.SqlServer.Types.SqlHierarchyId.Parse("/") },
                    { 2, "Technology", 1, Microsoft.SqlServer.Types.SqlHierarchyId.Parse("/1/") },
                    { 3, "HR", 1, Microsoft.SqlServer.Types.SqlHierarchyId.Parse("/2/") },
                    { 4, "Domestic-Flight", 2, Microsoft.SqlServer.Types.SqlHierarchyId.Parse("/1/2/") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentId",
                table: "Departments",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
