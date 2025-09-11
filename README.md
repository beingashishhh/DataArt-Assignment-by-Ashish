# DataArt Assignment by Ashish

This .NET project was created as part of the **.NET School by DataArt program**.  
It represents my learning journey, where I practice building projects, improving coding skills, and applying concepts learned during the course.

---

## 📌 Homework 1: Project Setup & First API

### What was done
- Set up a **3-layer architecture**:
  - `MyProject.API` → Controllers, Swagger UI
  - `MyProject.Data` → EF Core, DbContext, Migrations
  - `MyProject.Domain` → Entities (e.g., Product)
- Created **DbContext** (`AppDbContext`)
- Created **Entity** (`Product`)
- Configured **SQL Server connection**
- Added **API Controller** for Products
- Implemented **CRUD endpoints**:
  - `GET /api/products` → List all products
  - `GET /api/products/{id}` → Get product by ID
  - `POST /api/products` → Add product
  - `PUT /api/products/{id}` → Update product
  - `DELETE /api/products/{id}` → Delete product
- Tested endpoints using **Swagger UI**

### How to run
1. Clone the repo:
   ```bash
   git clone https://github.com/beingashishhh/DataArt-Assignment-by-Ashish.git


## 📌 Homework 2: API Style, Contracts & Controllers

### ✅ Step 1: Compare API Styles
We evaluated **gRPC, GraphQL, and REST** for the AICalendar system.  

| Criteria               | REST                          | GraphQL                           | gRPC                            |
|-------------------------|--------------------------------|------------------------------------|----------------------------------|
| Simplicity              | Easy to learn, CRUD friendly   | Flexible queries but learning curve | Complex setup, not browser-native |
| Tooling                 | Swagger/OpenAPI widely supported | Apollo/Relay ecosystem strong      | Protobuf + gRPC tooling          |
| Latency/Payload         | Can over/under fetch           | Precise data fetching               | Efficient, binary transport      |
| Client Support          | Works everywhere (web, mobile) | JS/TS support good                  | Strong but limited browser support |
| Security                | Mature standards (JWT/OAuth2)  | Field-level auth possible           | TLS + certs                      |
| Observability           | Easy logging/monitoring        | Needs extra setup                   | Built-in tracing                 |
| Best Fit for AICalendar | ✅ CRUD-friendly, simple rollout | Could work but overkill             | Not needed yet                   |

**Decision →** We chose **REST API** because it’s simple, widely supported, and fits CRUD operations for calendars/events.

## Homework 2 (Progress)

### ✅ Chosen API Style
- We are using **REST API** with controllers (`EventsController`, `CalendarsController`, `AttendeesController`).
- REST was chosen because it’s simple, widely supported, and fits CRUD operations for calendars/events.

### ✅ API Contract Overview
- Endpoints:
  - `GET /api/Events` → List all events
  - `GET /api/Events/{id}` → Get event by ID
  - `POST /api/Events` → Create a new event
  - `PUT /api/Events/{id}` → Update an event
  - `DELETE /api/Events/{id}` → Delete an event
  - (similar endpoints exist for `/api/Calendars` and `/api/Attendees`)

### ✅ Example API Calls
```bash
# Get all events
curl -X GET http://localhost:5000/api/Events

# Create an event
curl -X POST http://localhost:5000/api/Events \
   -H "Content-Type: application/json" \
   -d '{"title":"Team Meeting","description":"Discuss project","start":"2025-09-12T10:00:00Z","end":"2025-09-12T11:00:00Z","timezone":"UTC","calendarId":1}'
