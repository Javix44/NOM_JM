import { Modal, Table, Input } from 'antd'; // Removed 'Spin' since it's unused
import { Order } from '../service/types';
import { useState } from 'react';

type Props = {
  visible: boolean;
  orders: Order[];
  onSelect: (order: Order) => void;
  onCancel: () => void;
};

const OrderSearchModal = ({ visible, orders, onSelect, onCancel }: Props) => {
  const [searchText, setSearchText] = useState('');

  const filtered = orders.filter(order =>
    order.orderId?.toString().includes(searchText) ||
    order.customer?.companyName?.toLowerCase().includes(searchText.toLowerCase()) ||
    `${order.employee?.firstName ?? ''} ${order.employee?.lastName ?? ''}`.toLowerCase().includes(searchText.toLowerCase())
  );

  return (
    <Modal open={visible} onCancel={onCancel} footer={null}
      title="Search Orders" width={800}>
      <Input
        placeholder="Search by ID, Customer, Employee..."
        value={searchText}
        onChange={e => setSearchText(e.target.value)}
        style={{ marginBottom: 10 }}
      />
      <Table
        dataSource={filtered}
        rowKey="orderId"
        onRow={(record) => ({
          onClick: () => onSelect(record)
        })}
        columns={[
          { title: 'Order ID', dataIndex: 'orderId', key: 'orderId' },
          {
            title: 'Customer',
            key: 'customerCompanyName',
            dataIndex: 'customerCompanyName',
          },
          {
            title: 'Employee',
            key: 'employeeFullName',
            dataIndex: 'employeeFullName'
          },
          { title: 'Order Date', dataIndex: 'orderDate', key: 'orderDate' },
        ]}
        pagination={false}
      />
    </Modal>
  );
};

export default OrderSearchModal;