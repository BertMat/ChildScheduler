using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class initialize_costs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cost_Categories_CategoryId",
                table: "Cost");

            migrationBuilder.DropForeignKey(
                name: "FK_Cost_Events_EventId",
                table: "Cost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cost",
                table: "Cost");

            migrationBuilder.RenameTable(
                name: "Cost",
                newName: "Costs");

            migrationBuilder.RenameIndex(
                name: "IX_Cost_EventId",
                table: "Costs",
                newName: "IX_Costs_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Cost_CategoryId",
                table: "Costs",
                newName: "IX_Costs_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Costs",
                table: "Costs",
                column: "CostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Categories_CategoryId",
                table: "Costs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Events_EventId",
                table: "Costs",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Categories_CategoryId",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Events_EventId",
                table: "Costs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Costs",
                table: "Costs");

            migrationBuilder.RenameTable(
                name: "Costs",
                newName: "Cost");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_EventId",
                table: "Cost",
                newName: "IX_Cost_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_CategoryId",
                table: "Cost",
                newName: "IX_Cost_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cost",
                table: "Cost",
                column: "CostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cost_Categories_CategoryId",
                table: "Cost",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cost_Events_EventId",
                table: "Cost",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
