using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GAID.Domain.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_AspNetUsers_CreatedById",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_AspNetUsers_ModifiedById",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Enrollment_EnrollmentId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_AspNetUsers_CreatedById",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_AspNetUsers_ModifiedById",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Programs_ProgramId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Page_AspNetUsers_CreatedById",
                table: "Page");

            migrationBuilder.DropForeignKey(
                name: "FK_Page_AspNetUsers_ModifiedById",
                table: "Page");

            migrationBuilder.DropForeignKey(
                name: "FK_Page_Partners_PartnerId",
                table: "Page");

            migrationBuilder.DropForeignKey(
                name: "FK_Page_Programs_ProgramId",
                table: "Page");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Page",
                table: "Page");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Donation",
                table: "Donation");

            migrationBuilder.RenameTable(
                name: "Page",
                newName: "Pages");

            migrationBuilder.RenameTable(
                name: "Enrollment",
                newName: "Enrollments");

            migrationBuilder.RenameTable(
                name: "Donation",
                newName: "Donations");

            migrationBuilder.RenameIndex(
                name: "IX_Page_ProgramId",
                table: "Pages",
                newName: "IX_Pages_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Page_PartnerId",
                table: "Pages",
                newName: "IX_Pages_PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Page_ModifiedById",
                table: "Pages",
                newName: "IX_Pages_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Page_CreatedById",
                table: "Pages",
                newName: "IX_Pages_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_ProgramId",
                table: "Enrollments",
                newName: "IX_Enrollments_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_ModifiedById",
                table: "Enrollments",
                newName: "IX_Enrollments_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_CreatedById",
                table: "Enrollments",
                newName: "IX_Enrollments_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Donation_ModifiedById",
                table: "Donations",
                newName: "IX_Donations_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Donation_EnrollmentId",
                table: "Donations",
                newName: "IX_Donations_EnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Donation_CreatedById",
                table: "Donations",
                newName: "IX_Donations_CreatedById");

            migrationBuilder.AddColumn<string>(
                name: "PaypalOrderId",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pages",
                table: "Pages",
                column: "PageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "EnrollmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Donations",
                table: "Donations",
                column: "DonationId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), null, "Admin", "Admin" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), null, "Partner", "Partner" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), null, "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PaymentInformation", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000001"), 0, "6a9943f8-af5b-4231-9a8d-63f8c43c6e0c", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@email.com", true, "System Admin", false, false, null, "admin@email.com", "admin", "AQAAAAIAAYagAAAAEMeRmOWs9W/KsBTc0NEYwk5Efsp1rjs48fPIPWSW0xhuuKWByjTRlnJXKrEmn9yPhA==", "", null, false, null, "3YYPM246ONSVZFAKY3TR2JSVKMX7ZM4D", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001") });

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_AspNetUsers_CreatedById",
                table: "Donations",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_AspNetUsers_ModifiedById",
                table: "Donations",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Enrollments_EnrollmentId",
                table: "Donations",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "EnrollmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_CreatedById",
                table: "Enrollments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AspNetUsers_ModifiedById",
                table: "Enrollments",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Programs_ProgramId",
                table: "Enrollments",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "ProgramId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_AspNetUsers_CreatedById",
                table: "Pages",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_AspNetUsers_ModifiedById",
                table: "Pages",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Partners_PartnerId",
                table: "Pages",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Programs_ProgramId",
                table: "Pages",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "ProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_AspNetUsers_CreatedById",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_AspNetUsers_ModifiedById",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Enrollments_EnrollmentId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_CreatedById",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AspNetUsers_ModifiedById",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Programs_ProgramId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_AspNetUsers_CreatedById",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_AspNetUsers_ModifiedById",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Partners_PartnerId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Programs_ProgramId",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Donations",
                table: "Donations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DropColumn(
                name: "PaypalOrderId",
                table: "Donations");

            migrationBuilder.RenameTable(
                name: "Pages",
                newName: "Page");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "Enrollment");

            migrationBuilder.RenameTable(
                name: "Donations",
                newName: "Donation");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_ProgramId",
                table: "Page",
                newName: "IX_Page_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_PartnerId",
                table: "Page",
                newName: "IX_Page_PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_ModifiedById",
                table: "Page",
                newName: "IX_Page_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_CreatedById",
                table: "Page",
                newName: "IX_Page_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_ProgramId",
                table: "Enrollment",
                newName: "IX_Enrollment_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_ModifiedById",
                table: "Enrollment",
                newName: "IX_Enrollment_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CreatedById",
                table: "Enrollment",
                newName: "IX_Enrollment_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_ModifiedById",
                table: "Donation",
                newName: "IX_Donation_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_EnrollmentId",
                table: "Donation",
                newName: "IX_Donation_EnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_CreatedById",
                table: "Donation",
                newName: "IX_Donation_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Page",
                table: "Page",
                column: "PageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment",
                column: "EnrollmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Donation",
                table: "Donation",
                column: "DonationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_AspNetUsers_CreatedById",
                table: "Donation",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_AspNetUsers_ModifiedById",
                table: "Donation",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Enrollment_EnrollmentId",
                table: "Donation",
                column: "EnrollmentId",
                principalTable: "Enrollment",
                principalColumn: "EnrollmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_AspNetUsers_CreatedById",
                table: "Enrollment",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_AspNetUsers_ModifiedById",
                table: "Enrollment",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Programs_ProgramId",
                table: "Enrollment",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "ProgramId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Page_AspNetUsers_CreatedById",
                table: "Page",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Page_AspNetUsers_ModifiedById",
                table: "Page",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Page_Partners_PartnerId",
                table: "Page",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Page_Programs_ProgramId",
                table: "Page",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "ProgramId");
        }
    }
}
