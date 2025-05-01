import React from 'react';
import { Button, Col, Input, Row, Space, Typography, Divider } from 'antd';
import { CheckSquareFilled, DeleteOutlined, PlusCircleFilled } from '@ant-design/icons';
import { Product, OrderDetails } from '../service/types';

const { Text } = Typography;

type Props = {
  orderDetails: OrderDetails[];
  setOrderDetails: (details: OrderDetails[]) => void;
  products: Product[];
  selectedProduct: Product | null;
  onOpenModal: () => void;
  setEditingIndex: (index: number | null) => void;
  onSaveDetails: () => void;
  onDeleteLine: (index: number) => void;
};

const OrderDetailsForm = ({
  orderDetails,
  setOrderDetails,
  products,
  onOpenModal,
  setEditingIndex,
  onSaveDetails,
  onDeleteLine,
}: Props) => {
  const handleChange = (index: number, field: keyof OrderDetails, value: string | number) => {
    const updated = [...orderDetails];
    const current = updated[index];

    updated[index] = {
      ...current,
      [field]: Number(value),
    };

    setOrderDetails(updated);
  };

  const getProductById = (id: number) => products.find(p => p.productId === id);

  return (
    <div
      style={{
        backgroundColor: '#d4d4d4',
        padding: 24,
        borderRadius: 8,
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
      }}
    >
      <Row gutter={20} style={{ marginBottom: 8, width: '100%', justifyContent: 'center' }}>
        <Col span={7}>
          <Text strong style={{ fontSize: 18 }}>Product</Text>
        </Col>
        <Col span={4}>
          <Text strong style={{ fontSize: 18 }}>Quantity</Text>
        </Col>
        <Col span={4}>
          <Text strong style={{ fontSize: 18 }}>Unit Price</Text>
        </Col>
        <Col span={4}>
          <Text strong style={{ fontSize: 18 }}>Total</Text>
        </Col>
      </Row>

      <Divider style={{ margin: '8px 0', width: '100%' }} />

      {orderDetails.map((line, i) => {
        const product = getProductById(line.productId);
        return (
          <Row
            gutter={20}
            key={i}
            align="middle"
            style={{
              marginBottom: 12,
              width: '100%',
              justifyContent: 'center',
            }}
          >
            <Col span={8}>
              <Button
                style={{ padding: 5, fontSize: 13.5, width: '100%' }}
                onClick={() => {
                  setEditingIndex(i);
                  onOpenModal();
                }}
              >
                {product ? product.productName : 'Select Product'}
              </Button>
            </Col>
            <Col span={4}>
              <Input
                type="number"
                value={line.quantity
                }
                onChange={e => handleChange(i, 'quantity', Number(e.target.value))}
                placeholder="Quantity"
                style={{ fontSize: 15 }}
                min={1}
              />
            </Col>
            <Col span={4}>
              <Input
                className='custom-disabled'
                value={`$${line.unitPrice}`}
                disabled
                placeholder="Price"
                style={{ fontSize: 15 }}
              />
            </Col>
            <Col span={4}>
              <Input
                value={
                  `$${line.quantity * line.unitPrice}`
                }
                disabled
                className='custom-disabled'
                placeholder="Total"
                style={{ fontSize: 15 }}
              />
            </Col>
            <Col span={2}>
              <Button className='btn-delete' onClick={() => onDeleteLine(i)}>
                <DeleteOutlined />
              </Button>
            </Col>
          </Row>
        );
      })}

      <Divider style={{ width: '100%' }} />

      <div style={{ textAlign: 'center' }}>
        <Space>
          <Button
            onClick={() => {
              setEditingIndex(null);
              onOpenModal();
            }}
            type="primary"
            style={{ fontSize: 15 }}
            className='btn-new'
          >
            Add Product
            {<PlusCircleFilled />}
          </Button>
          <Button type="primary"
            className='btn-save'
            onClick={onSaveDetails}
            style={{ fontSize: 15 }}>
            Save Changes
            {<CheckSquareFilled />}
          </Button>
        </Space>
      </div>
    </div>
  );
};

export default OrderDetailsForm;
