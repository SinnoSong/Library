using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.API.Migrations
{
    public partial class MidifyLendRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "LendBookRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "LendBookRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2093BF77-ED2E-453B-911F-325BC99C4504",
                column: "ConcurrencyStamp",
                value: "bafe90ff-e3b6-4bef-bdbc-2c8181b01a32");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77967ECF-0A18-4993-A243-FDCC86F7EC1B",
                column: "ConcurrencyStamp",
                value: "32e4a2a6-6adf-469b-9dd5-32bfb539b12a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BCC4BFC0-6252-4A32-B5B8-9005E4560402",
                column: "ConcurrencyStamp",
                value: "aaf62c9d-fd67-452b-b140-cf34c799ffd7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a24107-d279-4e37-96fd-01af5b38cb27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae712320-14bb-41d0-8246-b4ffa3b1dbb0", "AQAAAAEAACcQAAAAEL0ikS8IHKWggiE4JORJV6sRnM2V8wRJmoSEkACtjhwPHogcC6GT4TSr7Mk8ZCVhZg==", "b224e26f-6c2b-433a-82e5-79fb841b8a98" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32A61BDD-DF3C-4B71-9444-F5730F8A619B",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58eadebc-030a-4270-a6ac-766f47ad4ce1", "AQAAAAEAACcQAAAAEHBuZ6OKrDIWnQ5fBSfCKq5MkjBD0vN3Z2ttdIHDNtNC3LTyqFS4jp9Qu+HWZFlEUw==", "cf894cc9-874d-48d3-833d-7af02e95d827" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e448afa-f008-446e-a52f-13c449803c2e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b0aac18-820e-4620-8a51-ea1b7c16111d", "AQAAAAEAACcQAAAAEE0yS7lGIv0pAgpzhE0ImU1TJsjAzdcw7i36IVg+J/u658NpxxZjAZMj4sGawPljzA==", "de1a8e18-edb3-414f-a4f6-8c100f709c1c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LendBookRecords");

            migrationBuilder.AddColumn<decimal>(
                name: "StudentId",
                table: "LendBookRecords",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2093BF77-ED2E-453B-911F-325BC99C4504",
                column: "ConcurrencyStamp",
                value: "f732b8c6-486e-45ad-af74-9e477028080f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77967ECF-0A18-4993-A243-FDCC86F7EC1B",
                column: "ConcurrencyStamp",
                value: "f6f4b977-3e0e-45d0-9261-a00c0413a4c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BCC4BFC0-6252-4A32-B5B8-9005E4560402",
                column: "ConcurrencyStamp",
                value: "4c57bdf8-3391-4cd4-8033-0d759bd82372");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a24107-d279-4e37-96fd-01af5b38cb27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "710230d9-590a-41f8-859c-82a7adb68ac0", "AQAAAAEAACcQAAAAELbSR9VQJsVVjdhmR8xg82A4oh+XTlIw53PTIvrMQJXMnNvR5awoPTn5Y5pWSgy08g==", "de7865cc-05cd-4e25-b4ea-3bfa39ac347e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32A61BDD-DF3C-4B71-9444-F5730F8A619B",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb07aa59-dc4a-4992-afc6-f4ea01045e72", "AQAAAAEAACcQAAAAEE4z+qY0a3tzseE/mx3DXI4RUGg9phvWdi143fAj6mlKc/j5wSUvbw5mUCcf+LMmbQ==", "98c990d9-42e5-4d26-b443-7758c81a2791" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e448afa-f008-446e-a52f-13c449803c2e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edd2c020-56a9-4419-85db-2f8f83497340", "AQAAAAEAACcQAAAAEBr6gQn6yawom5qQUYgktYkIlgmnVyZrBxpH/jP/2dOlmCfTCf9Eeg3OBn5crO/otw==", "0d00579f-3a57-48d4-aef3-b367cce45ed5" });
        }
    }
}
