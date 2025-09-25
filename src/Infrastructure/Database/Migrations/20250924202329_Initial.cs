using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mottu");

            migrationBuilder.CreateTable(
                name: "Deliveries",
                schema: "mottu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cnpj = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CnhNumber = table.Column<string>(type: "text", nullable: false),
                    CnhType = table.Column<string>(type: "text", nullable: false),
                    CnhImage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                schema: "mottu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Plate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalPlans",
                schema: "mottu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DurationInDays = table.Column<int>(type: "integer", nullable: false),
                    DailyCost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalSubscriptions",
                schema: "mottu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uuid", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentalPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DailyCost = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalSubscriptions_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalSchema: "mottu",
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentalSubscriptions_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalSchema: "mottu",
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentalSubscriptions_RentalPlans_RentalPlanId",
                        column: x => x.RentalPlanId,
                        principalSchema: "mottu",
                        principalTable: "RentalPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalSubscriptions_DeliveryId",
                schema: "mottu",
                table: "RentalSubscriptions",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalSubscriptions_MotorcycleId",
                schema: "mottu",
                table: "RentalSubscriptions",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalSubscriptions_RentalPlanId",
                schema: "mottu",
                table: "RentalSubscriptions",
                column: "RentalPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalSubscriptions",
                schema: "mottu");

            migrationBuilder.DropTable(
                name: "Deliveries",
                schema: "mottu");

            migrationBuilder.DropTable(
                name: "Motorcycles",
                schema: "mottu");

            migrationBuilder.DropTable(
                name: "RentalPlans",
                schema: "mottu");
        }
    }
}
