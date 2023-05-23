using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportForSchoolActivities.DAL.Migrations
{
    public partial class InsertedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
