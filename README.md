# MisAspire
### ТЗ
Написать мини проект на .Net 9, .NET Aspire, ASP.NET Core

Создать БД со следующими сущностями (пациент, доктор, болезни), продумать поля для потенциальной бизнес логики МИС (Медицинская информационная система)
Использовать механизм миграций

Создать контроллеры на вывод информации о каждом пациенте, о всех пациентах.
Информация о докторах по специальностям
Также общий список болезней
Не забывать обработку исключений и логирование

### Что сделано
Всё согласно ТЗ.


Обработка исключений через интерфейс `IExceptionHandler`.


Хранение данных в `PostgreSQL`.


Логирование настроено через `Log4Net`, в папке с `MisAspire.WebAPI.dll`
Путь к логам (для Debug):
```text
.\MisAspire\MisAspire.WebAPI\bin\Debug\net9.0\Logs\MisAspire.log
```
Дополнительно добавлены сущности:
- Болезни Пациента (Patient_Disease)
- Прием (Appointment)


База данных (если пустая) заполняется данными из файла `MisContextExtensions.cs`.

### Структура решения
```bash
MisAspire/
├── MisAspire.AppHost/                        проект .NET Aspire (оркестратор)
│   └── AppHost.cs
├── MisAspire.Domain/                         проект содержит сущности
│   └── Entity/
│       ├── Appointment.cs
│       ├── Disease.cs
│       ├── Doctor.cs
│       ├──	Patient.cs
│ 		└──	PatientDisease.cs
├── MisAspire.Persistence/                    проект содержит слой доступа к данным
│   ├── Context/
│	│	└── MisContext.cs
│	├── Extensions/
│	│	└── MisContextExtensions.cs
│	└── Migtarions/
│		├── 20250915032945_Init.cs
│		├── 20250915032945_Init.Designer.cs
│		└── MisContextModelSnapshot.cs
├──	MisAspire.ServiceDefaults/                 проект .NET Aspire (конфигурации)
│	└── Extensions.cs
└──	MisAspire.WebAPI/                          проект содержит контроллеры
	├── Controllers/
	│	├── DiseasesController.cs
	│	├── DoctorsController.cs
	│	└── PatientsController.cs
	├── Exceptions/
	│	└── GlobalExceptionHandler.cs
	└── Program.cs
```

### Схема Базы Данных
![DB][https://github.com/Certalarm/MisAspire/blob/master/_assets/BD_diagram.png]

### Запуск приложения
```bash
dotnet run
```
### Web API
#### Дашборд Aspire доступен по адресу:
```
https://localhost:17235/
```
#### Контроллеры доступны по адресам:
- Вывод информации о всех пациентах
```
https://localhost:7181/api/patients/
```
- Вывод информации о каждом пациенте:
```
https://localhost:7181/api/patients/1
```
- Вывод информации о всех докторах;
```
https://localhost:7181/api/doctors/
```
- Вывод информации о докторах по специальностям:
```
https://localhost:7181/api/doctors/specialty/хирург
```
- Вывод общего списка болезней:
```
https://localhost:7181/api/diseases/
```

### Возможные проблемы
Для поддержки https необходимо установить сертификаты разработки:
```bash
dotnet dev-certs https --trust
```
Если компилируется нормально, но при запуске ошибка `Fast Fail ...`, добавить в каждый csproj-файл:
```xml
<CETCompat>false</CETCompat>
```

### Пример вывода информации о всех пациентах
По запросу `https://localhost:7181/api/patients/` будет выведено:
```json
[
  {
    "id": 1,
    "firstName": "Петя",
    "lastName": "Иванов",
    "birthDate": "1995-09-15T03:30:03.284694Z",
    "gender": "Мужской",
    "contactNumber": "+79999999999",
    "email": "pivanov@ya.ru",
    "address": "Яблочная 1",
    "insuranceNumber": "99-9999-99999",
    "patientDiseases": [
      {
        "id": 1,
        "patientId": 1,
        "diseaseId": 1,
        "diagnosedDate": "2025-09-10T03:30:03.343085Z",
        "recoveryDate": "2025-09-13T03:30:03.343137Z",
        "treatmentNotes": "Анальгин, 3р в д"
      }
    ],
    "appointments": [
      {
        "id": 1,
        "patientId": 1,
        "doctorId": 1,
        "scheduleTime": "2025-09-10T03:30:03.32942Z",
        "status": "Выполнено",
        "notes": "Открывался больничный"
      }
    ]
  },
  {
    "id": 2,
    "firstName": "Люба",
    "lastName": "Петрова",
    "birthDate": "2007-09-15T03:30:03.317367Z",
    "gender": "Женский",
    "contactNumber": "+78888888888",
    "email": "yupetrova@mail.ru",
    "address": "Грушевая 2",
    "insuranceNumber": "88-8888-88888",
    "patientDiseases": [
      {
        "id": 2,
        "patientId": 2,
        "diseaseId": 2,
        "diagnosedDate": "2025-09-08T03:30:03.353101Z",
        "recoveryDate": "2025-09-12T03:30:03.353102Z",
        "treatmentNotes": "Аспирин 2р в д"
      }
    ],
    "appointments": [
      {
        "id": 2,
        "patientId": 2,
        "doctorId": 2,
        "scheduleTime": "2025-09-08T03:30:03.342561Z",
        "status": "Выполнено",
        "notes": "Открывался больничный"
      }
    ]
  },
  {
    "id": 3,
    "firstName": "Витя",
    "lastName": "Сидоров",
    "birthDate": "1981-09-15T03:30:03.317756Z",
    "gender": "Мужской",
    "contactNumber": "+77777777777",
    "email": "vsidorov@gmail.com",
    "address": "Виноградная 3",
    "insuranceNumber": "77-7777-77777",
    "patientDiseases": [
      {
        "id": 3,
        "patientId": 3,
        "diseaseId": 3,
        "diagnosedDate": "2025-09-06T03:30:03.353154Z",
        "recoveryDate": "2025-09-11T03:30:03.353154Z",
        "treatmentNotes": "Цитрамон, 5р в д"
      }
    ],
    "appointments": [
      {
        "id": 3,
        "patientId": 3,
        "doctorId": 3,
        "scheduleTime": "2025-08-31T03:30:03.342652Z",
        "status": "Выполнено",
        "notes": "Не открывался больничный"
      }
    ]
  }
]
```
