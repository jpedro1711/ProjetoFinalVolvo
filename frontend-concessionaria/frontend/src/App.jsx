import { useState } from 'react';
import './App.css';
import VendedoresList from './components/VendedoresList';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Vendedor from './components/Vendedor';
import CadastrarVendedor from './components/CadastrarVendedor';
import ExcluirVendedor from './components/ExcluirVendedor';

function App() {
  const [count, setCount] = useState(0);

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<VendedoresList />} />
          <Route path="/cadastrarVendedor" element={<CadastrarVendedor />} />
          <Route path="/vendedor/:vendedorId/*" element={<Vendedor />} />
          <Route path="/excluir/:vendedorId/*" element={<ExcluirVendedor />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
