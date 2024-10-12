using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatGPTCloneInfrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByIp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    SecurityStamp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Revoked = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RevokedByIp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2798212b-3e5d-4556-8629-a64eb70da4a8"),
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "12e6c72b-c110-48e8-8f58-add5c726f019", "merve@gmail.com", "Merve", "Ekşi", "MERVE@GMAİL.COM", "MERVE", "AQAAAAIAAYagAAAAEG7cCNWZWx27kFQyYNY+BjWC96QBU6/P3/ePuHL3yM8j4YmxprMI2YzvjLAMLDT0oA==", null, "merve" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppUserId_Token",
                table: "RefreshTokens",
                columns: new[] { "AppUserId", "Token" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2798212b-3e5d-4556-8629-a64eb70da4a8"),
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "6f9b02ed-2cff-4ba6-ab04-0313333e009b", "alihan@gmail.com", "Alihan", "Güdenoğlu", "ALIHAN", "ALIHAN@GMAIL.COM", "AQAAAAIAAYagAAAAEHHFH34nvYhuLpioq8VTOon574gGOi5kJURFEEHIPvE7Pr5JrokFf4iUAgGPv7QJHQ==", "930ad93a-97ae-4472-abda-b9e0f5f43515", "alihan" });
        }
    }
}
