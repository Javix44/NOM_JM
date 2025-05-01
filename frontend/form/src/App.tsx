import OrderManagementUI from './pages/OrderManagementUI';

function App() {
  return (
    <div style={{
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      width: '98vw',
    }}>
      <div style={{
        margin: '20px auto',
        border: '3px solid #ccc',
        borderRadius: '8px',
        backgroundColor: '#f9f9f9',
        width: '60%',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        minHeight: '60px',
      }}>
        <OrderManagementUI />
      </div>
    </div>
  );
}

export default App;
