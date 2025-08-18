using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PaymentApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MidtransCredentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ClientKey = table.Column<string>(type: "text", nullable: false),
                    ServerKey = table.Column<string>(type: "text", nullable: false),
                    EndPointUrl = table.Column<string>(type: "text", nullable: false),
                    CallBackToken = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidtransCredentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MidtransRequestLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidtransRequestLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentLinkCharges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MidtransCredentialId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    ChargeAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CallBackUrl = table.Column<string>(type: "text", nullable: false),
                    PaymentUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLinkCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentLinkCharges_MidtransCredentials_MidtransCredentialId",
                        column: x => x.MidtransCredentialId,
                        principalTable: "MidtransCredentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MidtransResponseLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MidtransRequestLogId = table.Column<long>(type: "bigint", nullable: false),
                    Header = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidtransResponseLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MidtransResponseLogs_MidtransRequestLogs_MidtransRequestLog~",
                        column: x => x.MidtransRequestLogId,
                        principalTable: "MidtransRequestLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MidtransResponseLogs_MidtransRequestLogId",
                table: "MidtransResponseLogs",
                column: "MidtransRequestLogId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentLinkCharges_MidtransCredentialId",
                table: "PaymentLinkCharges",
                column: "MidtransCredentialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MidtransResponseLogs");

            migrationBuilder.DropTable(
                name: "PaymentLinkCharges");

            migrationBuilder.DropTable(
                name: "MidtransRequestLogs");

            migrationBuilder.DropTable(
                name: "MidtransCredentials");
        }
    }
}
