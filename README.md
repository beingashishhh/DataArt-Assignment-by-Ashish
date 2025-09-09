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
