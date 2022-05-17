using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromotionAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Promotion",
                schema: "dbo",
                columns: table => new
                {
                    PromotionId = table.Column<string>(type: "text", nullable: false),
                    PromoType = table.Column<string>(type: "text", nullable: false),
                    ValueType = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Item = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.PromotionId);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "dbo",
                columns: table => new
                {
                    StoreId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "RelPromotionStore",
                schema: "dbo",
                columns: table => new
                {
                    PromotionId = table.Column<string>(type: "text", nullable: false),
                    StoreId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelPromotionStore", x => new { x.PromotionId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_RelPromotionStore_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalSchema: "dbo",
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelPromotionStore_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Store",
                columns: new[] { "StoreId", "Name" },
                values: new object[,]
                {
                    { "111", "Toko Jakarta" },
                    { "222", "Toko Tangerang" },
                    { "333", "Toko Bogor" },
                    { "444", "Toko Bandung" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelPromotionStore_StoreId",
                schema: "dbo",
                table: "RelPromotionStore",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelPromotionStore",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Promotion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "dbo");
        }
    }
}
