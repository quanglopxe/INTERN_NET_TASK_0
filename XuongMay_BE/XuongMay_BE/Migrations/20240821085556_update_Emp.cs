using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XuongMay_BE.Migrations
{
    /// <inheritdoc />
    public partial class update_Emp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_ProductionLine_ProductionLinesLineID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_ProductionLinesLineID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ProductionLinesLineID",
                table: "Employee");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_LineID",
                table: "Employee",
                column: "LineID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_ProductionLine_LineID",
                table: "Employee",
                column: "LineID",
                principalTable: "ProductionLine",
                principalColumn: "LineID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_ProductionLine_LineID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_LineID",
                table: "Employee");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductionLinesLineID",
                table: "Employee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ProductionLinesLineID",
                table: "Employee",
                column: "ProductionLinesLineID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_ProductionLine_ProductionLinesLineID",
                table: "Employee",
                column: "ProductionLinesLineID",
                principalTable: "ProductionLine",
                principalColumn: "LineID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
