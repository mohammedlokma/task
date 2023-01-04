using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace task_api.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartementId = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employess_Departements_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departements",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Dep1" });

            migrationBuilder.InsertData(
                table: "Employess",
                columns: new[] { "Id", "Address", "DepartementId", "Name", "PhoneNumber", "password", "username" },
                values: new object[,]
                {
                    { 1, "1St", 1, "mohamed", "0123456789", "123456", "mohamed_123" },
                    { 2, "1St", 1, "hesham", "0123456789", "123456", "hesham_123" },
                    { 3, "1St", 1, "ebrahim", "0123456789", "123456", "ebrahim_123" },
                    { 4, "1St", 1, "mustafa", "0123456789", "123456", "mustafa_123" },
                    { 5, "1St", 1, "ali", "0123456789", "123456", "ali_123" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employess_DepartementId",
                table: "Employess",
                column: "DepartementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employess");

            migrationBuilder.DropTable(
                name: "Departements");
        }
    }
}
