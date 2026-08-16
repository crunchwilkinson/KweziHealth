# KweziHealth ESOP (Enterprise Staff Operations Platform) 🏥

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET_Core-MVC-blue?style=for-the-badge)
![EF Core](https://img.shields.io/badge/EF_Core-InMemory-3db28c?style=for-the-badge)
![Bootstrap 5](https://img.shields.io/badge/Bootstrap-5-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)
![Phase 1](https://img.shields.io/badge/Status-Phase_1_Complete-success?style=for-the-badge)

**Phase 1 Foundation**

KweziHealth Systems is a large South African digital healthcare solutions company. Due to rapid expansion and the need to support staff across multiple provinces, the Enterprise Staff Operations Platform (ESOP) was launched to replace outdated spreadsheets and legacy systems. 

This project represents the Phase 1 foundational build of ESOP: a secure, modular, and scalable web-based staff management system built using ASP.NET Core MVC.

## 🚀 Features

* **Secure Admin Access:** Authentication handled by ASP.NET Core Identity's robust security engine, routed through a custom MVC login portal. Open registration is disabled for enterprise security.
* **Staff Directory Management:** Full CRUD (Create, Read, Update, Delete) functionality for managing enterprise personnel records.
* **Modern UI/UX:** A clean, responsive, trust-inspiring healthcare dashboard built with Bootstrap 5 and Bootstrap Icons.
* **Decoupled Architecture:** Strict separation of concerns utilizing a dedicated Service Layer (`IStaffService`) injected via Dependency Injection.
* **Future-Ready Data Layer:** Utilizes Entity Framework Core with an InMemory database provider, designed for a seamless zero-friction transition to SQL Server in Phase 2.

## 🛠️ Tech Stack

* **Framework:** .NET / C# ASP.NET Core MVC
* **Data Access:** Entity Framework Core (In-Memory Provider for Phase 1)
* **Authentication:** ASP.NET Core Identity (Engine only, custom UI)
* **Frontend:** HTML5, CSS3, Bootstrap 5, Bootstrap Icons

## 📁 Project Structure

* `/Controllers` - Contains the `AccessController` for authentication and the secured `StaffController` for staff management.
* `/Models` - Domain entities (`StaffMember`, `SystemAdmin`) and data transfer objects (`LoginViewModel`).
* `/Services` - The business logic layer containing `IStaffService` and its EF Core implementation.
* `/Data` - The `ApplicationDbContext` managing the EF Core models.
* `/Views` - The frontend Razor views, customized for the KweziHealth brand.

## ⚙️ Getting Started

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) (Version 6.0 or higher recommended)
* An IDE such as Visual Studio 2022 or Visual Studio Code
### Installation & Execution

1. **Clone the repository**
   ```bash
   git clone [https://github.com/crunchwilkinson/KweziHealth.git](https://github.com/crunchwilkinson/KweziHealth.git)
   cd KweziHealth
   ```
2. **Navigate to the web project**
   ```bash
   cd KweziHealth.Web
   ```
3. **Restore dependencies**
   ```bash
   dotnet restore
   ```
4. **Run the application**
   ```bash
   dotnet run
   ```
5. **Access the platform**
   Open your browser and navigate to the localhost port provided in your terminal (e.g., https://localhost:5001). You will automatically be redirected to the secure Administrator Login portal.

### 🔐 Administrator Access
Note: Because public registration is strictly disabled in this enterprise platform, an initial Super Admin account is provisioned dynamically by the database seeder upon application startup.
