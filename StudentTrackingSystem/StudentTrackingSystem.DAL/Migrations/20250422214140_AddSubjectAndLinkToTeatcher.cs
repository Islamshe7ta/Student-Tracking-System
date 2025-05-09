﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectAndLinkToTeatcher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Teatchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teatchers_SubjectId",
                table: "Teatchers",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teatchers_Subjects_SubjectId",
                table: "Teatchers",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teatchers_Subjects_SubjectId",
                table: "Teatchers");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Teatchers_SubjectId",
                table: "Teatchers");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Teatchers");
        }
    }
}
