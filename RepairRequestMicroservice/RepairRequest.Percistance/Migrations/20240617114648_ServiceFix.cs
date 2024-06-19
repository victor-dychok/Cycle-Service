using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairRequest.Percistance.Migrations
{
    /// <inheritdoc />
    public partial class ServiceFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_MasterId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServicesCenters_ServiceCenterId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesCenters_Users_MasterId",
                table: "ServicesCenters");

            migrationBuilder.DropIndex(
                name: "IX_ServicesCenters_MasterId",
                table: "ServicesCenters");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceCenterId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "MasterId",
                table: "ServicesCenters");

            migrationBuilder.DropColumn(
                name: "ServiceCenterId",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "MasterId",
                table: "Requests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "LeadTime",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ServiceCenterId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServiceCenterUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    ServiceCenterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCenterUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCenterUser_ServicesCenters_ServiceCenterId",
                        column: x => x.ServiceCenterId,
                        principalTable: "ServicesCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceCenterUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenterUser_ServiceCenterId",
                table: "ServiceCenterUser",
                column: "ServiceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenterUser_UserId",
                table: "ServiceCenterUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_MasterId",
                table: "Requests",
                column: "MasterId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_MasterId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "ServiceCenterUser");

            migrationBuilder.DropColumn(
                name: "LeadTime",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ServiceCenterId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "MasterId",
                table: "ServicesCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceCenterId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MasterId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesCenters_MasterId",
                table: "ServicesCenters",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceCenterId",
                table: "Services",
                column: "ServiceCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_MasterId",
                table: "Requests",
                column: "MasterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServicesCenters_ServiceCenterId",
                table: "Services",
                column: "ServiceCenterId",
                principalTable: "ServicesCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesCenters_Users_MasterId",
                table: "ServicesCenters",
                column: "MasterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
