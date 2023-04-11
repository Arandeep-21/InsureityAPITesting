using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsureityAPI.Migrations
{
    /// <inheritdoc />
    public partial class migrAran1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessMaster",
                columns: table => new
                {
                    BMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessMaster", x => x.BMId);
                });

            migrationBuilder.CreateTable(
                name: "LoginDetailsList",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginDetailsList", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PoliciesMaster",
                columns: table => new
                {
                    PlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePremium = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliciesMaster", x => x.PlId);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMaster",
                columns: table => new
                {
                    PMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMaster", x => x.PMId);
                });

            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    ConsumerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsumerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsumerEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PAN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.ConsumerId);
                    table.ForeignKey(
                        name: "FK_Consumers_LoginDetailsList_UserId",
                        column: x => x.UserId,
                        principalTable: "LoginDetailsList",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    BusinessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessTurnover = table.Column<double>(type: "float", nullable: false),
                    CapitalInvested = table.Column<double>(type: "float", nullable: false),
                    TotalEmployees = table.Column<int>(type: "int", nullable: false),
                    ROI = table.Column<double>(type: "float", nullable: true),
                    BusinessScore = table.Column<int>(type: "int", nullable: true),
                    ConsumerId = table.Column<int>(type: "int", nullable: false),
                    BMId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.BusinessId);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinessMaster_BMId",
                        column: x => x.BMId,
                        principalTable: "BusinessMaster",
                        principalColumn: "BMId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Businesses_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssuredSum = table.Column<double>(type: "float", nullable: false),
                    PremiumRate = table.Column<double>(type: "float", nullable: false),
                    PremiumAmount = table.Column<double>(type: "float", nullable: false),
                    PolicyStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlId = table.Column<int>(type: "int", nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyId);
                    table.ForeignKey(
                        name: "FK_Policies_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Policies_PoliciesMaster_PlId",
                        column: x => x.PlId,
                        principalTable: "PoliciesMaster",
                        principalColumn: "PlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyAge = table.Column<int>(type: "int", nullable: false),
                    OwnershipType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetCost = table.Column<double>(type: "float", nullable: false),
                    SalvageValue = table.Column<double>(type: "float", nullable: false),
                    UsefulLife = table.Column<int>(type: "int", nullable: false),
                    DepreciationExpense = table.Column<double>(type: "float", nullable: true),
                    PropertyScore = table.Column<int>(type: "int", nullable: false),
                    BusinessID = table.Column<int>(type: "int", nullable: false),
                    PMId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_Businesses_BusinessID",
                        column: x => x.BusinessID,
                        principalTable: "Businesses",
                        principalColumn: "BusinessId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyMaster_PMId",
                        column: x => x.PMId,
                        principalTable: "PropertyMaster",
                        principalColumn: "PMId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BMId",
                table: "Businesses",
                column: "BMId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessName",
                table: "Businesses",
                column: "BusinessName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_ConsumerId",
                table: "Businesses",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ConsumerEmail",
                table: "Consumers",
                column: "ConsumerEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_PAN",
                table: "Consumers",
                column: "PAN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_UserId",
                table: "Consumers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginDetailsList_UserEmail",
                table: "LoginDetailsList",
                column: "UserEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policies_BusinessId",
                table: "Policies",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_PlId",
                table: "Policies",
                column: "PlId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_BusinessID",
                table: "Properties",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PMId",
                table: "Properties",
                column: "PMId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "PoliciesMaster");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "PropertyMaster");

            migrationBuilder.DropTable(
                name: "BusinessMaster");

            migrationBuilder.DropTable(
                name: "Consumers");

            migrationBuilder.DropTable(
                name: "LoginDetailsList");
        }
    }
}
