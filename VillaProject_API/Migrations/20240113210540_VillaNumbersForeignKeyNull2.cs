using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaProject_API.Migrations
{
    /// <inheritdoc />
    public partial class VillaNumbersForeignKeyNull2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 204,
                column: "VillaId",
                value: null);
            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "VillaNo",
                keyValue: 103,
                column: "VillaId",
                value: null);
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 23, 5, 39, 652, DateTimeKind.Local).AddTicks(4921));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 23, 5, 39, 652, DateTimeKind.Local).AddTicks(5037));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 23, 5, 39, 652, DateTimeKind.Local).AddTicks(5043));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 23, 5, 39, 652, DateTimeKind.Local).AddTicks(5049));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 23, 2, 41, 465, DateTimeKind.Local).AddTicks(2896));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 23, 2, 41, 465, DateTimeKind.Local).AddTicks(2979));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 23, 2, 41, 465, DateTimeKind.Local).AddTicks(2985));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 23, 2, 41, 465, DateTimeKind.Local).AddTicks(2991));
        }
    }
}
