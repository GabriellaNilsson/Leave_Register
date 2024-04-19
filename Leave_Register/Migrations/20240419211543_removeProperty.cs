using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leave_Register.Migrations
{
    /// <inheritdoc />
    public partial class removeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "selectedMonth",
                table: "LeaveRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "selectedMonth",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
