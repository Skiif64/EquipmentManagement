# EquipmentManagement

Приложения для учета оборудования, создано в рамках практики в ООО ВФ "Элна". Предоставляет возможности:

- Добавлять оборудование.
- Добавлять сотрудников.
- Добавлять статусы оборудования.
- Назначать сотрудника и статус оборудования.
- Добавлять изображения для оборудования.

Для аутентификации используется JWT аутентификация с короткоживущим токеном.

## Конфигурация 
Конфигурация содержиться в файле .env. Для примера существует .env.example. Структура .env:
``` BASH
JWT_ISSUER=EquipmentManagement.API # издатель токена
JWT_AUDIENCE=EquipmentManagement.WASM # аудитория токена
JWT_SECRETKEY=Super-secret-key # секретный ключ подписи токена
JWT_LIFETIME=10 # время жизни токена (в минутах)
ADMIN_USERNAME=Admin # логин админа по умолчанию
ADMIN_PASSWORD=default # пароль админа по умолчанию
APP_DATA="Host=db;Username=postgres;Password=example;Database=ApplicationDb" # строка подключения к бд (postgres) для хранения данных приложения.
AUTH_DATA="Host=db;Username=postgres;Password=example;Database=UserDb" # строка подключения к бд (postgres) для хранения данных польхователей.
PG_PASSWORD=example # пароль бд (postgres). Важно, чтобы пароль в строках подключений и данной переменной окружения были одинаковые
```

## Структура проекта

- Application - содержит главную логику приложения. Основан на паттерне CQRS реализованном через библиотеку MediatR. Также используется AutoMapper для маппинга типов.
- Auth - логика для аутентификации. Используется JWT аутентификация.
- DAL - логика доступа к базе данных и хранилищу изображений, для работы с БД используется Entity Framework 6
- Domain - основные типы приложения
- WebApi - Api приложения
- Contracts - общие типы для WebApi и WebUI

В структуре frontend\`а происходит полнейший ад.

UI сделан на базе Blazor WebAssembly.
