using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairRequest.Percistance.Migrations
{
    /// <inheritdoc />
    public partial class ServiceAdminFKFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCenterUser_ServicesCenters_ServiceCenterId",
                table: "ServiceCenterUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCenterUser_Users_UserId",
                table: "ServiceCenterUser");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCenterUser_ServiceCenterId",
                table: "ServiceCenterUser");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCenterUser_UserId",
                table: "ServiceCenterUser");

            migrationBuilder.DropColumn(
                name: "ServiceCenterId",
                table: "ServiceCenterUser");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "ServicesCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceCenterEntityId",
                table: "ServiceCenterUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesCenters_AdminId",
                table: "ServicesCenters",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenterUser_ServiceCenterEntityId",
                table: "ServiceCenterUser",
                column: "ServiceCenterEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCenterUser_ServicesCenters_ServiceCenterEntityId",
                table: "ServiceCenterUser",
                column: "ServiceCenterEntityId",
                principalTable: "ServicesCenters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesCenters_Users_AdminId",
                table: "ServicesCenters",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCenterUser_ServicesCenters_ServiceCenterEntityId",
                table: "ServiceCenterUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesCenters_Users_AdminId",
                table: "ServicesCenters");

            migrationBuilder.DropIndex(
                name: "IX_ServicesCenters_AdminId",
                table: "ServicesCenters");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCenterUser_ServiceCenterEntityId",
                table: "ServiceCenterUser");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "ServicesCenters");

            migrationBuilder.DropColumn(
                name: "ServiceCenterEntityId",
                table: "ServiceCenterUser");

            migrationBuilder.AddColumn<int>(
                name: "ServiceCenterId",
                table: "ServiceCenterUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenterUser_ServiceCenterId",
                table: "ServiceCenterUser",
                column: "ServiceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenterUser_UserId",
                table: "ServiceCenterUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCenterUser_ServicesCenters_ServiceCenterId",
                table: "ServiceCenterUser",
                column: "ServiceCenterId",
                principalTable: "ServicesCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCenterUser_Users_UserId",
                table: "ServiceCenterUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
