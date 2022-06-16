using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.API.Migrations
{
    public partial class MidifySeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2093BF77-ED2E-453B-911F-325BC99C4504",
                column: "ConcurrencyStamp",
                value: "4448fc24-cc16-403b-b84b-9cf55a27f2ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77967ECF-0A18-4993-A243-FDCC86F7EC1B",
                column: "ConcurrencyStamp",
                value: "6a998a07-4dcd-4d4e-a253-dcfbf450ff98");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BCC4BFC0-6252-4A32-B5B8-9005E4560402",
                column: "ConcurrencyStamp",
                value: "878eafa9-acd2-459a-9119-1b1be74cf204");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a24107-d279-4e37-96fd-01af5b38cb27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a81d5c9-5be6-4c52-bd26-2c9e7cb35053", "AQAAAAEAACcQAAAAEITjFTRo593ISjTyppcqpJJHezOeW9lU5q7bUVcU3xjwSxAVyxQI/bRq+ss1zGYZEQ==", "88f32663-9994-4374-9916-a68ce3ac58e0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32A61BDD-DF3C-4B71-9444-F5730F8A619B",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cdec30dd-4a7d-4143-a9af-d549e24d108a", "SUPER@LIBRARY.COM", "SUPER@LIBRARY.COM", "AQAAAAEAACcQAAAAELp0lg/b/gj3uS5E86+By1fQzsCUB/VI7KuupNjn9ENSZ0EG5iRtk7PXJ+WsYcJFAQ==", "eca57ad7-8bb6-47f0-b7ba-0a2cd8b492bf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e448afa-f008-446e-a52f-13c449803c2e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d49fc34-4cf9-43d7-912d-b7e3206b3d2f", "AQAAAAEAACcQAAAAEE6SvJJc+2bNXC92cDcaZJ7n5VKOs7/U8nxxP3LcKV3QOVGqJEZzJ3qO1pd1pUIX/g==", "df7fdc7a-20ad-4c7d-8125-9f5944f7ef8b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2093BF77-ED2E-453B-911F-325BC99C4504",
                column: "ConcurrencyStamp",
                value: "fd9decbd-e935-42f3-971c-474592252b77");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77967ECF-0A18-4993-A243-FDCC86F7EC1B",
                column: "ConcurrencyStamp",
                value: "c929e066-e8ab-468a-98e5-2793cc4b095e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BCC4BFC0-6252-4A32-B5B8-9005E4560402",
                column: "ConcurrencyStamp",
                value: "4e79ca88-1584-422b-9b67-46dc769aedad");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a24107-d279-4e37-96fd-01af5b38cb27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f9ac718-d2b2-4aa0-af82-0a0d03714dda", "AQAAAAEAACcQAAAAEOFCmrBC4oSYAOMQagAe6r5IopsgXAryteT7RjGavqCDmV161sSUVp8u2Hn6WOJEyw==", "2a3f7700-883b-4265-99c5-6cd93ef42d3b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32A61BDD-DF3C-4B71-9444-F5730F8A619B",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cda2562-2745-4ba9-8c2a-65770e745dbd", "SUPER@@LIBRARY.COM", "SUPER@@LIBRARY.COM", "AQAAAAEAACcQAAAAEAlYRxvIAFBkkPlDvql/Iot9hzX6hJ+wFt6gDxOVBHnhP5gUAw4cZJD3mmsAN8YhHQ==", "7f9fd223-debf-462b-a13e-fb42f910d1ee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e448afa-f008-446e-a52f-13c449803c2e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d1872c47-dced-40e0-a7bc-a67f1110be80", "AQAAAAEAACcQAAAAEAA0yWheiRDwWeHX+f2JWDHC+UQHplf5fCGoCmGHH4b2MK27lqBvV9ci1OF3JbcTmQ==", "eeb12188-b54b-4c91-92c0-97e4c785757c" });
        }
    }
}
