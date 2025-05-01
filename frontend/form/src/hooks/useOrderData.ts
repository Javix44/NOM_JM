import { useState, useEffect } from 'react';
import {
  fetchOrders, fetchCustomers, fetchEmployees,
  fetchProducts, PrintAllOrdersReport,
  PrintOrderDetailReport, deleteOrder,
  updateOrder,
  createOrder
} from '../service/api';
import { Order, Customer, Employee, Product } from '../service/types';
import { addOrEditProduct } from '../utils/productUtils';
import { OrderDetails } from '../service/types';
import { Dayjs } from 'dayjs';
import Swal from 'sweetalert2';

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
    if (!selectedEmployee || !selectedCustomer || !orderDate || !shipAddress) {
      Swal.fire('Validation Error', 'Please complete all required fields.', 'warning');
      return;
    }
    try {
      if (selectedOrder?.orderId) {
        // Actualizar
        const payload: Order = {
          orderId: selectedOrder.orderId,
          customerId: selectedCustomer,
          employeeId: selectedEmployee,
          orderDate: orderDate.toISOString(),
          shipAddress
        };
        await updateOrder(selectedOrder.orderId, payload);
        Swal.fire('Updated', 'Order updated successfully.', 'success');
        reloadOrders();
      } else {
        // Crear
        const createPayload = {
          customerId: selectedCustomer,
          employeeId: selectedEmployee,
          orderDate: orderDate.toISOString(),
          shipAddress,
          orderDetails
        };
        const newOrderId = await createOrder(createPayload);
        Swal.fire('Created', `Order #${newOrderId} created successfully.`, 'success');
        // Recarga órdenes y selecciona el nuevo registro
        const updated = await reloadOrders();
        const newIndex = updated.findIndex(o => o.orderId === newOrderId);
        if (newIndex !== -1) {
          setSelectedIndex(newIndex);
          setSelectedOrder(updated[newIndex]);
        }
      }
    } catch (error) {
      console.error('Error saving order:', error);
      Swal.fire('Error', 'Failed to save order.', 'error');
    }
  };


  // Function to handle the delete of an order 
  const handleOrderDelete = async () => {
    if (!selectedOrder) return;

    const result = await Swal.fire({
      title: 'Are you sure?',
      text: 'Do you really want to delete this order?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes, delete it!',
    });

    if (result.isConfirmed) {
      try {
        await deleteOrder(selectedOrder.orderId);
        const updated = await reloadOrders();
        setSelectedOrder(updated[0] ?? null);
        Swal.fire('Deleted!', 'The order has been deleted.', 'success');
      } catch (error) {
        Swal.fire('Error', 'Failed to delete the order: ' + (error as Error).message, 'error');
      }
    }
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
    handleSaveOrder, setShipAddress, shipAddress, selectedCustomer,
    setSelectedCustomer, selectedEmployee, setSelectedEmployee,
    orderDate, setOrderDate,
  };
};
