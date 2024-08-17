using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XuongMay_BE.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_CategoryID1",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_CategoryID1",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CategoryID1",
                table: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryID1",
                table: "Category",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryID1",
                table: "Category",
                column: "CategoryID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_CategoryID1",
                table: "Category",
                column: "CategoryID1",
                principalTable: "Category",
                principalColumn: "CategoryID");
        }
    }
}
