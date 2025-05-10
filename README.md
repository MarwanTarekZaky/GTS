# Todo Management API and Web UI

## 🌐 Project Overview

This project is a complete Todo Management System built using **ASP.NET Core 8**
with a RESTful API and a front-end Web UI using **Bootstrap**.
It provides a simple, clean interface to manage todos,
with features for filtering, creating, updating, deleting, and marking todos as complete.

## 🚀 Features

* 📝 **API with Full CRUD Operations** (Create, Read, Update, Delete)
* ✅ **Filter Todos by Status, Priority, and Date Range**
* 📊 **Sortable and Searchable Todo List**
* ⚡ **Domain-Driven Design with Clean Code Principles**
* 💡 **Custom Exception Handling and Logging**
* 🔑 **Error Handling with Validation and NotFound Exceptions**
* 📅 **Beautiful Bootstrap UI** (Responsive and User-Friendly)
* 🚀 **Confirmation Modals for Delete and Mark Complete Actions**

## 🗂 Project Structure

```
GTS Solution/
├── API/                  # ASP.NET Core API Project (Todo API)
│   └── Controllers/      # API Controllers
├── APPlication/                  # ASP.NET Core APP Project (APP)
│   └── DTOs/             # Dtos 
│   └── Services/         # Business Logic Services
│   └── Mappings/         # AutoMapper
├── Domain/               # ASP.NET Core Domain Project (Domain)
│   └── Entites/          # Enums & Models 
│   └── Events/           # Events
│   └── Handlers/         # Event handlers  
│   └── Domain/           # Domain Entities and Domain Events
├── Infrastructure/       # ASP.NET Core Infrastructure Project (Infra)
│   └── IRpeositories/    # Repositories Interfaces
│   └── Migrations/       # Migrations folder
│   └── Presistence/      # Main DBContext  
│   └── Repositories/     # Repositories Implementations
├── Shared/               # ASP.NET Core Shared Project (Shared)
│   └── Config/           # Configurations (static)
│   └── Exceptions/       # Custom Exceptions
│   └── MiddleWares/      # Global ExceptionMiddleWare  
│   └── Resources/        # Localization
├── WebUI/                # ASP.NET Core MVC Project (Web UI)
│   └── Controllers/      # Web UI Controllers
│   └── Views/            # Razor Views (Index, Edit, Create, etc.)
│   └── wwwroot/          # Static Assets (CSS, JS, Images)
│   └── Models/           # Web UI Models
├── README.md             # Project Documentation
```

## ⚙️ How to Install and Run

### Prerequisites

* .NET SDK 8.0 or above
* SQL Server (LocalDB or any configured SQL Server)
<!-- In My case I used Docker image for SqlServer on Macintosh m1-->
### Step 1: Clone the Repository

```bash
git clone https://github.com/MarwanTarekZaky/GTS.git
```

### Step 2: Setup the Database

* Update the connection string in `API/appsettings.json`.
* Run database migrations:

```bash
cd API
dotnet ef database update
```

### Step 3: Run the API

```bash
cd API
dotnet run
```

* The API should be running on `http://localhost:5237`. (http)

### Step 4: Run the Web UI

```bash
cd WebUI
dotnet run
```

* The Web UI should be running on `http://localhost::5231`. (http)


## 🚦 API Endpoints

* `GET /api/todo` - List all todos (with filters)
* `POST /api/todo` - Create a new todo
* `PUT /api/todo/{id}` - Update a todo
* `PATCH /api/todo/{id}/complete` - Mark a todo as complete
* `DELETE /api/todo/{id}` - Delete a todo

## 💡 Contributing

Feel free to fork this project, make improvements, and submit a pull request.

## 📄 License

This project is open-source and available under the MIT License.
