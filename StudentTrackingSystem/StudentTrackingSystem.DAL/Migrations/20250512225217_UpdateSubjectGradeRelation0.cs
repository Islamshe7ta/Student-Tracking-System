using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTrackingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubjectGradeRelation0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeatcherSubjectGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeatcherId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    TeatcherId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeatcherSubjectGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeatcherSubjectGrades_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeatcherSubjectGrades_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeatcherSubjectGrades_Teatchers_TeatcherId",
                        column: x => x.TeatcherId,
                        principalTable: "Teatchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeatcherSubjectGrades_Teatchers_TeatcherId1",
                        column: x => x.TeatcherId1,
                        principalTable: "Teatchers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeatcherSubjectGrades_GradeId",
                table: "TeatcherSubjectGrades",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TeatcherSubjectGrades_SubjectId",
                table: "TeatcherSubjectGrades",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeatcherSubjectGrades_TeatcherId",
                table: "TeatcherSubjectGrades",
                column: "TeatcherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeatcherSubjectGrades_TeatcherId1",
                table: "TeatcherSubjectGrades",
                column: "TeatcherId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeatcherSubjectGrades");
        }
    }
}
