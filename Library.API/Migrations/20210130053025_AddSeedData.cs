using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.API.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthData", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("e4a08daf-1ff5-40b9-b4ca-f1ed5ac868bf"), new DateTimeOffset(new DateTime(1993, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "上海", "author1@e.com", "author1" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthData", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("a6053251-d1c6-4b9c-86be-a2fe8124a80f"), new DateTimeOffset(new DateTime(1994, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "北京", "author2@e.com", "author2" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthData", "BirthPlace", "Email", "Name" },
                values: new object[] { new Guid("058ddac3-c6f5-42c2-a8ec-4a8000802990"), new DateTimeOffset(new DateTime(1995, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "成都", "author3@e.com", "author3" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Pages", "Title" },
                values: new object[] { new Guid("058ddac3-c6f5-42c2-a8ec-4a8000802990"), new Guid("e4a08daf-1ff5-40b9-b4ca-f1ed5ac868bf"), "book1 Description", 100, "book1 Title" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Pages", "Title" },
                values: new object[] { new Guid("d64263c7-a382-482c-b773-ba74caa8f4a1"), new Guid("a6053251-d1c6-4b9c-86be-a2fe8124a80f"), "book2 Description", 100, "book2 Title" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Pages", "Title" },
                values: new object[] { new Guid("7c75c7c4-1268-4d60-999b-686b08d3b269"), new Guid("058ddac3-c6f5-42c2-a8ec-4a8000802990"), "book3 Description", 100, "book3 Title" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("058ddac3-c6f5-42c2-a8ec-4a8000802990"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7c75c7c4-1268-4d60-999b-686b08d3b269"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d64263c7-a382-482c-b773-ba74caa8f4a1"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("058ddac3-c6f5-42c2-a8ec-4a8000802990"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("a6053251-d1c6-4b9c-86be-a2fe8124a80f"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("e4a08daf-1ff5-40b9-b4ca-f1ed5ac868bf"));
        }
    }
}
