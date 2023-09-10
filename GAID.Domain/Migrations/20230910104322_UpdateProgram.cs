using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GAID.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Programs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Programs",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "EmailTemplateId", "Body", "EmailTemplateType", "Subject" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000004"), "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n    <title>Document</title>\r\n  </head>\r\n  <body>\r\n    <div\r\n      style=\"\r\n        display: flex;\r\n        flex-direction: column;\r\n        width: 100%;\r\n        font-family: Poppins, sans-serif;\r\n        color: #8b8b8b;\r\n        background-color: #fff5ed;\r\n      \"\r\n    >\r\n      <div style=\"display: flex; flex-direction: row; height: 100px\">\r\n        <div style=\"background: #158f67; opacity: 0.5; flex: 1\"></div>\r\n        <div style=\"background: #e9b929; opacity: 0.5; flex: 1\"></div>\r\n        <div style=\"background: #fd4c42; opacity: 0.5; flex: 1\"></div>\r\n        <div style=\"background: #396dc4; opacity: 0.5; flex: 1\"></div>\r\n        <img\r\n          src=\"https://sem33proj.blob.core.windows.net/attachments/a4370000-3a9b-000d-7d3c-08dbadbe1ad8\"\r\n          alt=\"\"\r\n          style=\"position: absolute; width: 80px; height: 80px; margin: 10px\"\r\n        />\r\n      </div>\r\n      <div style=\"padding: 1% 10%\">\r\n        <h2 style=\"color: #fd645b\">Dear [[Recipient_Name]],</h2>\r\n        <p>\r\n          I hope this message finds you well. We are excited to extend an\r\n          invitation to you for an upcoming event that promises to be both\r\n          inspiring and impactful. Give-AID is hosting a special program, and we\r\n          believe your participation would greatly enrich our efforts.\r\n        </p>\r\n        <p>Here's our program information:</p>\r\n        <table style=\"border: #8b8b8b 1px solid; width: 50%;\">\r\n          <tr style=\"display: flex; gap: 10px; padding: 8px;\">\r\n            <th style=\"width: 200px; text-align: start;\">\r\n              <b>Program Name</b>\r\n            </th>\r\n            <td style=\"flex: 1\">[[Program_Name]]</td>\r\n          </tr>\r\n          <tr style=\"display: flex; gap: 10px; padding: 8px;\">\r\n            <th style=\"width: 200px; text-align: start;\">\r\n              <b>Partner</b>\r\n            </th>\r\n            <td style=\"flex: 1\">[[Partner_Name]]</td>\r\n          </tr>\r\n          <tr style=\"display: flex; gap: 10px; padding: 8px;\">\r\n            <th style=\"width: 200px; text-align: start;\">\r\n              <b>Start Date</b>\r\n            </th>\r\n            <td style=\"flex: 1\">[[Start_Date]]</td>\r\n          </tr>\r\n          <tr style=\"display: flex; gap: 10px; padding: 8px;\">\r\n            <th style=\"width: 200px; text-align: start;\"><b>End Date</b></th>\r\n            <td style=\"flex: 1\">[[End_Date]]</td>\r\n          </tr>\r\n          <tr style=\"display: flex; gap: 10px; padding: 8px;\">\r\n            <th style=\"width: 200px; text-align: start;\"><b>Target</b></th>\r\n            <td style=\"flex: 1\">[[Target]]</td>\r\n          </tr>\r\n        </table>\r\n        <p>\r\n          We are committed to creating a positive impact, and your presence at\r\n          our program would mean a great deal to us. Together, we can make a\r\n          difference in our community and beyond.\r\n        </p>\r\n        <p>\r\n          Should you have any questions or require further information, please do not hesitate to reach out to us at, please follow this link:\r\n          <a href=\"[[Program_Url]]\" style=\"font-style: italic\"\r\n            >[[Program_Name]]_[[Partner_Name]]</a\r\n          >\r\n        </p>\r\n        <p>\r\n          Thank you once again for choosing to be a part of Give-AID. We look\r\n          forward to working alongside you and creating a brighter future for\r\n          all.\r\n        </p>\r\n        <p>\r\n          <b><i>With deep gratitude,</i></b>\r\n        </p>\r\n        <p>\r\n          <b><i>[[Partner_Name]]</i></b>\r\n        </p>\r\n        <p>\r\n          <b><i>Give-AID</i></b>\r\n        </p>\r\n        <p>\r\n          <b><i>[[Home_Url]]</i></b>\r\n        </p>\r\n      </div>\r\n    </div>\r\n  </body>\r\n</html>\r\n", 3, "[Give-AID] Join Us in Making a Difference: Invitation to Our NGO Program" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "EmailTemplateId",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"));

            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Programs");
        }
    }
}
