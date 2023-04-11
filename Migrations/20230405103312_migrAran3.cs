using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsureityAPI.Migrations
{
    /// <inheritdoc />
    public partial class migrAran3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Businesses_BusinessId",
                table: "Policies");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Policies",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Policies_BusinessId",
                table: "Policies",
                newName: "IX_Policies_PropertyId");

            migrationBuilder.AddColumn<bool>(
                name: "IsInsured",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "AssuredSum",
                table: "PoliciesMaster",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BusinessPenalty",
                table: "PoliciesMaster",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PropertyPenalty",
                table: "PoliciesMaster",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PropertyType",
                table: "PoliciesMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "Policies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuoteId",
                table: "Policies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    QuoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsumerId = table.Column<int>(type: "int", nullable: false),
                    BusinessID = table.Column<int>(type: "int", nullable: false),
                    QuoteAmount = table.Column<double>(type: "float", nullable: false),
                    Tenure = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.QuoteId);
                    table.ForeignKey(
                        name: "FK_Quotes_Businesses_BusinessID",
                        column: x => x.BusinessID,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Policies_AgentId",
                table: "Policies",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_QuoteId",
                table: "Policies",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_BusinessID",
                table: "Quotes",
                column: "BusinessID");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_LoginDetailsList_AgentId",
                table: "Policies",
                column: "AgentId",
                principalTable: "LoginDetailsList",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Properties_PropertyId",
                table: "Policies",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Quotes_QuoteId",
                table: "Policies",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "QuoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_LoginDetailsList_AgentId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Properties_PropertyId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Quotes_QuoteId",
                table: "Policies");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Policies_AgentId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_QuoteId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "IsInsured",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "AssuredSum",
                table: "PoliciesMaster");

            migrationBuilder.DropColumn(
                name: "BusinessPenalty",
                table: "PoliciesMaster");

            migrationBuilder.DropColumn(
                name: "PropertyPenalty",
                table: "PoliciesMaster");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                table: "PoliciesMaster");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "QuoteId",
                table: "Policies");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "Policies",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Policies_PropertyId",
                table: "Policies",
                newName: "IX_Policies_BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Businesses_BusinessId",
                table: "Policies",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "BusinessId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
