using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace artf_MVC.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "empresa",
                columns: table => new
                {
                    idempre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    rsempre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dirempre = table.Column<string>(type: "varchar(355)", maxLength: 355, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tipoempre = table.Column<string>(type: "enum('asignatario','particular','concesionario','permisionario')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idempre);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "fabricante",
                columns: table => new
                {
                    idfab = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    rsfab = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    marfab = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idfab);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "modelo",
                columns: table => new
                {
                    idmod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    modequi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idmod);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    iduser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tipouser = table.Column<string>(type: "enum('admin','second')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nomuser = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pauser = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sauser = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mailuser = table.Column<string>(type: "tinytext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    passuser = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.iduser);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "solrf",
                columns: table => new
                {
                    idsol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idempsol = table.Column<int>(type: "int", nullable: true),
                    idusersol = table.Column<int>(type: "int", nullable: true),
                    numacuofsol = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    obssol = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecapsol = table.Column<DateTime>(type: "date", nullable: true),
                    docsol = table.Column<byte[]>(type: "longblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idsol);
                    table.ForeignKey(
                        name: "solrf_ibfk_1",
                        column: x => x.idempsol,
                        principalTable: "empresa",
                        principalColumn: "idempre");
                    table.ForeignKey(
                        name: "solrf_ibfk_2",
                        column: x => x.idusersol,
                        principalTable: "users",
                        principalColumn: "iduser");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "insrf",
                columns: table => new
                {
                    idins = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idempins = table.Column<int>(type: "int", nullable: true),
                    idsolins = table.Column<int>(type: "int", nullable: true),
                    iduserins = table.Column<int>(type: "int", nullable: true),
                    numacuofins = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    obsins = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecapins = table.Column<DateTime>(type: "date", nullable: true),
                    docins = table.Column<byte[]>(type: "blob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idins);
                    table.ForeignKey(
                        name: "insrf_ibfk_1",
                        column: x => x.idempins,
                        principalTable: "empresa",
                        principalColumn: "idempre");
                    table.ForeignKey(
                        name: "insrf_ibfk_2",
                        column: x => x.idsolins,
                        principalTable: "solrf",
                        principalColumn: "idsol");
                    table.ForeignKey(
                        name: "insrf_ibfk_3",
                        column: x => x.iduserins,
                        principalTable: "users",
                        principalColumn: "iduser");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rectrf",
                columns: table => new
                {
                    idrect = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idinsrect = table.Column<int>(type: "int", nullable: true),
                    iduserrect = table.Column<int>(type: "int", nullable: true),
                    numacuofrect = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fichatecrect = table.Column<byte[]>(type: "blob", nullable: true),
                    numdocemp = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fechadocsol = table.Column<DateTime>(type: "date", nullable: false),
                    fecharect = table.Column<DateTime>(type: "date", nullable: false),
                    desrect = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    obsrect = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    acurect = table.Column<byte[]>(type: "blob", nullable: true),
                    claverect = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fechamodrect = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idrect);
                    table.ForeignKey(
                        name: "rectrf_ibfk_1",
                        column: x => x.idinsrect,
                        principalTable: "insrf",
                        principalColumn: "idins");
                    table.ForeignKey(
                        name: "rectrf_ibfk_2",
                        column: x => x.iduserrect,
                        principalTable: "users",
                        principalColumn: "iduser");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "modrf",
                columns: table => new
                {
                    idmod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idrectmod = table.Column<int>(type: "int", nullable: true),
                    idusermod = table.Column<int>(type: "int", nullable: true),
                    numacuofmod = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    acumod = table.Column<byte[]>(type: "blob", nullable: true),
                    fechamod = table.Column<DateTime>(type: "date", nullable: false),
                    desmod = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    obsmod = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fichatecmod = table.Column<byte[]>(type: "blob", nullable: false),
                    clavemod = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idmod);
                    table.ForeignKey(
                        name: "modrf_ibfk_1",
                        column: x => x.idrectmod,
                        principalTable: "rectrf",
                        principalColumn: "idrect");
                    table.ForeignKey(
                        name: "modrf_ibfk_2",
                        column: x => x.idusermod,
                        principalTable: "users",
                        principalColumn: "iduser");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "canrf",
                columns: table => new
                {
                    idcan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idmodcan = table.Column<int>(type: "int", nullable: true),
                    idusercan = table.Column<int>(type: "int", nullable: true),
                    numacuofcan = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fechaofcan = table.Column<DateTime>(type: "date", nullable: false),
                    juscan = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    obscan = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fichacan = table.Column<byte[]>(type: "blob", nullable: false),
                    clavecan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fechacan = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idcan);
                    table.ForeignKey(
                        name: "canrf_ibfk_1",
                        column: x => x.idmodcan,
                        principalTable: "modrf",
                        principalColumn: "idmod");
                    table.ForeignKey(
                        name: "canrf_ibfk_2",
                        column: x => x.idusercan,
                        principalTable: "users",
                        principalColumn: "iduser");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "equiuni",
                columns: table => new
                {
                    idequi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idempreequi = table.Column<int>(type: "int", nullable: true),
                    idfabequi = table.Column<int>(type: "int", nullable: true),
                    idmodeequi = table.Column<int>(type: "int", nullable: true),
                    idsolequi = table.Column<int>(type: "int", nullable: true),
                    idinsequi = table.Column<int>(type: "int", nullable: true),
                    idrectequi = table.Column<int>(type: "int", nullable: true),
                    idmodequi = table.Column<int>(type: "int", nullable: true),
                    idcanequi = table.Column<int>(type: "int", nullable: true),
                    modaequi = table.Column<string>(type: "enum('Arrastre','Tractivo','Trabajo')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tipequi = table.Column<string>(type: "enum('Coche','Coche domo/comedor','Coche terraza','Coche bar','Furgon','Gondola','Plataforma','S169','Tolva','Locomotora','Locomotora-AC','Locomotora-DC','Locomotora-EVO')", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    combuequi = table.Column<string>(type: "enum('Diesel','Gasolina','Electrico')", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pequi = table.Column<int>(type: "int", nullable: true),
                    nserie = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    regiequi = table.Column<string>(type: "enum('Arrendado','Propio')", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    graequi = table.Column<string>(type: "enum('Si','No')", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usoequi = table.Column<string>(type: "enum('Carga','Pasajeros','Patio','Mixto')", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fcons = table.Column<short>(type: "year", nullable: true),
                    nofact = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tcontra = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fcontra = table.Column<DateTime>(type: "date", nullable: true),
                    vcontra = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mrent = table.Column<int>(type: "int", nullable: true),
                    monrent = table.Column<string>(type: "enum('MXN','USD')", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    obsarre = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    obsgra = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    obsequi = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fichaequi = table.Column<byte[]>(type: "blob", nullable: true),
                    fecharequi = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idequi);
                    table.ForeignKey(
                        name: "equiuni_ibfk_1",
                        column: x => x.idempreequi,
                        principalTable: "empresa",
                        principalColumn: "idempre");
                    table.ForeignKey(
                        name: "equiuni_ibfk_2",
                        column: x => x.idfabequi,
                        principalTable: "fabricante",
                        principalColumn: "idfab");
                    table.ForeignKey(
                        name: "equiuni_ibfk_3",
                        column: x => x.idmodeequi,
                        principalTable: "modelo",
                        principalColumn: "idmod");
                    table.ForeignKey(
                        name: "equiuni_ibfk_4",
                        column: x => x.idsolequi,
                        principalTable: "solrf",
                        principalColumn: "idsol");
                    table.ForeignKey(
                        name: "equiuni_ibfk_5",
                        column: x => x.idinsequi,
                        principalTable: "insrf",
                        principalColumn: "idins");
                    table.ForeignKey(
                        name: "equiuni_ibfk_6",
                        column: x => x.idrectequi,
                        principalTable: "rectrf",
                        principalColumn: "idrect");
                    table.ForeignKey(
                        name: "equiuni_ibfk_7",
                        column: x => x.idmodequi,
                        principalTable: "modrf",
                        principalColumn: "idmod");
                    table.ForeignKey(
                        name: "equiuni_ibfk_8",
                        column: x => x.idcanequi,
                        principalTable: "canrf",
                        principalColumn: "idcan");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "idmodcan",
                table: "canrf",
                column: "idmodcan");

            migrationBuilder.CreateIndex(
                name: "idusercan",
                table: "canrf",
                column: "idusercan");

            migrationBuilder.CreateIndex(
                name: "idcanequi",
                table: "equiuni",
                column: "idcanequi");

            migrationBuilder.CreateIndex(
                name: "idempreequi",
                table: "equiuni",
                column: "idempreequi");

            migrationBuilder.CreateIndex(
                name: "idfabequi",
                table: "equiuni",
                column: "idfabequi");

            migrationBuilder.CreateIndex(
                name: "idinsequi",
                table: "equiuni",
                column: "idinsequi");

            migrationBuilder.CreateIndex(
                name: "idmodeequi",
                table: "equiuni",
                column: "idmodeequi");

            migrationBuilder.CreateIndex(
                name: "idmodequi",
                table: "equiuni",
                column: "idmodequi");

            migrationBuilder.CreateIndex(
                name: "idrectequi",
                table: "equiuni",
                column: "idrectequi");

            migrationBuilder.CreateIndex(
                name: "idsolequi",
                table: "equiuni",
                column: "idsolequi");

            migrationBuilder.CreateIndex(
                name: "idempins",
                table: "insrf",
                column: "idempins");

            migrationBuilder.CreateIndex(
                name: "idsolins",
                table: "insrf",
                column: "idsolins");

            migrationBuilder.CreateIndex(
                name: "iduserins",
                table: "insrf",
                column: "iduserins");

            migrationBuilder.CreateIndex(
                name: "idrectmod",
                table: "modrf",
                column: "idrectmod");

            migrationBuilder.CreateIndex(
                name: "idusermod",
                table: "modrf",
                column: "idusermod");

            migrationBuilder.CreateIndex(
                name: "idinsrect",
                table: "rectrf",
                column: "idinsrect");

            migrationBuilder.CreateIndex(
                name: "iduserrect",
                table: "rectrf",
                column: "iduserrect");

            migrationBuilder.CreateIndex(
                name: "idempsol",
                table: "solrf",
                column: "idempsol");

            migrationBuilder.CreateIndex(
                name: "idusersol",
                table: "solrf",
                column: "idusersol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "equiuni");

            migrationBuilder.DropTable(
                name: "fabricante");

            migrationBuilder.DropTable(
                name: "modelo");

            migrationBuilder.DropTable(
                name: "canrf");

            migrationBuilder.DropTable(
                name: "modrf");

            migrationBuilder.DropTable(
                name: "rectrf");

            migrationBuilder.DropTable(
                name: "insrf");

            migrationBuilder.DropTable(
                name: "solrf");

            migrationBuilder.DropTable(
                name: "empresa");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
