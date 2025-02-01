# PatientCareManager
A healthcare-focused full-stack web application built with **.NET (C#) for the backend** and **Angular** for the front end. This app allows healthcare professionals to manage patient records efficiently. It includes features such as search, filtering, sorting, pagination, and viewing patient recommendations such as allergy checks and screenings.

## Features

🔍 Search Patients by name or other details

📊 Sort & Filter patient records

📑 Pagination for large datasets

✅ Update Recommendation Status

🔄 API Integration for fetching and updating patient data


## Technologies Used

### **Frontend (Angular)**
- Angular 19
- TypeScript
- Tailwind CSS
- RxJS (for API requests)
- HTTP Client Module (for API communication)

### **Backend (.NET)**
- .NET Core / .NET 8
- C#
- Entity Framework Core 
- ASP.NET Web API

### **Database**
- SQL Server

### **Authentication:** 
- JWT-based authentication
  
### **API Documentation:** 
- Swagger / OpenAPI


### **Prerequisites**

- [Node.js](https://nodejs.org/) (for Angular)
- [Angular CLI](https://angular.io/cli)  
- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Server


## Architecture Overview
- **Frontend (Angular):** Handles UI and API communication.
- **Backend (.NET Core Web API):** Implements business logic and database interactions.
- **Database (SQL Server):** Stores patient data.
- **Security:** Includes JWT authentication, role-based access, and protection against SQL Injection & XSS.


## Setup Instructions

### **Backend Setup (.NET)**
1. Clone the repository:
   - git clone https://github.com/krishashah64/PatientCareManager.git
   - cd PatientCareManager/backend
2. Restore dependencies:
   - dotnet restore
3. Update appsettings.json with your database connection details.
4. Run database migrations
   - dotnet ef database update
5. Start the backend server:
   - dotnet run

### **Frontend Setup (Angular)**
1. Navigate to the frontend directory:
   - cd ../frontend
2. Install dependencies:
   - npm install
3. Start the Angular development server:
   - ng serve
4. Open the application in your browser:
   - http://localhost:4200


