using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairRequest.Percistance.Migrations
{
    /// <inheritdoc />
    public partial class RequestServiceComponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequestComponent_Components_ComponentId",
                table: "RepairRequestComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequestComponent_Requests_RepairRequestId",
                table: "RepairRequestComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequestService_Requests_RepairRequestId",
                table: "RepairRequestService");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequestService_Services_ServiceId",
                table: "RepairRequestService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairRequestService",
                table: "RepairRequestService");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequestService_RepairRequestId",
                table: "RepairRequestService");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequestService_ServiceId",
                table: "RepairRequestService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairRequestComponent",
                table: "RepairRequestComponent");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequestComponent_ComponentId",
                table: "RepairRequestComponent");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequestComponent_RepairRequestId",
                table: "RepairRequestComponent");

            migrationBuilder.RenameTable(
                name: "RepairRequestService",
                newName: "RepairRequestsService");

            migrationBuilder.RenameTable(
                name: "RepairRequestComponent",
                newName: "RepairRequestsComponent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairRequestsService",
                table: "RepairRequestsService",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairRequestsComponent",
                table: "RepairRequestsComponent",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairRequestsService",
                table: "RepairRequestsService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairRequestsComponent",
                table: "RepairRequestsComponent");

            migrationBuilder.RenameTable(
                name: "RepairRequestsService",
                newName: "RepairRequestService");

            migrationBuilder.RenameTable(
                name: "RepairRequestsComponent",
                newName: "RepairRequestComponent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairRequestService",
                table: "RepairRequestService",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairRequestComponent",
                table: "RepairRequestComponent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequestService_RepairRequestId",
                table: "RepairRequestService",
                column: "RepairRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequestService_ServiceId",
                table: "RepairRequestService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequestComponent_ComponentId",
                table: "RepairRequestComponent",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequestComponent_RepairRequestId",
                table: "RepairRequestComponent",
                column: "RepairRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequestComponent_Components_ComponentId",
                table: "RepairRequestComponent",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequestComponent_Requests_RepairRequestId",
                table: "RepairRequestComponent",
                column: "RepairRequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequestService_Requests_RepairRequestId",
                table: "RepairRequestService",
                column: "RepairRequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequestService_Services_ServiceId",
                table: "RepairRequestService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
