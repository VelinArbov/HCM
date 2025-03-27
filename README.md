# 🧑‍💼 Human Capital Management System (HCM) — Built with .NET Aspire

A modern HR web application built with **.NET Aspire**, **Blazor Server**, **Minimal APIs**, and **Entity Framework Core** — designed to manage employee records, authentication, and role-based access for HR teams in small/medium businesses.

---

## 🧠 Powered by .NET Aspire

This solution leverages **.NET Aspire** for:

- 🌐 Composable, cloud-ready applications
- 📊 Built-in Observability and Metrics (via Aspire Dashboard)
- 🔌 Service orchestration
- 🛠️ Streamlined local development

> Learn more about [Aspire](https://aka.ms/aspire)

---

## 🏗️ Architecture Overview


---

## ⚙️ Tech Stack

- ✅ [.NET 8+ with Aspire](https://aka.ms/aspire)
- ✅ Blazor Server for UI
- ✅ Minimal APIs for endpoints
- ✅ EF Core with SQL Server
- ✅ ASP.NET Identity (cookie-based auth)
- ✅ Clean Architecture
- ✅ Aspire Dashboard for observability

---

## 📦 Features

- 🔐 Login system with cookie-based auth
- 👥 Role-based access: `HR Admin`, `Manager`, `Employee`
- 📋 People management (CRUD)
- 🧠 Seeded default users + roles
- 🗂️ Clean project separation for scalability
- 🔍 Centralized observability via Aspire dashboard

---

## 🚀 Getting Started (with Aspire)

### 🧰 Prerequisites

- [.NET 8 SDK (with Aspire workload)](https://aka.ms/aspire)
- [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- Docker (if using containers for SQL or other services)

---

### 🟢 1. Run Aspire AppHost

```bash
dotnet run --project HCM.AppHost
