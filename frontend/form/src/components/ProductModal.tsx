import { Modal, Table, Input } from 'antd';
import { Product } from '../service/types';
import { useState } from 'react';

type Props = {
  visible: boolean;
  products: Product[];
  onCancel: () => void;
  loading: boolean;
  onSelectProduct: (product: Product) => void;
};

const ProductModal = ({ visible, products, onCancel, loading,onSelectProduct }: Props) => {
  const [searchText, setSearchText] = useState('');

  const filteredProducts = products.filter(product =>
    product.productName.toLowerCase().includes(searchText.toLowerCase())
  );

  return (
    <Modal
      open={visible}
      onCancel={onCancel}
      footer={null}
      title="Select Product"
    >
      <Input
        placeholder="Search products..."
        style={{ marginBottom: 10 }}
        value={searchText}
        onChange={e => setSearchText(e.target.value)}
      />
      <Table
        loading={loading}
        dataSource={filteredProducts}
        rowKey="productId"
        onRow={(record) => ({
          onClick: () => onSelectProduct(record)
        })}
        columns={[
          {
            title: 'Product',
            dataIndex: 'productName',
            key: 'productName',
          },
          {
            title: 'Price',
            dataIndex: 'unitPrice',
            key: 'unitPrice',
            render: (price: number) => `$${price}`,
          },
          {
            title: 'UnitsInStock',
            dataIndex: 'unitsInStock',
            key: 'unitsInStock',
            render: (Qty: number) => `${Qty}`
          }
        ]}
        pagination={false}
      />
    </Modal>
  );
};

export default ProductModal;
