using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class add_event_contact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildPhoto_Children_ChildId",
                table: "ChildPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_EventPhoto_Events_EventId",
                table: "EventPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventPhoto",
                table: "EventPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChildPhoto",
                table: "ChildPhoto");

            migrationBuilder.RenameTable(
                name: "EventPhoto",
                newName: "EventPhotos");

            migrationBuilder.RenameTable(
                name: "ChildPhoto",
                newName: "ChildPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_EventPhoto_EventId",
                table: "EventPhotos",
                newName: "IX_EventPhotos_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildPhoto_ChildId",
                table: "ChildPhotos",
                newName: "IX_ChildPhotos_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventPhotos",
                table: "EventPhotos",
                column: "EventPhotoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChildPhotos",
                table: "ChildPhotos",
                column: "ChildPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildPhotos_Children_ChildId",
                table: "ChildPhotos",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventPhotos_Events_EventId",
                table: "EventPhotos",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildPhotos_Children_ChildId",
                table: "ChildPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_EventPhotos_Events_EventId",
                table: "EventPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventPhotos",
                table: "EventPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChildPhotos",
                table: "ChildPhotos");

            migrationBuilder.RenameTable(
                name: "EventPhotos",
                newName: "EventPhoto");

            migrationBuilder.RenameTable(
                name: "ChildPhotos",
                newName: "ChildPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_EventPhotos_EventId",
                table: "EventPhoto",
                newName: "IX_EventPhoto_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildPhotos_ChildId",
                table: "ChildPhoto",
                newName: "IX_ChildPhoto_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventPhoto",
                table: "EventPhoto",
                column: "EventPhotoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChildPhoto",
                table: "ChildPhoto",
                column: "ChildPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildPhoto_Children_ChildId",
                table: "ChildPhoto",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventPhoto_Events_EventId",
                table: "EventPhoto",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
