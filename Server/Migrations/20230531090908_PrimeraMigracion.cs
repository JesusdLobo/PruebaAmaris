using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaAmaris.Server.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false),
                    CustomerID = table.Column<string>(nullable: true),
                    Freight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShipCountry = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                });

            migrationBuilder.AlterColumn<int>(
      name: "OrderID",
      table: "Orders",
      nullable: false)
      .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
