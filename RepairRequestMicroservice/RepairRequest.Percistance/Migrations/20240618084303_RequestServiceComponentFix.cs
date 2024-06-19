using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairRequest.Percistance.Migrations
{
    /// <inheritdoc />
    public partial class RequestServiceComponentFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RepairRequestsService_RepairRequestId",
                table: "RepairRequestsService",
                column: "RepairRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequestsService_ServiceId",
                table: "RepairRequestsService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequestsComponent_ComponentId",
                table: "RepairRequestsComponent",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequestsComponent_RepairRequestId",
                table: "RepairRequestsComponent",
                column: "RepairRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequestsComponent_Components_ComponentId",
                table: "RepairRequestsComponent",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequestsComponent_Requests_RepairRequestId",
                table: "RepairRequestsComponent",
                column: "RepairRequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequestsService_Requests_RepairRequestId",
                table: "RepairRequestsService",
                column: "RepairRequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequestsService_Services_ServiceId",
                table: "RepairRequestsService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequestsComponent_Components_ComponentId",
                table: "RepairRequestsComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequestsComponent_Requests_RepairRequestId",
                table: "RepairRequestsComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequestsService_Requests_RepairRequestId",
                table: "RepairRequestsService");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequestsService_Services_ServiceId",
                table: "RepairRequestsService");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequestsService_RepairRequestId",
                table: "RepairRequestsService");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequestsService_ServiceId",
                table: "RepairRequestsService");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequestsComponent_ComponentId",
                table: "RepairRequestsComponent");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequestsComponent_RepairRequestId",
                table: "RepairRequestsComponent");
        }
    }
}
