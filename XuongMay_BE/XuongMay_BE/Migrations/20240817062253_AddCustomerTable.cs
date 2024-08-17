using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XuongMay_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisor_ProductionLine_LineID",
                table: "Supervisor");

            migrationBuilder.DropIndex(
                name: "IX_Supervisor_LineID",
                table: "Supervisor");

            migrationBuilder.DropIndex(
                name: "IX_ProductionLine_SupervisorID",
                table: "ProductionLine");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_SupervisorID",
                table: "ProductionLine",
                column: "SupervisorID",
                unique: true,
                filter: "[SupervisorID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_ProductionLine_SupervisorID",
                table: "ProductionLine");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_LineID",
                table: "Supervisor",
                column: "LineID",
                unique: true,
                filter: "[LineID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_SupervisorID",
                table: "ProductionLine",
                column: "SupervisorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_ProductionLine_LineID",
                table: "Supervisor",
                column: "LineID",
                principalTable: "ProductionLine",
                principalColumn: "LineID");
        }
    }
}
