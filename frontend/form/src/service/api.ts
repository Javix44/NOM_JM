const API_BASE = 'https://localhost:7021/api';
import { Customer, Employee, Product, Order, OrderDetails } from '../service/types';

//FETCH DATA FROM TABLES TO DROPDOWNS
// Customers, Employees and Products
export const fetchCustomers = async (): Promise<Customer[]> => {
  const res = await fetch(`${API_BASE}/customers`);
  return res.json();
};

export const fetchEmployees = async (): Promise<Employee[]> => {
  const res = await fetch(`${API_BASE}/employees`);
  return res.json();
};

export const fetchProducts = async (): Promise<Product[]> => {
  const res = await fetch(`${API_BASE}/products`);
  return res.json();
};

//REPORTS
export const PrintAllOrdersReport = async (): Promise<Blob> => {
  const res = await fetch(`${API_BASE}/reports/all-orders`);
  if (!res.ok) {
    throw new Error('Failed to fetch report');
  }
  return res.blob();  // Debe retornar un Blob con el PDF
};

export const PrintOrderDetailReport = async (orderId: number): Promise<Blob> => {
  const res = await fetch(`${API_BASE}/reports/order-details-report/${orderId}`);
  if (!res.ok) {
    throw new Error('Failed to fetch report');
  }
  return res.blob();  // Debe retornar un Blob con el PDF
};


//CRUD ORDERS
export const fetchOrders = async (): Promise<Order[]> => {
  const res = await fetch(`${API_BASE}/orders`);
  return res.json();
};

export const fetchOrderById = async (id: number): Promise<Order> => {
  const res = await fetch(`${API_BASE}/orders/${id}`);
  return res.json();
};

export const createOrder = async (order: Order, details: OrderDetails[]): Promise<void> => {
  await fetch(`${API_BASE}/orders`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ order, details }),
  });
};

export const updateOrder = async (id: number, order: Order): Promise<void> => {
  await fetch(`${API_BASE}/orders/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(order),
  });
};

export const deleteOrder = async (id?: number): Promise<void> => {
  await fetch(`${API_BASE}/orders/${id}`, {
    method: 'DELETE',
  });
};
