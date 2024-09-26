# TMDB API Project

Dette projekt er en .NET API-applikation, der fungerer som et mellemled mellem din frontend-applikation og The Movie Database (TMDB). Formålet med denne API er at cache anmodninger for at forbedre ydelsen og reducere antallet af direkte kald til TMDB API.

## Kom godt i gang

For at køre dette projekt lokalt skal du have følgende værktøjer installeret:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- En TMDB API-nøgle

## Opsætning

Følg disse trin for at opsætte projektet lokalt:

1. **Klon repository:**

   ```bash
   git clone https://github.com/akariev/TMDB-Api
   cd TMDB-Api
   ```

2. **Tilføj TMDB API-nøglen:**

   For at kunne interagere med TMDB, skal du tilføje din API-nøgle som en miljøvariabel i din lokale udviklingsmiljø. Opret eller opdater `.env` filen i projektets rodmappe og tilføj følgende linje:

   ```plaintext
   TMDB_ApiKey=<din-tmdb-api-nøgle>
   ```

3. **Genoprett dependencies:**

   Kør følgende kommando for at genoprette nuværende afhængigheder:

   ```bash
   dotnet restore
   ```

4. **Kør applikationen:**

    Start applikationen ved hjælp af følgende kommando:

    ```bash
    dotnet run
    ```

5. **Adgang til Swagger UI:**

    Når applikationen kører, kan du få adgang til Swagger UI for at teste og dokumentere dine API endpoints via denne URL:
    
    ```
    http://localhost:<port>/swagger/index.html
    ```

6. **Demo-site:**

    Du kan også se den hostede version af applikationen på Azure ved hjælp af dette link:
    
    [Demo Site](https://tmdb-bsfkf9fse2avh7hc.germanywestcentral-01.azurewebsites.net/swagger/index.html)

## Afhængigheder

Projektet bruger følgende NuGet pakker:

- `Microsoft.AspNetCore.OpenApi` Version 8.0.8 - Til OpenAPI-specifikationer.
- `Swashbuckle.AspNetCore` Version 6.4.0 - Til generering af Swagger-dokumentation.

## Funktionalitet

Denne API har følgende nøglefunktioner:

- **Caching:** Reducerer antallet af direkte kald til TMDB ved at cache svarene fra tidligere anmodninger.
  
- **Proxying:** Tjener som en proxy mellem frontend-applikationen og TMDB, hvilket giver et ensartet interface for frontend'en.
