import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Navigate, useNavigate } from 'react-router-dom';
const CadastrarVeiculo = () => {
  const [numeroChassi, setNumeroChassi] = useState();
  const [valor, setValor] = useState();
  const [modelo, setModelo] = useState();
  const [quilometragem, setQuilometragem] = useState();
  const [versaoSistema, setVersaoSistema] = useState();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (
      numeroChassi == '' ||
      valor == '' ||
      modelo == '' ||
      quilometragem == '' ||
      versaoSistema == ''
    ) {
      alert('Dados inválidos');
    } else {
      const dados = {
        numeroChassi: numeroChassi,
        valor: parseFloat(valor),
        modelo: modelo,
        quilometragem: parseInt(quilometragem),
        versaoSistema: versaoSistema,
        acessorios: [],
      };
      const res = await axios.post('https://localhost:7084/Veiculo', dados);
      console.log(dados);
      console.log(res.status);
      console.log(res.headers);
      console.log(res.statusText);
      console.log(res.data);
      navigate('/veiculos');
    }
  };

  return (
    <div>
      <h2>Cadastrar veículo</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="nome" className="form-label">
            Número de chassi
          </label>
          <input
            type="text"
            className="form-control"
            id="numeroChassi"
            required
            onChange={(e) => setNumeroChassi(e.target.value)}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="nome" className="form-label">
            Valor
          </label>
          <input
            type="text"
            className="form-control"
            id="valor"
            required
            onChange={(e) => setValor(e.target.value)}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="nome" className="form-label">
            Modelo
          </label>
          <input
            type="text"
            className="form-control"
            id="modelo"
            required
            onChange={(e) => setModelo(e.target.value)}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="nome" className="form-label">
            Quilometragem
          </label>
          <input
            type="number"
            className="form-control"
            id="quilometragem"
            required
            onChange={(e) => setQuilometragem(e.target.value)}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="nome" className="form-label">
            Versão do sistema
          </label>
          <input
            type="string"
            className="form-control"
            id="versaoSistema"
            required
            onChange={(e) => setVersaoSistema(e.target.value)}
          />
        </div>

        <button type="submit" className="btn btn-primary">
          Cadastrar
        </button>
      </form>
    </div>
  );
};

export default CadastrarVeiculo;
