import { useState, useEffect } from 'react';
import {
  fetchOrders, fetchCustomers, fetchEmployees,
  fetchProducts, PrintAllOrdersReport,
  PrintOrderDetailReport, deleteOrder,
  updateOrder,
  createOrder,
  updateOrderDetails,
  createOrderDetails,
  deleteOrderDetails,
  fetchOrderDetailsByOrderId,
  fetchOrderDetailByIdQuery,
  validateAddress
} from '../service/api';
import { Order, Customer, Employee, Product, ValidatedAddress } from '../service/types';
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
  const [validatedData, setValidatedData] = useState<ValidatedAddress | null>(null);
  const [validationError, setValidationError] = useState(false);
  const [validationMessage, setValidationMessage] = useState<string | null>(null);
  const [mapLoading, setMapLoading] = useState(false);

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
        handleValidate();
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

  //-----------------------CRUD ORDER DETAILS----------------------------

  // Function to handle the addition or editing of a product in the order details
  const handleSaveOrderDetail = async (
    product: Product,
    quantity: number,
    unitPrice: number,
    originalProductId?: number
  ) => {
    const orderId = selectedOrder?.orderId;
    const newProductId = product?.productId;
    const originalProductI = originalProductId ?? newProductId;

    if (!orderId || !newProductId) {
      Swal.fire('Error', 'Missing order or product information.', 'error');
      return;
    }

    if (quantity <= 0 || unitPrice <= 0) {
      Swal.fire('Validation Error', 'Please provide valid quantity and unit price.', 'warning');
      return;
    }

    const detailPayload: OrderDetails = {
      orderId,
      productId: newProductId,
      quantity,
      unitPrice,
      originalProductId: originalProductI,
    };

    let updated = false;

    try {
      // Si el producto fue cambiado (editar con reemplazo)
      if (originalProductId && originalProductId !== newProductId) {
        const exists = await fetchOrderDetailByIdQuery(orderId, newProductId);
        if (exists) {
          Swal.fire('Error', 'This product already exists in the order.', 'warning');
          return;
        }

        await deleteOrderDetails(orderId, originalProductId);
        await createOrderDetails(detailPayload);
        Swal.fire('Updated', 'Order detail updated with new product.', 'success');

      } else {
        // Mismo producto: actualizar solo si cambió, crear si no existe
        const existingDetail = await fetchOrderDetailByIdQuery(orderId, newProductId);

        if (existingDetail) {
          const hasChanges =
            existingDetail.quantity !== quantity ||
            existingDetail.unitPrice !== unitPrice;

          if (hasChanges) {
            await updateOrderDetails(orderId, newProductId, detailPayload);
          } else {
            console.log('⚠️ No se detectaron cambios, no se actualiza.');
          }

        } else {
          await createOrderDetails(detailPayload);
          Swal.fire('Created', 'Order detail added successfully.', 'success');
        }
      }

      updated = true;

    } catch (error) {
      console.error('Error en guardado de detalle:', error);
      Swal.fire('Error', 'Failed to save order detail.', 'error');
      return;
    }

    if (updated) {
      try {
        const updatedDetails = await fetchOrderDetailsByOrderId(orderId);
        setOrderDetails(updatedDetails);
        setProductModalVisible(false);
      } catch (e) {
        console.error('Error actualizando vista de detalles:', e);
      }
    }
  };

  // Functions to handle the save of all order details by modal
  const handleSaveAllOrderDetails = async () => {
    if (!selectedOrder || !selectedOrder.orderId) return;

    let hasErrors = false;

    try {
      for (const detail of orderDetails) {
        const product = products.find(p => p.productId === detail.productId);
        if (product) {
          try {
            console.log('🔎 Orden a guardar:', detail);
            await handleSaveOrderDetail(product, detail.quantity, detail.unitPrice, detail.originalProductId);
          } catch (error) {
            console.error('Error guardando detalle:', error);
            hasErrors = true;
          }
        }
      }
      if (!hasErrors) {
        Swal.fire('Success', 'All order details saved successfully.', 'success');
      } else {
        Swal.fire('Partial Success', 'Some order details could not be saved.', 'warning');
      }
    } catch (error) {
      console.error('Error general:', error);
      Swal.fire('Error', 'An error occurred while saving order details.', 'error');
    }
  };


  const handleProductSelect = (product: Product) => {
    if (editingIndex !== null) {
      const updated = [...orderDetails];
      const originalProductId = updated[editingIndex].productId;

      updated[editingIndex] = {
        ...updated[editingIndex],
        productId: product.productId,
        unitPrice: product.unitPrice,
        originalProductId,
      };
      setOrderDetails(updated);
    } else {
      const newDetail: OrderDetails = {
        orderId: selectedOrder?.orderId ?? 0,
        productId: product.productId,
        quantity: 1,
        unitPrice: product.unitPrice,
        originalProductId: product.productId,
      };
      setOrderDetails([...orderDetails, newDetail]);
    }

    setProductModalVisible(false);
  };

  // Function to handle the delete of an order 
  const handleOrderDetailDelete = async (index: number) => {
    const detail = orderDetails[index]; // obtenemos el detalle desde el estado
    const orderId = selectedOrder?.orderId;

    // Verificamos que el detalle tiene los campos requeridos
    if (!orderId || !detail.productId) {
      Swal.fire('Error', 'Missing order or product information for deletion.', 'error');
      return;
    }

    const result = await Swal.fire({
      title: 'Are you sure?',
      text: 'Do you really want to delete this order detail?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes',
    });

    if (result.isConfirmed) {
      try {
        await deleteOrderDetails(orderId, detail.productId);
        const updated = [...orderDetails];
        updated.splice(index, 1); // Removemos el detalle de la vista
        setOrderDetails(updated); // Actualizamos el estado
        Swal.fire('Deleted!', 'The order detail has been deleted.', 'success');
      } catch (error) {
        Swal.fire(
          'Error',
          'Failed to delete the order detail: ' + (error as Error).message,
          'error'
        );
      }
    }
  };

  //-----------------------GOOGLE MAPS----------------------------
  const handleValidate = async () => {
    setMapLoading(true);
    setValidationError(false);
    try {
      const data = await validateAddress(shipAddress);
      if (data) {
        setValidatedData(data);
        setValidationError(false);
      } else {
        setValidatedData(null);
        setValidationError(true);
      }
    } catch (error) {
      console.error('Error validando dirección:', error);
      setValidatedData(null);
      setValidationError(true);
    } finally {
      setMapLoading(false);
    }
  };

  useEffect(() => {
    if (selectedOrder?.shipAddress) {
      setShipAddress(selectedOrder.shipAddress);
      setMapLoading(true);
      setValidationError(false);
      validateAddress(selectedOrder.shipAddress)
        .then(data => {
          if (data) {
            setValidatedData(data);
            setValidationError(false);
          } else {
            setValidatedData(null);
            setValidationError(true);
          }
        })
        .catch(err => {
          console.error('Error validando dirección:', err);
          setValidatedData(null);
          setValidationError(true);
        })
        .finally(() => setMapLoading(false));
    } else {
      setShipAddress('');
      setValidatedData(null);
      setValidationError(false);
      setMapLoading(false);
    }
  }, [selectedOrder]);

  const handleAddressAutocompleteSelect = async (address: string) => {
    setShipAddress(address);
    try {
      const validated = await validateAddress(address);
      setValidatedData(validated);
      setValidationError(false);
      setValidationMessage(null);
    } catch (error) {
      setValidatedData(null);
      setValidationError(true);
      setValidationMessage('Invalid address');
      console.error('Error validating address:', (error as Error).message);
    }
  };

  return {
    orders, customers, employees, products,
    selectedOrder, selectedIndex,
    setSelectedOrder, setSelectedIndex,
    loading, goNext, goPrev, goFirst, goLast,
    handleGenerateReport, handleGenerateIndividualReport,
    handleOrderDelete,
    orderDetails, setOrderDetails,
    productModalVisible, setProductModalVisible,
    editingIndex, setEditingIndex, handleNewOrder,
    handleSaveOrder, setShipAddress, shipAddress, selectedCustomer,
    setSelectedCustomer, selectedEmployee, setSelectedEmployee,
    orderDate, setOrderDate,
    handleSaveOrderDetail, handleOrderDetailDelete, handleSaveAllOrderDetails,
    handleProductSelect, handleValidate, validatedData, mapLoading, validationError, handleAddressAutocompleteSelect
  };
};