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

---

## 📌 Homework 2: API Style, Contracts & Controllers

###  Step 1: Compare API Styles
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

---

## Homework 2 (Progress)

###  Chosen API Style
- We are using **REST API** with controllers (`EventsController`, `CalendarsController`, `AttendeesController`).
- REST was chosen because it’s simple, widely supported, and fits CRUD operations for calendars/events.

###  API Contract Overview
- Endpoints:
  - `GET /api/Events` → List all events
  - `GET /api/Events/{id}` → Get event by ID
  - `POST /api/Events` → Create a new event
  - `PUT /api/Events/{id}` → Update an event
  - `DELETE /api/Events/{id}` → Delete an event
  - (similar endpoints exist for `/api/Calendars` and `/api/Attendees`)

###  Example API Calls
```bash
# Get all events
curl -X GET http://localhost:5000/api/Events

# Create an event
curl -X POST http://localhost:5000/api/Events \
    -H "Content-Type: application/json" \
    -d '{"title":"Team Meeting","description":"Discuss project","start":"2025-09-12T10:00:00Z","end":"2025-09-12T11:00:00Z","timezone":"UTC","calendarId":1}'
```
###  Versioning & Deprecation
- Current version: **v1** (all endpoints under `/api/`)
- Breaking changes will be released as `/api/v2/`
- Deprecated endpoints will remain active for one release cycle with warnings


###  Migration Notes for Clients
- Clients using Homework 1 endpoints should migrate to REST controllers
- All requests must now include a valid **JWT token** in the `Authorization: Bearer <token>` header


###  Local Development Setup
1. Clone this repository  
2. Run `dotnet restore`  
3. Run `dotnet build`  
4. Start the API with:  
   ```bash
   dotnet run --project MyProject.API
5. Open Swagger UI at → [https://localhost:7127/swagger](https://localhost:7127/swagger)


###  Security,  Performance &  Observability
- **Security**: JWT authentication enforced for all protected endpoints  
- **Performance**: REST endpoints optimized for CRUD, lightweight payloads  
- **Observability**: Structured logging enabled with ASP.NET Core logging  


###  Known Limitations
- No rate limiting yet (clients can send unlimited requests)  
- Pagination missing for list endpoints (large datasets may be slow to load)  
- Error handling is basic (we only return simple error codes for now)  
- Contract file (`openapi.yaml`) not yet finalized — endpoints are documented here in README  

---

## 📌 Homework 3 – Completion Summary

###  Coding Tasks Completed
- **Upgrade to .NET 9**
  - Project updated to target **net9.0**
  - All projects build and run successfully

- **Refactor with C# 13 Feature**
  - Applied **Params collections** for handling meeting attendees
  - EF Core mapping configured using a many-to-many relationship (`MeetingAttendees` join table)

- **Entities & Relationships**
  - **Calendars** → can hold multiple events
  - **Events** → linked to a calendar, can have attendees
  - **Attendees** → tied to events, can join multiple meetings
  - **Meetings** → scheduled with one or many attendees

- **Repositories & Controllers**
  - Added repositories for `Calendars`, `Events`, `Attendees`, and `Meetings`
  - Controllers expose full CRUD endpoints
  - JWT Authentication enforced with `[Authorize]`


##  Testing Flow (Swagger)

1. **Create Calendar**  
   `POST /api/Calendars`
   ```json
   { "id": 0, "name": "Work Calendar", "description": "Project tasks" }
   

2. **Create Event**  
   `POST /api/Events`  
   ```json
   { 
     "id": 0, 
     "name": "Tech Conference", 
     "description": "Main event for tech talks" 
   }
   ```
   
3. **Create Attendee**  
   `POST /api/Attendees`  
   ```json
   { 
     "id": 0, 
     "name": "Ashish", 
     "email": "ashish@example.com", 
     "eventId": 8 
   }
   ```
   **Create Attendee**  
   `POST /api/Attendees`  
   ```json
   { 
     "id": 0, 
     "name": "John", 
     "email": "john@example.com", 
     "eventId": 8 
   }
   ```
   
4. **Create Meeting**  
   `POST /api/Meetings`  
   ```json
   { 
     "id": 0, 
     "title": "Project Sync", 
     "startUtc": "2025-09-20T10:00:00", 
     "endUtc": "2025-09-20T11:00:00", 
     "attendeeIds": [8, 9] 
   }
   ```

5. **Get Meetings**  
   `GET /api/Meetings`  
   → returns scheduled meetings
   
6. **Reschedule Meeting**  
   `PUT /api/Meetings/5`  
   ```json
   { 
     "id": 5, 
     "title": "Project Sync (Rescheduled)", 
     "startUtc": "2025-09-20T14:00:00", 
     "endUtc": "2025-09-20T15:00:00", 
     "attendeeIds": [8, 9] 
   }
   ```

7. **Cancel Meeting**  
   `DELETE /api/Meetings/5`  
   → returns `204 No Content`  

   `GET /api/Meetings/5`  
   → returns `404 Not Found`

**Research on Open-Source LLaMA AI Model**  

     **How to Run LLaMA Locally**  
   - Use **Ollama** or **llama.cpp** to run Meta’s LLaMA model on your local machine.  
   - Example with Ollama:  
     ```bash
     ollama run llama2
     ```  
   - Requires **GPU (recommended)** or **CPU with sufficient RAM**.  

     **Integration with Backend API**  
   - ASP.NET Core API can call the local LLaMA server via HTTP.  
   - **Workflow:**  
     1. User sends a natural language query (e.g., “schedule a meeting with Ashish tomorrow at 10am”).  
     2. API forwards it to local LLaMA.  
     3. LLaMA classifies intent (create, reschedule, cancel, list).  
     4. LLaMA extracts entities (title, date, attendees).  
     5. API maps this into structured JSON and calls MCP server tools (Homework 4).  

     **Benefits for the AI Calendar Project**  
   - **Privacy** → Data stays local, not sent to cloud APIs.  
   - **Cost-efficient** → No API usage fees.  
   - **Customizable** → Can fine-tune prompts or model for calendar use-cases.  


#My project. mcp server created 
post/mcp working
NLP integration tomorrow



