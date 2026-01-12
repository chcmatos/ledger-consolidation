using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consolidation.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditAndDebitToDailyBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "amount",
                table: "daily_balances",
                newName: "debit_total");

            migrationBuilder.AddColumn<decimal>(
                name: "credit_total",
                table: "daily_balances",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "credit_total",
                table: "daily_balances");

            migrationBuilder.RenameColumn(
                name: "debit_total",
                table: "daily_balances",
                newName: "amount");
        }
    }
}
