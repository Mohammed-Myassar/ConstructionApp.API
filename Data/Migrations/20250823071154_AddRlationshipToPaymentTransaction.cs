using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRlationshipToPaymentTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Projects_ConstructionProjectId",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_ConstructionProjectId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "ConstructionProjectId",
                table: "PaymentTransactions");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "PaymentTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_ProjectId",
                table: "PaymentTransactions",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Projects_ProjectId",
                table: "PaymentTransactions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Projects_ProjectId",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_ProjectId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "PaymentTransactions");

            migrationBuilder.AddColumn<int>(
                name: "ConstructionProjectId",
                table: "PaymentTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_ConstructionProjectId",
                table: "PaymentTransactions",
                column: "ConstructionProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Projects_ConstructionProjectId",
                table: "PaymentTransactions",
                column: "ConstructionProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
