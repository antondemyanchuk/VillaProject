using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaProject_API.Migrations
{
    /// <inheritdoc />
    public partial class VillaNumbersTableUPD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "VillaNumbers",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "VillaNumbers",
                newName: "CreateDate");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 22, 5, 35, 345, DateTimeKind.Local).AddTicks(9662));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 22, 5, 35, 345, DateTimeKind.Local).AddTicks(9720));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 22, 5, 35, 345, DateTimeKind.Local).AddTicks(9722));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2024, 1, 13, 22, 5, 35, 345, DateTimeKind.Local).AddTicks(9725));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "VillaNumbers",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "VillaNumbers",
                newName: "CreatedDate");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 1, 12, 22, 49, 29, 760, DateTimeKind.Local).AddTicks(3729));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 1, 12, 22, 49, 29, 760, DateTimeKind.Local).AddTicks(3784));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 1, 12, 22, 49, 29, 760, DateTimeKind.Local).AddTicks(3786));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2024, 1, 12, 22, 49, 29, 760, DateTimeKind.Local).AddTicks(3788));
        }
    }
}
