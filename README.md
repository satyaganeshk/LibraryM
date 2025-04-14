=======
# 📚 Library Management System - ASP.NET Core + Cosmos DB

A full-stack backend Library Management System built with **ASP.NET Core**, **Azure Cosmos DB**, and layered architecture principles. This project demonstrates a real-world application structure with features like book management, student/librarian registration, and book issuing/returning — all built with scalability and maintainability in mind.

---

## 🚀 Features

### ✅ Core Modules
- **📘 Book Management**  
  - Add, update, view, and soft-delete books
- **👨‍🎓 Student Management**  
  - Student signup, login, update, and removal
- **👨‍🏫 Librarian Management**  
  - Admin-style librarian features
- **🔁 Book Borrow & Return**  
  - Track issue and return cycles with date stamping

### 🧱 Architecture
- **Entity Layer** – Database schema (Cosmos DB)
- **DTO Layer (Model)** – API data transfer objects
- **Service Layer** – Business logic (DI used)
- **Interface Layer** – For abstraction
- **Controller Layer** – Clean REST APIs

### ☁️ Technologies Used
- ASP.NET Core Web API
- Azure Cosmos DB
- Newtonsoft.Json (for document mapping)
- Dependency Injection
- Swagger UI (for testing APIs)
- C# .NET 8.0

---

## 📁 Project Structure


