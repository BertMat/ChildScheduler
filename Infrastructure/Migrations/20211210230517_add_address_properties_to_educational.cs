using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class add_address_properties_to_educational : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "EducationalInstitution",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "EducationalInstitution",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "EducationalInstitution",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "EducationalInstitution",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "EducationalInstitution");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "EducationalInstitution");

            migrationBuilder.DropColumn(
                name: "State",
                table: "EducationalInstitution");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "EducationalInstitution");
        }
    }
}
