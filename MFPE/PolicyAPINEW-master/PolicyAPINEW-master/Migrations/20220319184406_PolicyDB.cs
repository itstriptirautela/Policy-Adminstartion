using Microsoft.EntityFrameworkCore.Migrations;

namespace PolicyAPI.Migrations
{
    public partial class PolicyDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "policies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssuredSum = table.Column<long>(type: "bigint", nullable: false),
                    Tenure = table.Column<int>(type: "int", nullable: false),
                    BusinessValue = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<int>(type: "int", nullable: false),
                    BaseLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlolicyType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "consumerPolicies",
                columns: table => new
                {
                    PolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    QuoteValue = table.Column<int>(type: "int", nullable: false),
                    PolicyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolicyMasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumerPolicies", x => x.PolicyId);
                    table.ForeignKey(
                        name: "FK_consumerPolicies_policies_PolicyMasterId",
                        column: x => x.PolicyMasterId,
                        principalTable: "policies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_consumerPolicies_PolicyMasterId",
                table: "consumerPolicies",
                column: "PolicyMasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consumerPolicies");

            migrationBuilder.DropTable(
                name: "policies");
        }
    }
}
