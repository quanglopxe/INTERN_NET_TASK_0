using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XuongMay_BE.Migrations
{
    /// <inheritdoc />
    public partial class add_Task_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    TaskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedTo = table.Column<int>(type: "int", nullable: false),
                    EmpID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedBy = table.Column<int>(type: "int", nullable: false),
                    SupervisorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.TaskID);
                    table.ForeignKey(
                        name: "FK_Task_Employee_EmpID",
                        column: x => x.EmpID,
                        principalTable: "Employee",
                        principalColumn: "EmpID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Stage_StageID",
                        column: x => x.StageID,
                        principalTable: "Stage",
                        principalColumn: "StageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Supervisor_SupervisorID",
                        column: x => x.SupervisorID,
                        principalTable: "Supervisor",
                        principalColumn: "SupervisorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_EmpID",
                table: "Task",
                column: "EmpID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_OrderID",
                table: "Task",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_StageID",
                table: "Task",
                column: "StageID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_SupervisorID",
                table: "Task",
                column: "SupervisorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");
        }
    }
}
