using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaProject_API.Migrations
{
    /// <inheritdoc />
    public partial class AddingLocalUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 1, 29, 22, 49, 17, 462, DateTimeKind.Local).AddTicks(2608), "https://u.cubeupload.com/anton_demyanchuk/villa1.jpg " });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 1, 29, 22, 49, 17, 462, DateTimeKind.Local).AddTicks(2662), "https://u.cubeupload.com/anton_demyanchuk/villa2.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 1, 29, 22, 49, 17, 462, DateTimeKind.Local).AddTicks(2665), "https://u.cubeupload.com/anton_demyanchuk/villa5.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 1, 29, 22, 49, 17, 462, DateTimeKind.Local).AddTicks(2667), "https://u.cubeupload.com/anton_demyanchuk/villa4.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 1, 15, 21, 51, 40, 977, DateTimeKind.Local).AddTicks(2684), "" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 1, 15, 21, 51, 40, 977, DateTimeKind.Local).AddTicks(2746), "" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 1, 15, 21, 51, 40, 977, DateTimeKind.Local).AddTicks(2749), "" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateDate", "ImageUrl" },
                values: new object[] { new DateTime(2024, 1, 15, 21, 51, 40, 977, DateTimeKind.Local).AddTicks(2752), "" });
        }
    }
}
