using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XuongMay_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddSupervisorAndProductionLineTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductionLine",
                columns: table => new
                {
                    LineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SupervisorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLine", x => x.LineID);
                });

            migrationBuilder.CreateTable(
                name: "Supervisor",
                columns: table => new
                {
                    SupervisorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupervisorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LineID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisor", x => x.SupervisorID);
                    table.ForeignKey(
                        name: "FK_Supervisor_ProductionLine_LineID",
                        column: x => x.LineID,
                        principalTable: "ProductionLine",
                        principalColumn: "LineID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLine_SupervisorID",
                table: "ProductionLine",
                column: "SupervisorID");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_LineID",
                table: "Supervisor",
                column: "LineID",
                unique: true,
                filter: "[LineID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionLine_Supervisor_SupervisorID",
                table: "ProductionLine",
                column: "SupervisorID",
                principalTable: "Supervisor",
                principalColumn: "SupervisorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionLine_Supervisor_SupervisorID",
                table: "ProductionLine");

            migrationBuilder.DropTable(
                name: "Supervisor");

            migrationBuilder.DropTable(
                name: "ProductionLine");
        }
    }
}
