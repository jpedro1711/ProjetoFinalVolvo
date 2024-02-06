import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Navigate, useNavigate } from 'react-router-dom';

const CadastrarVenda = () => {
  const [vendedores, setVendedores] = useState([]);
  const [veiculos, setVeiculos] = useState([]);
  const [selectedVendedor, setSelectedVendedor] = useState('');
  const [selectedVeiculo, setSelectedVeiculo] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    axios.get('https://localhost:7084/Vendedor').then((res) => {
      setVendedores(res.data);
    });
    axios.get('https://localhost:7084/Veiculo').then((res) => {
      setVeiculos(res.data);
    });
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (selectedVeiculo == '' || selectedVendedor == '') {
      alert('Selecione o veículo e vendedor!');
    } else {
      const dados = {
        veiculoId: selectedVeiculo,
        vendedorId: selectedVendedor,
      };
      const res = await axios.post('https://localhost:7084/Venda', dados);
      console.log(dados);
      console.log(res.status);
      console.log(res.headers);
      console.log(res.statusText);
      console.log(res.data);
      navigate('/vendas');
    }
  };

  return (
    <div className="p-3 mt-5">
      <form onSubmit={handleSubmit}>
        <h2>Cadastrar venda</h2>
        <div className="mb-3">
          <label htmlFor="vendedorSelect" className="form-label">
            Selecione o Vendedor:
          </label>
          <select
            className="form-select"
            id="vendedorSelect"
            value={selectedVendedor}
            onChange={(e) => setSelectedVendedor(e.target.value)}
          >
            <option value="">Selecione um vendedor</option>
            {vendedores.map((vendedor) => (
              <option key={vendedor.vendedorId} value={vendedor.vendedorId}>
                {vendedor.nome}
              </option>
            ))}
          </select>
        </div>
        <div className="mb-3">
          <label htmlFor="veiculoSelect" className="form-label">
            Selecione o Veículo:
          </label>
          <select
            className="form-select"
            id="veiculoSelect"
            value={selectedVeiculo}
            onChange={(e) => setSelectedVeiculo(e.target.value)}
          >
            <option value="">Selecione um veículo</option>
            {veiculos.map((veiculo) => (
              <option key={veiculo.veiculoId} value={veiculo.veiculoId}>
                {veiculo.modelo} {' - '}
                {veiculo.numeroChassi}
              </option>
            ))}
          </select>
        </div>
        <button type="submit" className="btn btn-primary">
          Cadastrar Venda
        </button>
      </form>
    </div>
  );
};

export default CadastrarVenda;
