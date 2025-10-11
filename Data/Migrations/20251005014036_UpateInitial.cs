using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Projects_ProjectId",
                table: "PaymentTransactions");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "PaymentTransactions",
                newName: "ConstructionProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_ProjectId",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_ConstructionProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Projects_ConstructionProjectId",
                table: "PaymentTransactions",
                column: "ConstructionProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Projects_ConstructionProjectId",
                table: "PaymentTransactions");

            migrationBuilder.RenameColumn(
                name: "ConstructionProjectId",
                table: "PaymentTransactions",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_ConstructionProjectId",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Projects_ProjectId",
                table: "PaymentTransactions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
