import React, { useState, useEffect } from 'react';
import { Row, Col, Space, Divider, Typography, Input, Button, DatePicker, Spin } from 'antd';
import { CheckOutlined, FileOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';

//-----------------------CUSTOM HOOK WITH ALL DATA OF UTILS/SERVICES----------------------------
import { useOrderData } from '../hooks/useOrderData';
//-----------------------NAGEVATION/CREATION COMPONENTS----------------------------
import OrderToolbar from '../components/OrderToolbar';
//-----------------------MAP COMPONENT CARD----------------------------
import ValidatedAddressCard from '../components/ValidatedAddressCard';
//-----------------------SELECT COMPONENTS----------------------------
import CustomerSelect from '../components/CustomerSelect';
import EmployeeSelect from '../components/EmployeeSelect';
import OrderDetailsForm from '../components/OrderDetailsForm';
//-----------------------MODALS----------------------------  
import ProductModal from '../components/ProductModal';
import OrderSearchModal from '../components/OrderSearchModal';
//-----------------------CSS STYLES----------------------------
import '../styles/OrderManagementUI.css';

const { Title } = Typography;

const OrderManagementUI = () => {
  const {
    //data
    orders, customers, employees, products, shipAddress,
    setShipAddress, selectedEmployee, setSelectedEmployee,
    selectedCustomer, setSelectedCustomer, orderDate, setOrderDate,
    //selected data order and index
    selectedOrder, selectedIndex, setSelectedOrder, setSelectedIndex,
    //navigation functions
    loading, goNext, goPrev, goFirst, goLast,
    //Reports
    handleGenerateReport, handleGenerateIndividualReport,
    //CRUD functions ORDERS
    handleOrderDelete, handleNewOrder, handleSaveOrder,
    //CRUD functions ORDERDETAILS
    handleSaveAllOrderDetails, handleProductSelect, handleOrderDetailDelete,
    //Constants utility functions
    orderDetails, setOrderDetails,
    productModalVisible, setProductModalVisible,
    setEditingIndex,
  } = useOrderData();

  const [searchModalVisible, setSearchModalVisible] = useState(false);

  //DATA FETCHING
  //useEffect to set the selected order and its details when the selectedOrder changes
  useEffect(() => {
    if (selectedOrder) {
      setSelectedCustomer(selectedOrder.customerId ?? '');
      setSelectedEmployee(selectedOrder.employeeId ?? null);
      setOrderDate(selectedOrder.orderDate ? dayjs(selectedOrder.orderDate) : null);
      setShipAddress(selectedOrder.shipAddress ?? '');
      setOrderDetails(selectedOrder.orderDetails ?? []);
    }
  }, [selectedOrder]);

  //Loading state while fetching data
  if (loading) {
    return <Spin tip="Loading..." size="large" />;
  }

  return (
    <div className="order-management-container">
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <Title level={2} style={{ marginTop: 0, marginBottom: 5 }}>Order Options</Title>
        <span style={{ fontSize: 30, fontStyle: 'bold' }}>
          {selectedOrder ? `Order #${selectedOrder.orderId}` : 'New Order Creation'}</span>
      </div>
      <OrderToolbar
        onPrev={goPrev}
        onNew={handleNewOrder}
        onSave={handleSaveOrder}
        onNext={goNext}
        onFirst={goFirst}
        onLast={goLast}
        onDelete={handleOrderDelete}
        onGenerateReport={handleGenerateReport}
        onSearch={() => setSearchModalVisible(true)}
        canPrev={selectedIndex > 0}
        canNext={selectedIndex < orders.length - 1}
      />
      <Divider />

      <Row gutter={24}>
        <Col span={8}>
          <Space direction="vertical" style={{ width: '100%' }}>
            <CustomerSelect
              customers={customers}
              selectedCustomer={selectedCustomer}
              onChange={setSelectedCustomer}
            />
            <label style={{ fontSize: '18px', fontWeight: 'bold' }}>Order Date</label>
            <DatePicker
              className="custom-placeholder"
              value={orderDate}
              onChange={(date) => setOrderDate(date)}
            />
          </Space>
        </Col>
        <Col span={2}></Col>
        <Col span={14}>
          <Space direction="vertical" style={{ width: '100%' }}>
            <label style={{ fontSize: '18px', fontWeight: 'bold' }}>Shipping Address</label>
            <Space>
              <Input
                placeholder="Shipping address"
                className="custom-placeholder"
                value={shipAddress}
                onChange={(e) => setShipAddress(e.target.value)}
              />
              <Button icon={<CheckOutlined />} />
            </Space>
            <EmployeeSelect
              employees={employees}
              selectedEmployee={selectedEmployee}
              onChange={setSelectedEmployee}
            />
          </Space>
        </Col>
      </Row>

      <Divider />
      <Space direction="horizontal" align="center">
        <Title level={2} style={{ margin: 0 }}>Line Details</Title>
        <Button type="primary" className="btn-generate btn-new"
          onClick={() =>
            selectedOrder?.orderId !== undefined &&
            handleGenerateIndividualReport(selectedOrder.orderId)}
        >
          <FileOutlined /> Generate This Report
        </Button>
      </Space>

      <OrderDetailsForm
        orderDetails={orderDetails}
        setOrderDetails={setOrderDetails}
        products={products}
        selectedProduct={null}
        onOpenModal={() => setProductModalVisible(true)}
        setEditingIndex={setEditingIndex}
        onSaveDetails={handleSaveAllOrderDetails}
        onDeleteLine={handleOrderDetailDelete}
      />

      <Divider />
      <ValidatedAddressCard />

      <ProductModal
        visible={productModalVisible}
        products={products}
        onCancel={() => setProductModalVisible(false)}
        loading={loading}
        onSelectProduct={handleProductSelect}
      />

      <OrderSearchModal
        visible={searchModalVisible}
        orders={orders}
        onCancel={() => setSearchModalVisible(false)}
        onSelect={(order) => {
          const index = orders.findIndex(o => o.orderId === order.orderId);
          setSelectedIndex(index);
          setSelectedOrder(order);
          setSearchModalVisible(false);
        }}
      />
    </div>
  );
};

export default OrderManagementUI;
