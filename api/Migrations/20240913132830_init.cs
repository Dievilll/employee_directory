using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ParentDepartmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Departments_Departments_ParentDepartmentId",
                        column: x => x.ParentDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Salary = table.Column<decimal>(type: "numeric", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                    table.ForeignKey(
                        name: "FK_Positions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Photo = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Name", "ParentDepartmentId" },
                values: new object[,] { 
                    { "ZАVOД", null },         //1
                    { "Первый от завода", 1 }, //2
                    { "Второй от завода", 1 }, //3
                    { "Третий от завода", 1 }, //4
                    { "1 от 1 от завода", 2 }, //5
                    { "2 от 1 от завода", 2 }, //6
                    { "3 от 1 от завода", 2 }, //7
                    { "1 от 2 от завода", 3 }, //8
                    { "2 от 2 от завода", 3 }, //9
                    { "3 от 2 от завода", 3 }, //10
                    { "1 от 3 от завода", 4 }, //11
                    { "2 от 3 от завода", 4 }, //12
                    { "3 от 3 от завода", 4 }, //13
                    });
            

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Title", "Salary", "DepartmentId" },
                values: new object[,]
                {
                    { "Начальник завода", 1000000, 1 },//1
                    { "Начальник 1", 134000, 2 },//2
                    { "Старший программист", 100000, 2 },//3
                    { "Программист", 89000, 2 }, //4
                    { "Программист-стажер", 67000, 2 }, //5
                    { "Начальник 2", 126000, 3 },//6
                    { "Старший техник", 94000, 3 },//7
                    { "Техник", 78000, 3 }, //8
                    { "Техник-стажер", 66000, 3 }, //9
                    { "Начальник 3", 118000, 4 },//10
                    { "Старший разработчик", 98000, 4 },//11
                    { "Разработчик", 78000, 4 }, //12
                    { "Разработчик-стажер", 66000, 4 }, //13
                    { "Начальник 1 от 1", 100000, 5 },//14
                    { "Старший бухгалтер", 90000, 5 },//15
                    { "Бухгалтер", 70000, 5 }, //16
                    { "Бухгалтер-стажер", 60000, 5 }, //17
                    { "Начальник 2 от 1", 90000, 6 },//18
                    { "Старший техник", 83000, 6 },//19
                    { "Техник", 67000, 6 }, //20
                    { "Техник-стажер", 55000, 6 }, //21
                    { "Начальник 3 от 1", 83000, 7 },//22
                    { "Старший электрик", 78000, 7 },//23
                    { "Электрик", 60000, 7 }, //24
                    { "Электрик-стажер", 55000, 7 }, //25
                    { "Начальник 1 от 2", 96000, 8 },//26
                    { "Старший секретарь", 79000, 8 },//27
                    { "Секретарь", 63000, 8 }, //28
                    { "Секретарь-стажер", 49000, 8 }, //29
                    { "Начальник 2 от 2", 89000, 9 },//30
                    { "Старший конструктор", 82000, 9 },//31
                    { "Конструктор", 66000, 9 }, //32
                    { "Конструктор-стажер", 52000, 9 }, //33
                    { "Начальник 3 от 2", 82000, 10 },//34
                    { "Старший механик", 75000, 10 },//35
                    { "Механик", 60000, 10 }, //36
                    { "Механик-стажер", 50000, 10 }, //37
                    { "Начальник 1 от 3", 103000, 11 },//38
                    { "Старший инженер", 83000, 11 },//39
                    { "Инженер", 66000, 11 }, //40
                    { "Инженер-стажер", 52000, 11 }, //41
                    { "Начальник 2 от 3", 89000, 12 },//42
                    { "Старший планировщик", 82000, 12 },//43
                    { "Планировщик", 66000, 12 }, //44
                    { "Планировщик-стажер", 52000, 12 }, //45
                    { "Начальник 3 от 3", 89000, 13 },//46
                    { "Старший менеджер", 82000, 13 },//47
                    { "Менеджер", 66000, 13 }, //48
                    { "Менеджер-стажер", 52000, 13 }, //49
                });



            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] {"FullName", "DepartmentId", "PositionId", "PhoneNumber", "Photo"},
                values: new object[,]
                {
                    { "Сорокин Даниил Артурович", 1, 1, "+7 (718) 127-78-08", ReadPhotoFromFile("orig.png") }, //1 Начальник завода
                    { "Успенский Юрий Сергеевич", 2, 2, "+7 (199) 065-42-95", ReadPhotoFromFile("orig.png") }, //2 Начальник 1
                    { "Серов Дмитрий Дмитриевич", 2, 3, "+7 (879) 879-18-65", ReadPhotoFromFile("orig.png") }, //3 Старший программист
                    { "Борисов Максим Егорович", 2, 4, "+7 (355) 239-12-35", ReadPhotoFromFile("orig.png") }, //4 Программист
                    { "Бурова Аделина Ивановна", 2, 5, "+7 (895) 470-13-31", ReadPhotoFromFile("orig.png") }, //5 Программист-стажер
                    { "Киселева Александра Тимофеевна", 3, 6, "+7 (114) 734-89-90", ReadPhotoFromFile("orig.png") }, //6 Начальник 2
                    { "Молчанова Александра Робертовна", 3, 7, "+7 (671) 851-95-73", ReadPhotoFromFile("orig.png") }, //7 Старший техник
                    { "Аникина Анна Сергеевна", 3, 8, "+7 (592) 220-62-72", ReadPhotoFromFile("orig.png") }, //8 Техник
                    { "Кондратьев Иван Игоревич", 3, 9, "+7 (056) 117-75-25", ReadPhotoFromFile("orig.png") }, //9 Техник-стажер
                    { "Миронов Дмитрий Семёнович", 4, 10, "+7 (011) 324-25-72", ReadPhotoFromFile("orig.png") }, //10 Начальник 3
                    { "Беляев Кирилл Климович", 4, 11, "+7 (258) 711-63-54", ReadPhotoFromFile("orig.png") }, //11 Старший разработчик
                    { "Розанова Фатима Дмитриевна", 4, 12, "+7 (881) 680-21-05", ReadPhotoFromFile("orig.png") }, //12 Разработчик
                    { "Степанова Елена Владимировна", 4, 13, "+7 (859) 001-08-76", ReadPhotoFromFile("orig.png") }, //13 Разработчик-стажер
                    { "Кузнецов Фёдор Георгиевич", 5, 14, "+7 (441) 691-83-07", ReadPhotoFromFile("orig.png") }, //14 Начальник 1 от 1
                    { "Романов Александр Дамирович", 5, 15, "+7 (353) 413-25-44", ReadPhotoFromFile("orig.png") }, //15 Старший бухгалтер
                    { "Шмелев Дмитрий Иванович", 5, 16, "+7 (046) 923-56-80", ReadPhotoFromFile("orig.png") }, //16 Бухгалтер
                    { "Попов Даниил Дмитриевич", 5, 17, "+7 (715) 635-85-55", ReadPhotoFromFile("orig.png") }, //17 Бухгалтер-стажер
                    { "Лосева Ангелина Демидовна", 6, 18, "+7 (003) 315-21-62", ReadPhotoFromFile("orig.png") }, //18 Начальник 2 от 1
                    { "Демидов Марк Никитич", 6, 19, "+7 (394) 929-12-28", ReadPhotoFromFile("orig.png") }, //19 Старший техник
                    { "Герасимова Алёна Фёдоровна", 6, 20, "+7 (854) 980-20-55", ReadPhotoFromFile("orig.png") }, //20 Техник
                    { "Васильева Таисия Тимофеевна", 6, 21, "+7 (819) 499-19-69", ReadPhotoFromFile("orig.png") }, //21 Техник-стажер
                    { "Матвеева Милана Руслановна", 7, 22, "+7 (312) 348-32-82", ReadPhotoFromFile("orig.png") }, //22 Начальник 3 от 1
                    { "Калинин Кирилл Антонович", 7, 23, "+7 (575) 710-11-50", ReadPhotoFromFile("orig.png") }, //23 Старший электрик
                    { "Шубина Анна Михайловна", 7, 24, "+7 (448) 206-20-31", ReadPhotoFromFile("orig.png") }, //24 Электрик
                    { "Морозова Варвара Марковна", 7, 25, "+7 (271) 648-83-06", ReadPhotoFromFile("orig.png") }, //25 Электрик-стажер
                    { "Киселев Ярослав Даниилович", 8, 26, "+7 (722) 955-68-24", ReadPhotoFromFile("orig.png") }, //26 Начальник 1 от 2
                    { "Баранова Ксения Матвеевна", 8, 27, "+7 (711) 786-47-30", ReadPhotoFromFile("orig.png") }, //27 Старший секретарь
                    { "Демина Ольга Николаевна", 8, 28, "+7 (520) 226-68-16", ReadPhotoFromFile("orig.png") }, //28 Секретарь
                    { "Моисеев Фёдор Кириллович", 8, 29, "+7 (445) 745-98-87", ReadPhotoFromFile("orig.png") }, //29 Секретарь-стажер
                    { "Грачева Анна Александровна", 9, 30, "+7 (165) 906-40-69", ReadPhotoFromFile("orig.png") }, //30 Начальник 2 от 2
                    { "Сидоров Дмитрий Антонович", 9, 31, "+7 (577) 071-61-79", ReadPhotoFromFile("orig.png") }, //31 Старший конструктор
                    { "Горбачева Дарья Семёновна", 9, 32, "+7 (779) 004-13-95", ReadPhotoFromFile("orig.png") }, //32 Конструктор
                    { "Петрова Виктория Елисеевна", 9, 33, "+7 (388) 426-53-02", ReadPhotoFromFile("orig.png") }, //33 Конструктор-стажер
                    { "Александрова Кира Тимуровна", 10, 34, "+7 (410) 890-11-71", ReadPhotoFromFile("orig.png") }, //34 Начальник 3 от 2
                    { "Пахомов Кирилл Платонович", 10, 35, "+7 (964) 125-91-21", ReadPhotoFromFile("orig.png") }, //35 Старший механик
                    { "Соболева Арина Дамировна", 10, 36, "+7 (494) 762-01-55", ReadPhotoFromFile("orig.png") }, //36 Механик
                    { "Поликарпов Иван Леонидович", 10, 37, "+7 (703) 321-33-99", ReadPhotoFromFile("orig.png") }, //37 Механик-стажер
                    { "Савельев Иван Леонидович", 11, 38, "+7 (316) 405-81-88", ReadPhotoFromFile("orig.png") }, //38 Начальник 1 от 3
                    { "Грачева Кристина Григорьевна", 11, 39, "+7 (620) 220-64-90", ReadPhotoFromFile("orig.png") }, //39 Старший инженер
                    { "Зайцева София Егоровна", 11, 40, "+7 (458) 873-05-84", ReadPhotoFromFile("orig.png") }, //40 Инженер
                    { "Михайлов Георгий Кириллович", 11, 41, "+7 (145) 894-60-30", ReadPhotoFromFile("orig.png") }, //41 Инженер-стажер
                    { "Волкова Дарья Кирилловна", 12, 42, "+7 (336) 886-14-69", ReadPhotoFromFile("orig.png") }, //42 Начальник 2 от 3
                    { "Воробьев Платон Дмитриевич", 12, 43, "+7 (574) 712-44-35", ReadPhotoFromFile("orig.png") }, //43 Старший планировщик
                    { "Горлова Ксения Ивановна", 12, 44, "+7 (080) 811-14-24", ReadPhotoFromFile("orig.png") }, //44 Планировщик
                    { "Сахарова Алиса Никитична", 12, 45, "+7 (531) 633-90-13", ReadPhotoFromFile("orig.png") }, //45 Планировщик-стажер
                    { "Новиков Денис Степанович", 13, 46, "+7 (656) 223-14-70", ReadPhotoFromFile("orig.png") }, //46 Начальник 3 от 3
                    { "Аникин Артём Максимович", 13, 47, "+7 (141) 542-56-34", ReadPhotoFromFile("orig.png") }, //47 Старший менеджер
                    { "Сорокин Даниил Данилович", 13, 48, "+7 (812) 812-19-93", ReadPhotoFromFile("orig.png") }, //48 Менеджер
                    { "Чернышева Мария Фёдоровна", 13, 49, "+7 (478) 037-19-37", ReadPhotoFromFile("orig.png") }, //49 Менеджер-стажер

                });


            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Password", "Role" },
                values: new object[] { "root", "root", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentDepartmentId",
                table: "Departments",
                column: "ParentDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmentId",
                table: "Positions",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Departments");
        }
        private byte[] ReadPhotoFromFile(string filePath)
        {
            // Укажите путь к фотографии относительно корня проекта
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "static", "img", filePath);

            if (File.Exists(fullPath))
            {
                return File.ReadAllBytes(fullPath);
            }
            return null;
        }
    }
}