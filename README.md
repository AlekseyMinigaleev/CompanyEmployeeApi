# Company Employee API

Это WebAPI приложение, реализует CRUD операции для двух связанных сущностей: Company и Employee.

## Используемые технологии

- .NET 8
- ASP.NET Core 8
- EF Core 8
- PostgreSQL
- Swagger

## Настройка и запуск

Для успешного запуска проекта убедитесь, что выполнены следующие шаги:

1. **Клонирование репозитория:**

   ```bash
   git clone https://github.com/your/repository.git
   cd repository-folder
  
2. **Настройка PostgreSQL:**
   
   - Убедитесь, что PostgreSQL установлен и запущен.
   - Создайте базу данных `CompanyEmployee` или укажите уже существующую.
   
3. **Настройка строки подключения в `appsettings.json`:**
   
   Откройте файл `appsettings.json` и обновите блок `"ConnectionStrings"` с учетом вашей конфигурации PostgreSQL. Пример строки подключения:
   
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=5432;Database=CompanyEmployee;User Id=postgres;Password=your_password;"
     }
   }
   ```
  Замените "localhost" на адрес вашего сервера PostgreSQL, если требуется.
  Проверьте порт ("Port=5432"), имя пользователя ("User Id=postgres") и пароль ("Password=your_password").

  4. **Запуск приложения**

     ```bash
     dotnet run
     ```
