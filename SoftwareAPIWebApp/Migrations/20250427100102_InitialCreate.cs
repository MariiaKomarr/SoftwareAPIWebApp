using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareAPIWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dean = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyId);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsageTerms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Annotation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.SoftwareId);
                    table.ForeignKey(
                        name: "FK_Softwares_SoftwareTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SoftwareTypes",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Installations",
                columns: table => new
                {
                    InstallId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstallDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installations", x => x.InstallId);
                    table.ForeignKey(
                        name: "FK_Installations_Softwares_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Installations_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Installations_SoftwareId",
                table: "Installations",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_StudentId",
                table: "Installations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_TypeId",
                table: "Softwares",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installations");

            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "SoftwareTypes");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
