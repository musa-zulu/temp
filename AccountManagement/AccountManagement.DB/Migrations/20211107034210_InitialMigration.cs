using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AccountManagement.DB.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    surname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    id_number = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    person_code = table.Column<int>(type: "int", nullable: false),
                    account_number = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    outstanding_balance = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.code);
                    table.ForeignKey(
                        name: "FK_Account_Person",
                        column: x => x.person_code,
                        principalTable: "Persons",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountStatus",
                columns: table => new
                {
                    code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    account_code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatus", x => x.code);
                    table.ForeignKey(
                        name: "FK_AccountStatus_Accounts",
                        column: x => x.account_code,
                        principalTable: "Accounts",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_code = table.Column<int>(type: "int", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    capture_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.code);
                    table.ForeignKey(
                        name: "FK_Transaction_Account",
                        column: x => x.account_code,
                        principalTable: "Accounts",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_num",
                table: "Accounts",
                column: "account_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_person_code",
                table: "Accounts",
                column: "person_code");

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatus_account_code",
                table: "AccountStatus",
                column: "account_code");

            migrationBuilder.CreateIndex(
                name: "IX_Person_id",
                table: "Persons",
                column: "id_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_account_code",
                table: "Transactions",
                column: "account_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountStatus");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}