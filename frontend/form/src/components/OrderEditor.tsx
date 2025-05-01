import React, { useState } from 'react';
import OrderDetailsForm from './OrderDetailsForm';
import ProductModal from './ProductModal';
import { Product, OrderDetails } from '../service/types';
import Swal from 'sweetalert2';

const OrderEditor = ({ products }: { products: Product[] }) => {
  const [orderDetails, setOrderDetails] = useState<OrderDetails[]>([]);
  const [modalVisible, setModalVisible] = useState(false);
  const [editingIndex, setEditingIndex] = useState<number | null>(null);

  const handleSelectProduct = (product: Product) => {
    const isDuplicate = orderDetails.some(
      (item, idx) => item.productId === product.productId && idx !== editingIndex
    );

    if (isDuplicate) {
      Swal.fire('Addition Error', 'This product is already added.', 'warning');
      return;
    }

    const updated = [...orderDetails];

    if (editingIndex !== null) {
      updated[editingIndex] = {
        ...updated[editingIndex],
        productId: product.productId,
        unitPrice: product.unitPrice,
      };
    } else {
      updated.push({
        productId: product.productId,
        unitPrice: product.unitPrice,
        quantity: 1,
        orderId: 0,
      });
    }

    setOrderDetails(updated);
    setEditingIndex(null);
    setModalVisible(false);
  };

  return (
    <>
      <OrderDetailsForm
        orderDetails={orderDetails}
        setOrderDetails={setOrderDetails}
        products={products}
        selectedProduct={null}
        onOpenModal={() => setModalVisible(true)}
        setEditingIndex={setEditingIndex}
      />

      <ProductModal
        visible={modalVisible}
        products={products}
        onSelect={handleSelectProduct}
        onCancel={() => setModalVisible(false)}
        loading={false}
      />
    </>
  );
};

export default OrderEditor;
