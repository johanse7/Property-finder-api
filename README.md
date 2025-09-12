# PropertyApp API

This is a **.NET 9** API for managing properties, owners, property images, and property traces. It uses **MongoDB** as the database and **Swagger** for exploring and testing endpoints. The API also supports **Docker** for running the database and seeding initial data.

---

## 🚀 Technologies

- .NET 9
- C#
- MongoDB
- AutoMapper
- Swagger / OpenAPI
- Dependency Injection (DI)
- Docker / Docker Compose

---

## ⚙️ Configuration

1. Clone the repository:

```bash
git clone <repository-url>
cd PropertyApp.Api
```

2. Configure MongoDB connection in `appsettings.json`:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "propertydb"
  }
}
```

3. Restore dependencies:

```bash
dotnet restore
```

---

## 🏃‍♂️ Run the API

Run the API in development mode:

```bash
dotnet run
```

By default, it will run at endpoint:

```
http://localhost:5154/real-estate/properties
```

---

## 🐳 Running MongoDB with Docker and Seed Data

You can use **Docker Compose** to spin up MongoDB with initial data:

1. Example `docker-compose.yml`:

```yaml
version: "3.8"

services:
  mongo:
    image: mongo:6.0
    container_name: property-mongo
    ports:
      - "27017:27017"
    volumes:
      - ./docker-init:/docker-entrypoint-initdb.d
```

2. Place your seed JSON files in `./docker-init/`:

- `owners.json`
- `properties.json`
- `propertyImages.json`
- `propertyTraces.json`

3. Add a seed script (`seed.sh`) in the same folder:

```bash
#!/bin/bash
set -e

DB_NAME="propertydb"
MONGO_URI="mongodb://localhost:27017"

echo "Uploading data to $DB_NAME..."

mongoimport --uri=$MONGO_URI/$DB_NAME --collection=owners --file=/docker-entrypoint-initdb.d/owners.json --jsonArray --drop
mongoimport --uri=$MONGO_URI/$DB_NAME --collection=properties --file=/docker-entrypoint-initdb.d/properties.json --jsonArray --drop
mongoimport --uri=$MONGO_URI/$DB_NAME --collection=propertyImages --file=/docker-entrypoint-initdb.d/propertyImages.json --jsonArray --drop
mongoimport --uri=$MONGO_URI/$DB_NAME --collection=propertyTraces --file=/docker-entrypoint-initdb.d/propertyTraces.json --jsonArray --drop

echo "Data uploaded successfully to $DB_NAME"
```

4. Run Docker Compose:

```bash
docker-compose down -v
docker-compose up --build

```

---

## 📄 Swagger UI

Swagger is enabled in **Development** environment. Access it at:

```
http://localhost:5154/swagger
```

---

## ⚡ Available Endpoints

The API is exposed under the `/real-estate/properties` route:

| HTTP Method | Endpoint                       | Description                          |
| ----------- | ------------------------------ | ------------------------------------ |
| GET         | `/real-estate/properties`      | Get properties with optional filters |
| GET         | `/real-estate/properties/{id}` | Get property details by ID           |

### Query Parameters for GET `/real-estate/properties`:

- `Name` (string) — Filter by property name
- `Address` (string) — Filter by property address
- `MinPrice` (decimal) — Minimum price filter
- `MaxPrice` (decimal) — Maximum price filter
- `Page` (int) — Page number for pagination
- `PageSize` (int) — Number of items per page

---

## 🧱 Project Structure

```
PropertyApp.Api/
├─ Controllers/          # API controllers
├─ Infrastructure/       # Repositories and DB configuration
├─ Application/          # Use cases and DTOs
├─ Domain/               # Domain entities
├─ Program.cs            # Application configuration
├─ appsettings.json      # MongoDB and other configuration
├─ docker-init/          # Seed JSON files and seed.sh
```

