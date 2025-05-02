# 🧾 Order Management System - Northwind Traders

## Overview

This repository contains the **Order Management System** for **Northwind Traders**, developed using **ASP.NET Core** for the backend and **React** for the frontend. It provides functionalities for order creation, validation, management, and report generation.

> 📘 For complete technical documentation, refer to the `North_Order_Mang_Documentation` file located at the root level alongside this README.

### ✅ Key Features

* Full **CRUD operations** for orders and order details
* **Address validation** with Google Maps API (autocomplete & interactive map)
* **PDF report generation** for all orders and order details
* **Unit testing** with XUnit (in progress)

---

## ⚙️ Backend

The backend is built with **ASP.NET Core**, adhering to **Clean Architecture** principles and using **Entity Framework Core** for database interactions.

### 🔧 Capabilities

* Exposes a **REST API** for managing orders
* Generates PDFs with **QuestPDF** for:

  * All Orders Report
  * Order Details Report
* Validates addresses using **Google Maps API**

### 🧰 Technologies Used

* ASP.NET Core
* Entity Framework Core
* MediatR (CQRS pattern)
* QuestPDF
* Google Maps API

---

## 🖥️ Frontend

The frontend is implemented using **React** with **Ant Design** for UI components and **React hooks** for state management.

### 🔧 Capabilities

* Address autocomplete via Google Maps API
* Dynamic forms for order creation/editing
* Interactive map for displaying validated addresses
* CRUD functionality for orders and order details

### 🧰 Technologies Used

* React
* Ant Design
* Axios

---

## 🧪 Unit Tests

The backend includes **unit tests** for key services and controllers using **XUnit**, with dependencies mocked using **Moq**.

> 🚧 Unit tests are still under active development.

---

## 🚀 Getting Started

### 📋 Prerequisites

* [.NET SDK 5.0](https://dotnet.microsoft.com/download) or later
* [Node.js 14](https://nodejs.org/en/download) or later
* Google Maps API Key (set in `.env` file)

---

### 🛠️ Running the App Locally

1. **Clone the repository**

   ```bash
   git clone https://github.com/yourusername/OrderManagementSystem.git
   ```

2. **Backend Setup**

   ```bash
   cd Backend
   dotnet restore
   dotnet run
   ```

3. **Frontend Setup**

   ```bash
   cd ../Frontend
   npm install
   npm start
   ```

4. **Open the app**

Frontend: Visit your configured frontend URL,
http://localhost:5173 (or whichever port you've set manually)

Backend: Ensure your API is running at the expected backend URL,
http://localhost:7021 (or your configured backend port)

🔧 Make sure the frontend knows the backend URL. If needed, configure it in your .env file or through environment variables.
