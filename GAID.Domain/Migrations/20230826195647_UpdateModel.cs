using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GAID.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProgramThumbnailId",
                table: "Programs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerThumbnailId",
                table: "Partners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ProgramThumbnailId",
                table: "Programs",
                column: "ProgramThumbnailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partners_PartnerThumbnailId",
                table: "Partners",
                column: "PartnerThumbnailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Attachments_PartnerThumbnailId",
                table: "Partners",
                column: "PartnerThumbnailId",
                principalTable: "Attachments",
                principalColumn: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Attachments_ProgramThumbnailId",
                table: "Programs",
                column: "ProgramThumbnailId",
                principalTable: "Attachments",
                principalColumn: "AttachmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Attachments_PartnerThumbnailId",
                table: "Partners");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Attachments_ProgramThumbnailId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ProgramThumbnailId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Partners_PartnerThumbnailId",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "ProgramThumbnailId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "PartnerThumbnailId",
                table: "Partners");
        }
    }
}
