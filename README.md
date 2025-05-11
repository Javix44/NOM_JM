# 🧾 Order Management System – Northwind Traders

> A full-stack web application for managing customer orders, built with **ASP.NET Core** and **React**.

> 📘 For complete technical documentation, refer to the North_Order_Mang_Documentation file located at the root level alongside this README.

---

## 🧩 Tech Stack

| Technology                                                                                            | Description                                          |               
| ----------------------------------------------------------------------------------------------------- | ---------------------------------------------------- | 
| ![oaicite:26](https://img.shields.io/badge/ASP.NET_Core-Backend-blue?logo=dotnet)                     | Backend REST API using Clean Architecture principles |               
| ![oaicite:28](https://img.shields.io/badge/Entity_Framework_Core-ORM-green?logo=dotnet)               | Object-Relational Mapping for database interactions  |               
| ![oaicite:30](https://img.shields.io/badge/MediatR-CQRS-purple)                                       | Implements the CQRS pattern for request handling     |              
| ![oaicite:32](https://img.shields.io/badge/React-Frontend-blue?logo=react)                            | Frontend library for building user interfaces        |              
| ![oaicite:34](https://img.shields.io/badge/Ant_Design-UI_Framework-orange)                            | UI components for React applications                 |              
| ![oaicite:36](https://img.shields.io/badge/Axios-HTTP_Client-lightgrey)                               | Promise-based HTTP client for the browser            |               
| ![oaicite:38](https://img.shields.io/badge/QuestPDF-PDF_Generation-red)                               | Library for generating PDF documents                 |               
| ![oaicite:40](https://img.shields.io/badge/Google_Maps_API-Address_Validation-yellow?logo=googlemaps) | Autocomplete and geolocation services                |               

---

## 📚 Features

* **Order Management**: Create, read, update, and delete orders and order details.
* **Address Validation**: Utilize Google Maps API for address autocomplete and validation.
* **PDF Reports**: Generate comprehensive PDF reports for all orders and specific order details using QuestPDF.
* **Responsive UI**: User-friendly interface built with React and Ant Design components.
* **Unit Testing**: Backend services and controllers are tested using XUnit and Moq.

---

## 🚀 Getting Started

### 📋 Prerequisites

* [.NET SDK 5.0](https://dotnet.microsoft.com/download) or later
* [Node.js 14](https://nodejs.org/en/download) or later
* Google Maps API Key (set in `.env` file)
* Northwind Database (File `instnwnd.sql`)

### 🛠️ Installation

1. **Clone the repository**:

   ```bash
   git clone https://github.com/yourusername/OrderManagementSystem.git
   ```

2. **Backend Setup**:

   ```bash
   cd OrderManagementSystem/Backend
   dotnet restore
   dotnet run
   ```

   The backend will run at: `http://localhost:7021`

3. **Frontend Setup**:

   ```bash
   cd ../Frontend
   npm install
   npm start
   ```

   The frontend will run at: `http://localhost:5173`

4. **Environment Configuration**:

   Create a `.env` file in the `Frontend` directory and add your Google Maps API key:

   ```env
   REACT_APP_GOOGLE_MAPS_API_KEY=your_api_key_here
   ```
   
   Add your Google Maps API key in `appsettings.Development.json` file in the `Backend`:
   ```env
   "GoogleMaps": {
    "ApiKey": "your_api_key_here"
   }   
   ```

5. Running the App
 
   There is a `tasks.json` file in the .vscode folder that allows you to run the backend, frontend, and tests individually:
   * Press F1 in Visual Studio Code.
   * Select "Tasks: Run Task".
   * Choose the task you want to execute (backend, frontend, or tests).
    
---

## 🔌 API Endpoints

| Method | Endpoint                                      | Description                                    |                                    
| ------ | --------------------------------------------- | ---------------------------------------------- | 
| GET    | `/api/customers`                              | Retrieve all customers                         |                                    
| GET    | `/api/employees`                              | Retrieve all employees                         |                                    
| GET    | `/api/products`                               | Retrieve all products                          |                                    
| GET    | `/api/orders`                                 | Retrieve all orders                            |                                    
| GET    | `/api/orders/{id}`                            | Retrieve a specific order by ID                |                                   
| POST   | `/api/orders`                                 | Create a new order                             |                                    
| PATCH  | `/api/orders/{id}`                            | Update an existing order                       |                                    
| DELETE | `/api/orders/{id}`                            | Delete an order                                |                                    
| POST   | `/api/orders/validate-address`                | Validate shipping address using Google Maps    |                                    
| GET    | `/api/orderdetails`                           | Retrieve all order details                     |                                    
| GET    | `/api/orderdetails/{orderId}/{productId}`     | Retrieve specific order detail                 |                                   
| GET    | `/api/orderdetails/order/{orderId}`           | Retrieve all order details for an order        |                                    
| POST   | `/api/orderdetails`                           | Create a new order detail                      |                                    
| PATCH  | `/api/orderdetails/{orderId}/{productId}`     | Update an existing order detail                |                                   
| DELETE | `/api/orderdetails/{orderId}/{productId}`     | Delete an order detail                         |                                    
| GET    | `/api/reports/all-orders`                     | Generate PDF report for all orders             |                                   
| GET    | `/api/reports/order-details-report/{orderId}` | Generate PDF report for specific order details | 

---

## 🧪 Testing

* **XUnit**: Unit tests for backend services and controllers.
* **Moq**: Mocking dependencies for isolated testing.

---

## 📁 Project Structure

```
OrderManagementSystem/
├── Backend/
│   ├── API/
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
│   └── ...
├── Frontend/
│   ├── public/
│   ├── src/
│   │   ├── components/
│   │   ├── pages/
│   │   ├── services/
│   │   ├── hooks/
│   │   └── ...
│   └── ...
└── README.md
```


---

## 👩‍💻 Author

**Javier Alexander Martínez Vásquez**

> Developed as part of the RSM Trainee Program Final Project 2025.
