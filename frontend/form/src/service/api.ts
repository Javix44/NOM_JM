const API_BASE = 'https://localhost:7021/api';
import { Customer, Employee, Product, Order, OrderDetails } from '../service/types';
//-----------------------FETCH DATA FROM TABLES TO DROPDOWNS----------------------------
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

//-----------------------REPORTS----------------------------

export const PrintAllOrdersReport = async (): Promise<Blob> => {
  const res = await fetch(`${API_BASE}/reports/all-orders`);
  if (!res.ok) {
    throw new Error('Failed to fetch report');
  }
  return res.blob();
};

export const PrintOrderDetailReport = async (orderId: number): Promise<Blob> => {
  const res = await fetch(`${API_BASE}/reports/order-details-report/${orderId}`);
  if (!res.ok) {
    throw new Error('Failed to fetch report');
  }
  return res.blob();
};


//-----------------------CRUD ORDERS----------------------------

export const fetchOrders = async (): Promise<Order[]> => {
  const res = await fetch(`${API_BASE}/orders`);
  return res.json();
};

export const fetchOrderById = async (id: number): Promise<Order> => {
  const res = await fetch(`${API_BASE}/orders/${id}`);
  return res.json();
};

export const createOrder = async (order: Order): Promise<void> => {
  const response = await fetch(`${API_BASE}/orders`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(order)
  });
  const orderId = await response.json();
  return orderId;
};

export const updateOrder = async (id: number, order: Order): Promise<void> => {
  await fetch(`${API_BASE}/orders/${id}`, {
    method: 'PATCH',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(order),
  });
};

export const deleteOrder = async (id?: number): Promise<void> => {
  await fetch(`${API_BASE}/orders/${id}`, {
    method: 'DELETE',
  });
};

//-----------------------CRUD ORDER DETAILS----------------------------

export const fetchOrdersDetails = async (): Promise<OrderDetails[]> => {
  const res = await fetch(`${API_BASE}/orderdetails`);
  return res.json();
};

export const fetchOrderDetailByIdQuery = async (
  orderId: number,
  productId: number
): Promise<OrderDetails | null> => {
  const res = await fetch(`${API_BASE}/orderdetails/${orderId}/${productId}`);

  if (res.status === 404) {
    return null;
  }
  if (!res.ok) {
    const errorText = await res.text();
    throw new Error(`Failed to fetch detail: ${res.status} ${errorText}`);
  }

  return res.json(); // Devuelve el detalle (un objeto)
};


export const fetchOrderDetailsByOrderId = async (orderId: number): Promise<OrderDetails[]> => {
  const res = await fetch(`${API_BASE}/orderdetails/order/${orderId}`);
  return res.json();
};

export const createOrderDetails = async (detail: OrderDetails): Promise<void> => {
  const res = await fetch(`${API_BASE}/orderdetails`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(detail),
  });

  if (!res.ok) {
    const text = await res.text();
    throw new Error(`Failed to create order detail: ${text}`);
  }
};


export const updateOrderDetails = async (
  orderId: number,
  productId: number,
  data: OrderDetails
): Promise<OrderDetails> => {
  const res = await fetch(`${API_BASE}/orderdetails/${orderId}/${productId}`, {
    method: 'PATCH',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to update order detail');
  return res.json();
};

export const deleteOrderDetails = async (
  orderId: number,
  productId: number
): Promise<void> => {
  const res = await fetch(`${API_BASE}/orderdetails/${orderId}/${productId}`, {
    method: 'DELETE',
  });
  if (!res.ok) throw new Error('Failed to delete order detail');
  return res.json();
};

