using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XuongMay_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddEmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Supervisor",
                columns: table => new
                {
                    SupervisorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupervisorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisor", x => x.SupervisorID);
                });

            migrationBuilder.CreateTable(
                name: "ProductionLines",
                columns: table => new
                {
                    LineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupervisorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLines", x => x.LineID);
                    table.ForeignKey(
                        name: "FK_ProductionLines_Supervisor_SupervisorID",
                        column: x => x.SupervisorID,
                        principalTable: "Supervisor",
                        principalColumn: "SupervisorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmpID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmpID);
                    table.ForeignKey(
                        name: "FK_Employee_ProductionLines_LineID",
                        column: x => x.LineID,
                        principalTable: "ProductionLines",
                        principalColumn: "LineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_LineID",
                table: "Employee",
                column: "LineID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLines_SupervisorID",
                table: "ProductionLines",
                column: "SupervisorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "ProductionLines");

            migrationBuilder.DropTable(
                name: "Supervisor");
        }
    }
}
