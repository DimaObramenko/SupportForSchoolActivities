using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportForSchoolActivities.DAL.Migrations
{
    public partial class AddRemarkEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "233cce72-ad80-49b7-94b1-bd2f71dfa3cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ce29bb2-c6db-4b4c-8d8d-a3dd6c797fb8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77a9b96b-8248-444b-ba00-0ae86670d563");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e60f25a4-4cb3-4dca-87e0-1ca8abd5e549");

            migrationBuilder.CreateTable(
                name: "Remark",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remark_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "393188ff-5593-495b-b339-562fba309748", "a078301a-0eb3-46d1-b014-96c542b994f8", "Teacher", "TEACHER" },
                    { "5e72a373-af7f-4bf4-acfd-42048d610d4c", "6571da5c-666e-4764-a94e-25308edb27ce", "Admin", "ADMIN" },
                    { "900fc2fc-e880-435c-b4f9-05188f6ac5b4", "8fed11fb-a5d8-49cf-ad9c-83d749788555", "Student", "STUDENT" },
                    { "aac9e634-b1c7-43fb-be65-52a281740e95", "53ff0a0a-26e7-444b-8777-3516a9f47d4a", "Parent", "PARENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remark_StudentId",
                table: "Remark",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remark");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "393188ff-5593-495b-b339-562fba309748");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e72a373-af7f-4bf4-acfd-42048d610d4c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "900fc2fc-e880-435c-b4f9-05188f6ac5b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aac9e634-b1c7-43fb-be65-52a281740e95");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "233cce72-ad80-49b7-94b1-bd2f71dfa3cf", "0d0bd938-4ccc-4e4d-abca-1e84ecf867d5", "Student", "STUDENT" },
                    { "3ce29bb2-c6db-4b4c-8d8d-a3dd6c797fb8", "87e4259e-093e-44a3-bf5e-32dc0e48e4fd", "Parent", "PARENT" },
                    { "77a9b96b-8248-444b-ba00-0ae86670d563", "ce586620-24e8-4d1c-80dc-bdef539e22e5", "Admin", "ADMIN" },
                    { "e60f25a4-4cb3-4dca-87e0-1ca8abd5e549", "eacb8e9a-e867-45dc-9b4d-c5ab3164e641", "Teacher", "TEACHER" }
                });
        }
    }
}
