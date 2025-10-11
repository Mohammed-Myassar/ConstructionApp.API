using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationbitweenTaskAndResourceUsage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceUsages_Resources_ResourceId",
                table: "ResourceUsages");

            migrationBuilder.DropIndex(
                name: "IX_ResourceUsages_ResourceId",
                table: "ResourceUsages");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "ResourceUsages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "ResourceUsages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceUsages_ResourceId",
                table: "ResourceUsages",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceUsages_Resources_ResourceId",
                table: "ResourceUsages",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
