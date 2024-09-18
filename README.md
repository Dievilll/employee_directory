# Проект "Employee Directory"

Этот проект представляет собой API для управления сотрудниками, отделами и должностями. Проект использует Entity Framework Core для работы с базой данных.

## Требования

- .NET SDK 6.0 или выше
- SQL Server (локальный или удаленный)
- Node.js и npm (для фронтенд-части)

## Установка и запуск проекта

### 1. Клонирование репозитория

Склонируйте репозиторий на ваш локальный компьютер:

```bash
git clone https://github.com/Dievilll/employee_directory.git
cd employee_directory
```
### 2. Настройка строки подключения к базе данных

Откройте файл appsettings.json и настройте строку подключения к вашей базе данных SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=employee_directory;User Id=your_username;Password=your_password;"
  }
}
```

### 3. Восстановление пакетов

Восстановите все необходимые пакеты:

```bash
dotnet restore
```
### 4. Создание базы данных и применение миграций

```bash
dotnet ef database update
```
Эта команда создаст базу данных (если она не существует) и применит все миграции, включая начальные данные.

### 5. Запуск проекта
Убедитесь, что находитесь в директории employee_directory.

Запуск API:

```bash
dotnet run --project .\api\
```

Запуск frontend:

```bash
cd /frontend
npm install
npm start 
```

Веб-интерфейс будет доступен по адресу 'http://localhost:3000/auth'

## Структура проекта
- 'api/': Основной проект API.
- 'api/Models/': Модели данных.
- 'api/Migrations/': Миграции БД.
- 'api/Controllers/': Контроллеры API.
- 'api/Data/': Контекст БД.
- 'frontend/': Фронтенд-часть проекта.
- 'frontend/src/': Исходный код фронтенд-приложения.


# Заключение
Этот `README.md` файл содержит все необходимые инструкции для установки, настройки и запуска проекта, включая фронтенд-часть. Если у вас все еще возникают проблемы, убедитесь, что файл `package.json` действительно находится в папке `frontend` и что у вас есть права на чтение и запись в этой папке.