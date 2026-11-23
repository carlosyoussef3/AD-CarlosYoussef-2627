# TimesheetApi: Capstone milestone 1

ASP.NET Web API (.NET 10) voor projecturenregistratie: projecten, urenregistraties en JWT-authenticatie.
Opslag in SQLite met Entity Framework, gelaagde architectuur (controller → service → repository) met dependency injection.

## Opstarten

Vereist: .NET 10 SDK en de EF-tools (`dotnet tool install --global dotnet-ef`).

```
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run --launch-profile https
```

De database `timesheets.db` wordt bij de eerste start automatisch gevuld met voorbeelddata (DbInitializer).
Testen kan met `app.http` in Visual Studio.

Seed-gebruiker om meteen in te loggen: **demo / demo123**

## Endpoints

| Methode | Route | Login nodig | Statuscodes |
|---|---|---|---|
| POST | /auth/register | nee | 200, 400, 409 (username bestaat al) |
| POST | /auth/login | nee | 200 (token), 401 |
| GET | /projects | nee | 200 |
| GET | /projects/{id} | nee | 200, 404 |
| POST | /projects | ja | 201, 400, 401 |
| PUT | /projects/{id} | ja | 204, 400, 401, 404 |
| DELETE | /projects/{id} | ja | 204, 401, 404 |
| GET | /timeentries?projectId= | nee | 200 |
| GET | /timeentries/{id} | nee | 200, 404 |
| POST | /timeentries | ja | 201, 400, 401, **409 (project afgesloten)** |
| DELETE | /timeentries/{id} | ja | 204, 401, 404 |

## Regels

- Een nieuw project start altijd met status `Active` (de client kiest dat niet).
- `RegisteredBy` komt uit het JWT-token (claim `Name`), niet uit de request.
- Businessregel (service-laag): uren boeken op een project met status `Closed` → **409 Conflict**.
- `ProjectId` moet naar een bestaand project verwijzen (service-laag) → anders 400.
- Validatie: Name, Client, Description verplicht; Hours > 0; Date verplicht.

## Structuur

```
Model/        Project, TimeEntry, User (entiteiten = tabellen)
Data/         AppDbContext, DbInitializer (seeding), repositories (enkel database-toegang)
Dtos/         wat de API ontvangt en teruggeeft + validatie + mappings (ToDto / ToEntity)
Services/     businesslogica en regels (o.a. de 409-regel), JWT aanmaken
Controllers/  HTTP: routes, [Authorize], juiste statuscodes
```
