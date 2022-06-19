using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.API.Migrations
{
    public partial class UpdateProcesser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Processer",
                table: "LendBookRecords",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2093BF77-ED2E-453B-911F-325BC99C4504",
                column: "ConcurrencyStamp",
                value: "7dbc1442-3f01-49c6-a670-92ae973eb09d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77967ECF-0A18-4993-A243-FDCC86F7EC1B",
                column: "ConcurrencyStamp",
                value: "c6eb3269-b4df-42ba-8f6b-47cc7a2aa00a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BCC4BFC0-6252-4A32-B5B8-9005E4560402",
                column: "ConcurrencyStamp",
                value: "c2d1be71-e893-4e9a-a17e-2b54dec0238a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a24107-d279-4e37-96fd-01af5b38cb27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f99f9077-b08b-4cf0-8b7f-c05a4268499e", "AQAAAAEAACcQAAAAED8p+jUDA8/rymAjzQw4MRkjnUKd0HXLNogBXIb5KQs7nKCRbSHImPUWOcpKlcXw+g==", "57730a98-ae55-4246-b579-dc969cd381da" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32A61BDD-DF3C-4B71-9444-F5730F8A619B",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14cb59f9-058c-47c9-abd5-e2601fd6ad8c", "AQAAAAEAACcQAAAAEKqu83wrnTpiWkPRywrgnpID4TSaWvFotiXPNHtjMVFu/br7vcyLeSv0GGE9USeKoA==", "9d87c70f-1fe3-4aea-986b-c8185e6b8fbf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e448afa-f008-446e-a52f-13c449803c2e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14c3106a-e54d-4f56-8fec-d5bbcb8c6b71", "AQAAAAEAACcQAAAAEJ/fhejtzisij/f07y9AWMbpBZ62bop2ydZGjBtxfV1Dms4XLIm6zKgKE54qhsQ31Q==", "d6e37a61-05b5-4fe9-9de7-02557f319065" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Processer",
                table: "LendBookRecords",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
    }
}
