using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentService.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountStatuses",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountApproved = table.Column<bool>(type: "bit", nullable: false),
                    BankApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatuses", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AccountStatuses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormSubmitteds",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdentityFormSubmit = table.Column<bool>(type: "bit", nullable: false),
                    BankFormSubmit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormSubmitteds", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_FormSubmitteds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "varchar(60)", nullable: true),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAdditionalInfos",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdditionalEmail = table.Column<string>(type: "varchar(30)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: true),
                    Country = table.Column<string>(type: "varchar(30)", nullable: true),
                    State = table.Column<string>(type: "varchar(50)", nullable: true),
                    City = table.Column<string>(type: "varchar(50)", nullable: true),
                    CountryCode = table.Column<string>(type: "varchar(6)", nullable: true),
                    Zip = table.Column<string>(type: "varchar(20)", nullable: true),
                    StreetAddress = table.Column<string>(type: "text", nullable: true),
                    HouseNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    ProfileImage = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdditionalInfos", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserAdditionalInfos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBankDetails",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankName = table.Column<string>(type: "varchar(100)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    BankSwiftCode = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBankDetails", x => x.AppUserId);
                    table.ForeignKey(
                        name: "FK_UserBankDetails_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "varchar(100)", nullable: true),
                    Seen = table.Column<bool>(type: "bit", nullable: false),
                    UserType = table.Column<string>(type: "varchar(10)", nullable: true),
                    CreatetAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    DocType = table.Column<string>(type: "varchar(20)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotifications_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VerifyAppusers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "varchar(100)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyAppusers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerifyAppusers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e900", null, "SuperAdmin", "SUPERADMIN" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e901", null, "User", "USER" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e909", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Deleted", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e575", 0, "3c9e06d7-814c-41d4-b881-829bb8894509", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "admin@dotnet.project", true, "Super", "User", false, null, "ADMIN@DOTNET.PROJECT", "ADMIN@DOTNET.PROJECT", "AQAAAAEAACcQAAAAEJ19lcpUMdhBcvdxl8qTASubgNoRqS98NhjMsgmRZLvf0gC6fvVEf+Kx82yt6HA46Q==", null, true, "ea2860fe-ea66-4782-8547-0fd03772a863", false, "admin@dotnet.project" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e571", 0, "14b02f68-8d0a-4308-bc73-d0935d9048c9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "demouser@dotnet.project", true, "Demo", "User", false, null, "DEMOUSER@DOTNET.PROJECT", "DEMOUSER@DOTNET.PROJECT", "AQAAAAEAACcQAAAAEAmn76sUvGPXCaRv/70o10o7p0mCBrGtgC1cTVTdndYkNKzM3cwAy3QfS6/S1xfKEw==", null, true, "ad7cb3d8-80f0-4f4b-b912-459983d2a6c1", false, "demouser@dotnet.project" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e579", 0, "ee7789e5-6f1c-42ea-9d6d-b0c98292975e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "demoadmin@dotnet.project", true, "Demo", "Admin", false, null, "DEMOADMIN@DOTNET.PROJECT", "DEMOADMIN@DOTNET.PROJECT", "AQAAAAEAACcQAAAAEHLl5Gu57A+8UvZtuzkSGnuuAxXGMtGbNCo0Neja9hqybF3bUdtwFu/H7MFTkQLl3Q==", null, true, "4b8da353-6bf6-4e66-a338-2513537e3fb3", false, "demoadmin@dotnet.project" }
                });

            migrationBuilder.InsertData(
                table: "AccountStatuses",
                columns: new[] { "UserId", "AccountApproved", "BankApproved" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e575", false, false },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e571", false, false },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e579", false, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e900", "a18be9c0-aa65-4af8-bd17-00bd9450e575" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e901", "a18be9c0-aa65-4af8-bd17-00bd9450e571" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e909", "a18be9c0-aa65-4af8-bd17-00bd9450e579" }
                });

            migrationBuilder.InsertData(
                table: "FormSubmitteds",
                columns: new[] { "UserId", "BankFormSubmit", "IdentityFormSubmit" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e575", false, false },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e571", false, false },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e579", false, false }
                });

            migrationBuilder.InsertData(
                table: "UserAdditionalInfos",
                columns: new[] { "UserId", "AdditionalEmail", "City", "Country", "CountryCode", "HouseNumber", "PhoneNumber", "ProfileImage", "State", "StreetAddress", "Zip" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e575", null, null, null, null, null, null, null, null, null, null },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e571", null, null, null, null, null, null, null, null, null, null },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e579", null, null, null, null, null, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "UserBankDetails",
                columns: new[] { "AppUserId", "BankAccountNumber", "BankName", "BankSwiftCode" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e575", null, null, null },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e571", null, null, null },
                    { "a18be9c0-aa65-4af8-bd17-00bd9450e579", null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_ApplicationUserId",
                table: "UserNotifications",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VerifyAppusers_UserId",
                table: "VerifyAppusers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountStatuses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FormSubmitteds");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "UserAdditionalInfos");

            migrationBuilder.DropTable(
                name: "UserBankDetails");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "VerifyAppusers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
