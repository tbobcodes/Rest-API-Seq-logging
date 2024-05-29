using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    public partial class seededDefaultUserandRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d169a3f-ba5f-4336-81ce-e145df98a117", "693f4a3d-54e5-4800-b9aa-e587bb6e57ac", "Administrator", "ADMINISTRATOR" },
                    { "d5ec26c2-b669-447b-9a8b-87adf91ec0b6", "abd46ecf-7776-4507-8837-f190e590fe03", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "47e3e214-13b4-4a99-b777-1e707c4889b5", 0, "8c60916b-0555-45b3-9181-d25c40d9110f", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEBWAX4QGHPdtTxxOKx49X+jmIsuMyfX4xjiAbGWDhOfxWJ+EzBtMbFErfQZacz60FA==", null, false, "caf5a176-6697-44fb-8116-68dc24dc599e", false, "admin@bookstore.com" },
                    { "89fa0c65-f78c-4b4e-a635-4b626fa007cc", 0, "7a7f7d28-d671-4031-a725-908530dcff3a", "user@bookstore.com", false, "User", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEOnIFt2NtQquUo+xY/VKoz4Y7ijLIfdHhYZqJWa1RHk0fniRIAhVwm829YEJx+PPwg==", null, false, "096c37e1-9d1c-4192-803d-91f54d7a9c07", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2d169a3f-ba5f-4336-81ce-e145df98a117", "47e3e214-13b4-4a99-b777-1e707c4889b5" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d5ec26c2-b669-447b-9a8b-87adf91ec0b6", "89fa0c65-f78c-4b4e-a635-4b626fa007cc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2d169a3f-ba5f-4336-81ce-e145df98a117", "47e3e214-13b4-4a99-b777-1e707c4889b5" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d5ec26c2-b669-447b-9a8b-87adf91ec0b6", "89fa0c65-f78c-4b4e-a635-4b626fa007cc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d169a3f-ba5f-4336-81ce-e145df98a117");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5ec26c2-b669-447b-9a8b-87adf91ec0b6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "47e3e214-13b4-4a99-b777-1e707c4889b5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89fa0c65-f78c-4b4e-a635-4b626fa007cc");
        }
    }
}
