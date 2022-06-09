using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.API.Migrations
{
    public partial class MidiffySeedDataAndEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Books",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Authors",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2093BF77-ED2E-453B-911F-325BC99C4504", "f582d9d7-c21a-4e4d-9abf-161cc48e51e1", "Administrator", "ADMINISTRATOR" },
                    { "77967ECF-0A18-4993-A243-FDCC86F7EC1B", "3a3f6f95-309c-4c05-b445-0e93b2f39694", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "30a24107-d279-4e37-96fd-01af5b38cb27", 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "3e5346a0-fdc2-4b02-8b87-58b0657918ca", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEKmGByJeUpd/7xQnf8a+vYJXjQTxzB5QwpHp6/bKIhbHCDHDIpqjDoS+wM9qqznuLA==", null, false, "9a81a063-b033-4548-8fbc-ef2233a92241", false, "user@bookstore.com" },
                    { "8e448afa-f008-446e-a52f-13c449803c2e", 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "285d3c72-d605-4656-b95a-c4fbb2adff24", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEKgIYZ7zW4Skau9oshxm7jgLw59nW2QuM3fBitX2CkGnmteGPgLBc/zzsbiyc27fdw==", null, false, "e6856b78-f84c-45cc-bc97-0b912fe0f3d6", false, "admin@bookstore.com" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("058ddac3-c6f5-42c2-a8ec-4a8000802990"),
                columns: new[] { "ISBN", "Summary" },
                values: new object[] { "12345667", "book1 Description" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7c75c7c4-1268-4d60-999b-686b08d3b269"),
                column: "Summary",
                value: "book3 Description");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d64263c7-a382-482c-b773-ba74caa8f4a1"),
                columns: new[] { "ISBN", "Summary" },
                values: new object[] { "12384298", "book2 Description" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "77967ECF-0A18-4993-A243-FDCC86F7EC1B", "30a24107-d279-4e37-96fd-01af5b38cb27" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2093BF77-ED2E-453B-911F-325BC99C4504", "8e448afa-f008-446e-a52f-13c449803c2e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "77967ECF-0A18-4993-A243-FDCC86F7EC1B", "30a24107-d279-4e37-96fd-01af5b38cb27" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2093BF77-ED2E-453B-911F-325BC99C4504", "8e448afa-f008-446e-a52f-13c449803c2e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2093BF77-ED2E-453B-911F-325BC99C4504");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77967ECF-0A18-4993-A243-FDCC86F7EC1B");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a24107-d279-4e37-96fd-01af5b38cb27");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e448afa-f008-446e-a52f-13c449803c2e");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("058ddac3-c6f5-42c2-a8ec-4a8000802990"),
                column: "Description",
                value: "book1 Description");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7c75c7c4-1268-4d60-999b-686b08d3b269"),
                column: "Description",
                value: "book3 Description");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d64263c7-a382-482c-b773-ba74caa8f4a1"),
                column: "Description",
                value: "book2 Description");
        }
    }
}
