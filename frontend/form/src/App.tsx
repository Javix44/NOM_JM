import OrderManagementUI from './pages/OrderManagementUI';

function App() {
  return (
    <div style={{
      padding: '20px',
      border: '1px solid #ccc',
      borderRadius: '8px',
      margin: '40px auto',  
      backgroundColor: '#f9f9f9',
      width: '60%',
      display: 'flex', 
      justifyContent: 'center'
    }}>
      <OrderManagementUI />
    </div>
  );
}

export default App;
