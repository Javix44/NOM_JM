import { useState, useEffect } from 'react';
import {
  fetchOrders, fetchCustomers, fetchEmployees,
  fetchProducts, PrintAllOrdersReport,
  PrintOrderDetailReport, deleteOrder
} from '../service/api';
import { message } from 'antd';
import { Order, Customer, Employee, Product } from '../service/types';
import { addOrEditProduct } from '../utils/productUtils';
import { OrderDetails } from '../service/types';
import axios from 'axios';
import { Dayjs } from 'dayjs';

export const useOrderData = () => {
  const [orders, setOrders] = useState<Order[]>([]);
  const [selectedOrder, setSelectedOrder] = useState<Order | null>(null);
  const [selectedIndex, setSelectedIndex] = useState(0);
  const [customers, setCustomers] = useState<Customer[]>([]);
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [editingIndex, setEditingIndex] = useState<number | null>(null);
  const [orderDetails, setOrderDetails] = useState<OrderDetails[]>([]);
  const [productModalVisible, setProductModalVisible] = useState(false);
  const [selectedCustomer, setSelectedCustomer] = useState<string>('');
  const [selectedEmployee, setSelectedEmployee] = useState<number | null>(null);
  const [orderDate, setOrderDate] = useState<Dayjs | null>(null);
  const [shipAddress, setShipAddress] = useState<string>('');
  
  useEffect(() => {
    const load = async () => {
      try {
        const [ordersData, customersData, employeesData, productsData] = await Promise.all([
          fetchOrders(),
          fetchCustomers(),
          fetchEmployees(),
          fetchProducts()
        ]);
        setOrders(ordersData);
        setCustomers(customersData);
        setEmployees(employeesData);
        setProducts(productsData);
        if (ordersData.length > 0) {
          setSelectedOrder(ordersData[0]);
        }
      } catch (e) {
        console.error('Data load error', e);
      } finally {
        setLoading(false);
      }
    };
    load();
  }, []);

  //-----------------------ORDER TOOL BAR NAVEGATION----------------------------

  const goNext = () => {
    const next = selectedIndex + 1;
    if (next < orders.length) {
      setSelectedIndex(next);
      setSelectedOrder(orders[next]);
    }
  };

  const goPrev = () => {
    const prev = selectedIndex - 1;
    if (prev >= 0) {
      setSelectedIndex(prev);
      setSelectedOrder(orders[prev]);
    }
  };
  const goFirst = () => {
    if (orders.length > 0) {
      setSelectedIndex(0);
      setSelectedOrder(orders[0]);
    }
  };

  const goLast = () => {
    if (orders.length > 0) {
      const last = orders.length - 1;
      setSelectedIndex(last);
      setSelectedOrder(orders[last]);
    }
  };
  //-----------------------REPORTS----------------------------

  const handleGenerateReport = async () => {
    try {
      const response = await PrintAllOrdersReport();
      const blob = new Blob([response], { type: 'application/pdf' });
      const link = document.createElement('a');
      link.href = URL.createObjectURL(blob);
      link.download = 'AllOrdersReport.pdf';
      link.click();
    } catch (error) {
      console.error('Error generating report:', error);
    }
  };

  const handleGenerateIndividualReport = async (orderId: number) => {
    try {
      const response = await PrintOrderDetailReport(orderId);
      const blob = new Blob([response], { type: 'application/pdf' });
      const link = document.createElement('a');
      link.href = URL.createObjectURL(blob);
      link.download = `Order_${orderId}_DetailsReport.pdf`;
      link.click();
    } catch (error) {
      console.error('Error generating report:', error);
    }
  };

  //-----------------------FETCH ORDERS----------------------------

  const reloadOrders = async () => {
    const updated = await fetchOrders();
    setOrders(updated);
    return updated;
  };


  //-----------------------CRUD ORDERS----------------------------

  // Function to handle the initial creation of a new order (cleaning the form)
  const handleNewOrder = () => {
    setSelectedOrder(null);
    setSelectedCustomer('');
    setSelectedEmployee(null);
    setOrderDate(null);
    setShipAddress('');
    setOrderDetails([]);
    setSelectedIndex(-1);
  };

  // Function to handle the save of an order or the update of an existing order
  const handleSaveOrder = async () => {
    const payload = {
      orderId: selectedOrder?.orderId,
      customerId: selectedCustomer,
      employeeId: selectedEmployee,
      orderDate: orderDate?.toISOString(),
      shipAddress,
    };

    try {
      if (selectedOrder?.orderId) {
        // Update
        await axios.patch(`/api/orders/${selectedOrder.orderId}`, payload);
        message.success('Order updated successfully');
      } else {
        // Create
        const response = await axios.post('/api/orders', payload);
        const newOrderId = response.data;
        message.success(`Order created with ID ${newOrderId}`);
        reloadOrders();
      }
    } catch (error) {
      console.error('Error saving order:', error);
      message.error('Failed to save order');
    }
  };

  // Function to handle the delete of an order 
  const handleOrderDelete = async () => {
    if (!selectedOrder) return;
    await deleteOrder(selectedOrder?.orderId);
    const updated = await reloadOrders();
    setSelectedOrder(updated[0] ?? null);
  };

  // Function to handle the addition or editing of a product in the order details
  const handleAddOrEditProduct = (product: Product) => {
    if (!selectedOrder) return;
    const updated = addOrEditProduct(product, orderDetails, editingIndex, selectedOrder?.orderId);
    setOrderDetails(updated);
    setProductModalVisible(false);
    setEditingIndex(null);

  };

  return {
    orders, customers, employees, products,
    selectedOrder, selectedIndex,
    setSelectedOrder, setSelectedIndex,
    loading, goNext, goPrev, goFirst, goLast,
    handleGenerateReport, handleGenerateIndividualReport,
    handleOrderDelete, handleAddOrEditProduct,
    orderDetails, setOrderDetails,
    productModalVisible, setProductModalVisible,
    editingIndex, setEditingIndex, handleNewOrder,
    handleSaveOrder, setShipAddress, shipAddress,selectedCustomer,
    setSelectedCustomer,selectedEmployee, setSelectedEmployee,
    orderDate, setOrderDate,
  };
};
