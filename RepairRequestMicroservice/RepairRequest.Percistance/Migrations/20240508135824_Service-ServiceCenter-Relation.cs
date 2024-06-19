using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairRequest.Percistance.Migrations
{
    /// <inheritdoc />
    public partial class ServiceServiceCenterRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceCenterId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceCenterId",
                table: "Services",
                column: "ServiceCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServicesCenters_ServiceCenterId",
                table: "Services",
                column: "ServiceCenterId",
                principalTable: "ServicesCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServicesCenters_ServiceCenterId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceCenterId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceCenterId",
                table: "Services");
        }
    }
}
