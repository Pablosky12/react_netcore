using Microsoft.EntityFrameworkCore.Migrations;

namespace angular_netcore.Migrations
{
    public partial class SeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Chevrolet')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Ford')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Tesla')");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelA', (SELECT ID FROM  Makes WHERE Name = 'Chevrolet'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelB', (SELECT ID FROM  Makes WHERE Name = 'Chevrolet'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-Modelc', (SELECT ID FROM  Makes WHERE Name = 'Chevrolet'))");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModelA', (SELECT ID FROM  Makes WHERE Name = 'Ford'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModelB', (SELECT ID FROM  Makes WHERE Name = 'Ford'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModelC', (SELECT ID FROM  Makes WHERE Name = 'Ford'))");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make3-ModelA', (SELECT ID FROM  Makes WHERE Name = 'Tesla'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make3-ModelB', (SELECT ID FROM  Makes WHERE Name = 'Tesla'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make3-Modelc', (SELECT ID FROM  Makes WHERE Name = 'Tesla'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes WHERE Name IN ('Chevrolet', 'Ford', 'Tesla')");
        }
    }
}
