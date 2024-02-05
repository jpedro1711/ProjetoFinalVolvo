import { useState } from 'react';
import './App.css';
import VendedoresList from './components/VendedoresList';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Vendedor from './components/Vendedor';
import CadastrarVendedor from './components/CadastrarVendedor';
import ExcluirVendedor from './components/ExcluirVendedor';
import Header from './components/Header';
import Vendas from './components/Vendas';
import Veiculos from './components/Veiculos';
import CadastrarVenda from './components/CadastrarVenda';
import ExcluirVenda from './components/ExcluirVenda';
import CadastrarVeiculo from './components/CadastrarVeiculo';
import ExcluirVeiculo from './components/ExcluirVeiculo';
import Pagina404 from './components/Pagina404';
import Home from './components/Home';
function App() {
  const [count, setCount] = useState(0);

  return (
    <>
      <BrowserRouter>
        <Header />
        <Routes>
          <Route path="/vendedores" element={<VendedoresList />} />
          <Route path="/cadastrarVendedor" element={<CadastrarVendedor />} />
          <Route path="/cadastrarVenda" element={<CadastrarVenda />} />
          <Route path="/vendedor/:vendedorId/*" element={<Vendedor />} />
          <Route path="/excluir/:vendedorId/*" element={<ExcluirVendedor />} />
          <Route
            path="excluirVeiculo/:veiculoId/*"
            element={<ExcluirVeiculo />}
          />
          <Route path="/veiculos" element={<Veiculos />}></Route>
          <Route path="/vendas" element={<Vendas />}></Route>
          <Route
            path="/cadastrarVeiculo"
            element={<CadastrarVeiculo />}
          ></Route>
          <Route
            path="/excluirVenda/:vendaId"
            element={<ExcluirVenda />}
          ></Route>
          <Route path="/" element={<Home />}></Route>
          <Route path="*" element={<Pagina404 />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
