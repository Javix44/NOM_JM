// /utils/productUtils.ts
import { OrderDetails, Product } from '../service/types';
import { Modal } from 'antd';

export const addOrEditProduct = (
  product: Product,
  prevDetails: OrderDetails[],
  editingIndex: number | null,
  orderId?: number
): OrderDetails[] => {
  if (editingIndex !== null) {
    const updated = [...prevDetails];
    const isDuplicate = prevDetails.some((item, idx) =>
      item.productId === product.productId && idx !== editingIndex
    );
    if (isDuplicate) {
      Modal.warning({ title: 'Product already added', content: `Product ${product.productName} is already in the order.` });
      return prevDetails;
    }
    updated[editingIndex] = { ...updated[editingIndex], productId: product.productId, unitPrice: product.unitPrice ?? 0 };
    return updated;
  }

  const alreadyExists = prevDetails.some(item => item.productId === product.productId);
  if (alreadyExists) {
    Modal.warning({ title: 'Product already added', content: `Product ${product.productName} is already in the order.` });
    return prevDetails.map(item =>
      item.productId === product.productId
        ? { ...item, quantity: item.quantity + 1 }
        : item
    );
  }

  return [...prevDetails, { productId: product.productId, quantity: 1, unitPrice: product.unitPrice ?? 0, orderId }];
};
