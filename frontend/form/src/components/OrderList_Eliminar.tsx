// // components/OrderList.tsx
// import { Table, Input, Space } from 'antd';
// import { useEffect, useState } from 'react';
// import { fetchOrders } from '../service/api';
// import { Order } from '../service/types';

// type Props = {
//   onSelect: (order: Order) => void;
// };

// const OrderList = ({ onSelect }: Props) => {
//   const [orders, setOrders] = useState<Order[]>([]);
//   const [filteredOrders, setFilteredOrders] = useState<Order[]>([]);
//   const [search, setSearch] = useState('');

//   useEffect(() => {
//     fetchOrders().then(data => {
//       setOrders(data);
//       setFilteredOrders(data);
//       if (data.length > 0) {
//         onSelect(data[0]); // Selecciona el primer registro al cargar
//       }
//     });
//   }, [onSelect]);

//   const handleSearch = (value: string) => {
//     setSearch(value);
//     const lower = value.toLowerCase();
//     setFilteredOrders(
//       orders.filter(order =>
//         order.customer?.companyName?.toLowerCase().includes(lower) ||
//         String(order.orderId).includes(lower)
//       )
//     );
//   };

//   return (
//     <Space direction="vertical" style={{ width: '100%' }}>
//       <Input.Search
//         placeholder="Search by customer or order ID"
//         onChange={e => handleSearch(e.target.value)}
//         value={search}
//         allowClear
//       />
//       <Table
//         dataSource={filteredOrders}
//         rowKey="orderId"
//         onRow={record => ({
//           onClick: () => onSelect(record),
//         })}
//         columns={[
//           { title: 'Order ID', dataIndex: 'orderId' },
//           { title: 'Customer', dataIndex: ['customer', 'companyName'] },
//           { title: 'Date', dataIndex: 'orderDate' },
//         ]}
//         pagination={false}
//       />
//     </Space>
//   );
// };

// export default OrderList;
