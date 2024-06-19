using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairRequest.Percistance.Migrations
{
    /// <inheritdoc />
    public partial class servicecentermasterrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MasterId",
                table: "ServicesCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesCenters_MasterId",
                table: "ServicesCenters",
                column: "MasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesCenters_Users_MasterId",
                table: "ServicesCenters",
                column: "MasterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesCenters_Users_MasterId",
                table: "ServicesCenters");

            migrationBuilder.DropIndex(
                name: "IX_ServicesCenters_MasterId",
                table: "ServicesCenters");

            migrationBuilder.DropColumn(
                name: "MasterId",
                table: "ServicesCenters");
        }
    }
}
