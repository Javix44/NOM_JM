
export type Customer = {
  customerId: string;
  companyName: string;
};

export type Employee = {
  employeeId: number;
  firstName: string;
  lastName: string;
};

export type Product = {
  productId: number;
  productName: string;
  unitPrice: number;
  UnitsInStock: number;
};

export type Order = {
  orderId?: number;
  customerId?: string;
  customer?: Customer;
  employeeId: number;
  employee?: Employee;
  orderDate?: string;
  shipAddress?: string;
  orderDetails?: OrderDetails[];
};

export type OrderDetails = {
  orderId?: number;
  productId: number;
  productName?: string;  
  quantity: number;
  unitPrice: number;  
};
