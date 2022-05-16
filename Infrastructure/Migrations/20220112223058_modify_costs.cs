using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class modify_costs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildEducationalInstitution_EducationalInstitution_Educatio~",
                table: "ChildEducationalInstitution");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildHistory_Children_ChildId",
                table: "ChildHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationalInstitution_Families_FamilyId",
                table: "EducationalInstitution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationalInstitution",
                table: "EducationalInstitution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChildHistory",
                table: "ChildHistory");

            migrationBuilder.RenameTable(
                name: "EducationalInstitution",
                newName: "EducationalInstitutions");

            migrationBuilder.RenameTable(
                name: "ChildHistory",
                newName: "ChildHistories");

            migrationBuilder.RenameIndex(
                name: "IX_EducationalInstitution_FamilyId",
                table: "EducationalInstitutions",
                newName: "IX_EducationalInstitutions_FamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildHistory_ChildId",
                table: "ChildHistories",
                newName: "IX_ChildHistories_ChildId");

            migrationBuilder.AddColumn<int>(
                name: "EducationalInstitutionId",
                table: "Events",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationalInstitutions",
                table: "EducationalInstitutions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChildHistories",
                table: "ChildHistories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cost",
                columns: table => new
                {
                    CostId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CostName = table.Column<string>(type: "text", nullable: true),
                    CostDescription = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    CostDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cost", x => x.CostId);
                    table.ForeignKey(
                        name: "FK_Cost_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cost_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EducationalInstitutionId",
                table: "Events",
                column: "EducationalInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cost_CategoryId",
                table: "Cost",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cost_EventId",
                table: "Cost",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildEducationalInstitution_EducationalInstitutions_Educati~",
                table: "ChildEducationalInstitution",
                column: "EducationalInstitutionsId",
                principalTable: "EducationalInstitutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildHistories_Children_ChildId",
                table: "ChildHistories",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalInstitutions_Families_FamilyId",
                table: "EducationalInstitutions",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EducationalInstitutions_EducationalInstitutionId",
                table: "Events",
                column: "EducationalInstitutionId",
                principalTable: "EducationalInstitutions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildEducationalInstitution_EducationalInstitutions_Educati~",
                table: "ChildEducationalInstitution");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildHistories_Children_ChildId",
                table: "ChildHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationalInstitutions_Families_FamilyId",
                table: "EducationalInstitutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EducationalInstitutions_EducationalInstitutionId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Cost");

            migrationBuilder.DropIndex(
                name: "IX_Events_EducationalInstitutionId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationalInstitutions",
                table: "EducationalInstitutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChildHistories",
                table: "ChildHistories");

            migrationBuilder.DropColumn(
                name: "EducationalInstitutionId",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "EducationalInstitutions",
                newName: "EducationalInstitution");

            migrationBuilder.RenameTable(
                name: "ChildHistories",
                newName: "ChildHistory");

            migrationBuilder.RenameIndex(
                name: "IX_EducationalInstitutions_FamilyId",
                table: "EducationalInstitution",
                newName: "IX_EducationalInstitution_FamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildHistories_ChildId",
                table: "ChildHistory",
                newName: "IX_ChildHistory_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationalInstitution",
                table: "EducationalInstitution",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChildHistory",
                table: "ChildHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildEducationalInstitution_EducationalInstitution_Educatio~",
                table: "ChildEducationalInstitution",
                column: "EducationalInstitutionsId",
                principalTable: "EducationalInstitution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildHistory_Children_ChildId",
                table: "ChildHistory",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalInstitution_Families_FamilyId",
                table: "EducationalInstitution",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
