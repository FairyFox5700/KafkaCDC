using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KafkaCDC.Deals.Data.Migration
{
    public partial class Initial : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    DealType = table.Column<int>(type: "integer", nullable: true),
                    DealStatus = table.Column<int>(type: "integer", nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedTimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    InitialPriceRangeLow = table.Column<decimal>(type: "numeric", nullable: true),
                    InitialPriceRangeHigh = table.Column<decimal>(type: "numeric", nullable: true),
                    RevisedPriceRangeLow = table.Column<decimal>(type: "numeric", nullable: true),
                    RevisedPriceRangeHigh = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AggregateType = table.Column<string>(type: "text", nullable: true),
                    AggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Payload = table.Column<string>(type: "text", nullable: true),
                    DateOccurred = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.DropTable(
                name: "OutboxEvents");
        }
    }
}
