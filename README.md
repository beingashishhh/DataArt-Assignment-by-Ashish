# DataArt Assignment by Ashish (AI Calendar)

An AI-driven calendar system that lets users schedule, update, and cancel events using natural language, powered by a local LLM and orchestrated through an MCP server.

---

## 📌 Homework 1: Project Setup & First API

Objective
Set up a .NET 3-layer architecture and build a basic CRUD API with EF Core and Swagger

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

Objective
Compare API styles, define contracts, and implement secured REST controllers with JWT

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
### Versioning & Deprecation
- The current API version is **v1** (everything starts with `/api/`).
- When big changes happen, a new version will be created → `/api/v2/`.
- Old (deprecated) versions will still work for one update, but you’ll see warnings.

### Migration Notes for Clients
- If you’re still using the old Homework 1 API, move to the new REST API endpoints.
- Every request now needs a **JWT token** in the header:  
  `Authorization: Bearer <your_token>`

### Local Development Setup
1. Clone (download) this project.  
2. Run:
   ```bash
   dotnet restore
   ```
   → installs all required packages.  
3. Build the project:
   ```bash
   dotnet build
   ```
4. Start the API:
   ```bash
   dotnet run --project MyProject.API
   ```
5. Open Swagger in your browser: [https://localhost:7127/swagger](https://localhost:7127/swagger)  
   → Swagger is an interface to test the API.

### Security, Performance & Observability
- **Security**: Only users with a valid JWT token can access protected parts of the API.  
- **Performance**: Endpoints are optimized to quickly handle basic Create/Read/Update/Delete (CRUD) actions.  
- **Observability**: Logging is enabled so you can track what’s happening inside the app.

### Known Limitations
- No request limits yet (clients can send unlimited requests).  
- No pagination → long lists might load slowly.  
- Error handling is very basic (only simple error codes).  

---

## 📌 Homework 3

Objective
Upgrade to .NET 9, add entities/relationships, and implement repositories with authentication

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

---

**Research on Open-Source LLaMA AI Model**  

     How to Run LLaMA Locally  
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

---

## 📌 Homework 4 – AI Calendar Integration

Objective
To extend the Calendar project with an **AI-driven orchestration layer**, enabling natural language event scheduling using **Ollama (phi3)** as the local LLM.


###   What was Done
1. **MCP Server (`MyProject.MCPServer`)**
   - Runs at `http://localhost:5035`
   - Provides endpoints:
     - `POST /save_event` → save event
     - `POST /update_event` → update event
     - `POST /cancel_event` → cancel event
     - `GET /get_events` → retrieve events
   - Currently stores events in memory (no database yet).

2. **API Layer (`MyProject.API`)**
   - Runs at `http://localhost:5173`
   - Acts as a **bridge between frontend and MCP**
   - `OrchestrationController` endpoints:
     - `POST /api/orchestration/process` → takes a natural language prompt, calls Ollama → generates structured JSON → MCP `/save_event`
     - `GET /api/orchestration/events` → fetches all events
     - `POST /api/orchestration/update` → forwards update request to MCP
     - `POST /api/orchestration/cancel` → forwards cancel request to MCP
   - Integrated `ITextModel` for Ollama (phi3) interaction.

3. **Domain Layer (`MyProject.Domain`)**
   - Defines contracts and interfaces (e.g., `ITextModel`)
   - Decouples AI logic from controllers

4. **Data Layer (`MyProject.Data`)**
   - Placeholder for future persistence (e.g., SQLite, EF Core)
   - Currently unused


###   How to Run

Open **3 terminals** in the project root:

**Run Ollama (phi3)**
```bash
ollama run phi3
```

**Run MCP**
```bash
dotnet run --project .\MyProject.MCPServer
```

**Run API**  
```bash
dotnet run --project .\MyProject.API
```

### Testing

- Open the Swagger UI in your browser:  
  [http://localhost:5173/swagger](http://localhost:5173/swagger)  

- Swagger allows you to try out the API without writing code.  
  - You can **add events**  
  - You can **update events**  
  - You can **cancel events**  
  - You can **view scheduled events**  

- Example → Add an event:  
  ```json
  {
    "prompt": "Meeting with Sarah tomorrow 3pm to 4pm"
  }
  ```

---

---

## Conclusion

With the help of these four homeworks, I progressively learned how to:  
- Set up and structure a .NET project with a 3-layer architecture.  
- Design, compare, and implement API styles and contracts.  
- Upgrade projects to the latest .NET version while applying new C# features.  
- Integrate AI with MCP and API orchestration to build a functional calendar system.  

This journey helped me grow my **.NET, API design, and AI integration skills**, while also improving my ability to think about system architecture step by step.  

---

## Screenshot

<img width="1912" height="1022" alt="Screenshot 2025-09-14 214606" src="https://github.com/user-attachments/assets/8ef8cebb-9846-4739-b161-8f012c0a4b8c" />
<img width="1919" height="1024" alt="Screenshot 2025-09-14 220237" src="https://github.com/user-attachments/assets/2458c3d0-84c0-42bf-85d7-7ac61c82f2d3" />

---
## Acknowledgements

I would like to sincerely thank:  

- **Suraj Ghosi**  
- **Abhishek Bharti**  
- **Almas Karimsakov**  
- **Yenlik Tazabek**  
- **Sheetal Kale**  
- And the entire **DataArt team**  

for their invaluable guidance, support, and for giving me this wonderful learning opportunity through the **.NET School by DataArt program**.  

---




