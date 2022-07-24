using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.API.Migrations
{
    public partial class DeleteNotice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notices");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2093BF77-ED2E-453B-911F-325BC99C4504",
                column: "ConcurrencyStamp",
                value: "b1be4e47-90ac-4157-97d8-2c2fc344aa71");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77967ECF-0A18-4993-A243-FDCC86F7EC1B",
                column: "ConcurrencyStamp",
                value: "4a4c73d7-0478-4d11-bc58-6cbd45305e99");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BCC4BFC0-6252-4A32-B5B8-9005E4560402",
                column: "ConcurrencyStamp",
                value: "4268e7f8-977f-4c0d-94cd-4305b7584296");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a24107-d279-4e37-96fd-01af5b38cb27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec5228b9-dc98-4bdb-86d4-99e25038d882", "AQAAAAEAACcQAAAAEIxhAGL6amcmQ0tONwvdxQN4V9Jg9wqWwCsmBcOJepRpSuaSboQBZRBt31FV5kU6UA==", "ab518102-3e14-47d0-89bc-c02529f112b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32A61BDD-DF3C-4B71-9444-F5730F8A619B",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b90b13c-9ffd-416d-a7af-87d0333c440f", "AQAAAAEAACcQAAAAEEQesAUeIWnSy2YDegjDZ+G8/oOhypAVkWURlyawaJRTbGh8CFuf4azh418W1+1HSg==", "bb9ef7ec-eba4-46b7-8a12-7af45d937949" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e448afa-f008-446e-a52f-13c449803c2e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cbf18c05-4586-440e-9299-0be1836b60e3", "AQAAAAEAACcQAAAAECtniVHesAKLoyO/1mqWAU9rfp2THCjPSJYEYWBaLmg6KmH0f3sraTZCrq5mya8ZWg==", "262160e6-d381-497b-82cc-6d4cd0390e59" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2093BF77-ED2E-453B-911F-325BC99C4504",
                column: "ConcurrencyStamp",
                value: "94bb1607-38fd-4b45-95bf-de6cc67045cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77967ECF-0A18-4993-A243-FDCC86F7EC1B",
                column: "ConcurrencyStamp",
                value: "d15d0ac3-633c-440b-a960-0d3a96135a43");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BCC4BFC0-6252-4A32-B5B8-9005E4560402",
                column: "ConcurrencyStamp",
                value: "ad0e9ae0-d7bc-4596-9764-07bb4c7ff336");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a24107-d279-4e37-96fd-01af5b38cb27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b1bc868-a994-4650-aa81-5f7aaa2128c5", "AQAAAAEAACcQAAAAEJIUEEw6ww84ayyxy3+xOD7Z/VGYj1HVKPeigvt2ZMv49cGReFdGxFyCf34Ve9xpog==", "a1e0fd4b-5393-442d-ab8d-d59edcb293ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32A61BDD-DF3C-4B71-9444-F5730F8A619B",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b66879a-6cbb-4b57-a035-9c648d7bd9cf", "AQAAAAEAACcQAAAAENI2pEX+8Yqi71b3Wch1ag2LRrTWmAXx8h86F4wPIebehG8VonhaiQPFc8iVnBJZAA==", "bdbb1738-3b7c-4abc-bc54-7779477bb389" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e448afa-f008-446e-a52f-13c449803c2e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99c2edb6-5feb-49d2-8c51-f9970b07897f", "AQAAAAEAACcQAAAAEOw7paXarbthziS4/TOMRaezw338l/o4LdvgZ5KGGDpM0O5A4oadX+aMnU3QEsx8xg==", "ea8ba8c9-4cff-495a-8d58-4910335478c7" });
        }
    }
}
