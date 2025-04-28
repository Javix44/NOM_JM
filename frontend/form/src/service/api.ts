// import { useEffect, useState } from 'react';
// import axios, { AxiosResponse } from 'axios';

// // Define el tipo de un "Order" si sabes qué campos trae
// interface Order {
//   id: number;
//   customerName: string;
//   orderDate: string;
//   // Agrega aquí más campos si tu modelo de datos los tiene
// }

// function App() {
//   const [orders, setOrders] = useState<Order[]>([]);
//   const [loading, setLoading] = useState<boolean>(true);

//   useEffect(() => {
//     console.log('Making GET request to the backend...');
//     axios.get<Order[]>('https://localhost:7021/api/orders')
//       .then((response: AxiosResponse<Order[]>) => {
//         console.log('Orders received:', response.data);
//         setOrders(response.data);
//       })
//       .catch((error: unknown) => {
//         console.error('Error fetching orders:', error);
//       })
//       .finally(() => {
//         setLoading(false);
//       });
//   }, []);

//   if (loading) {
//     return <p>Loading orders...</p>;
//   }

//   return (
//     <div>
//       <h1>Orders List</h1>
//       <pre>{JSON.stringify(orders, null, 2)}</pre>
//     </div>
//   );
// }

// export default App;
