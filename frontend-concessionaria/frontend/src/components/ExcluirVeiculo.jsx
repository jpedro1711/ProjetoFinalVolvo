import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Navigate, useNavigate } from 'react-router-dom';

const ExcluirVeiculo = () => {
  const [veiculo, setVeiculo] = useState(null);
  const navigate = useNavigate();
  const params = useParams();

  useEffect(() => {
    axios
      .get(`https://localhost:7084/Veiculo/${params.veiculoId}`)
      .then((res) => {
        setVeiculo(res.data);
      });
  }, [params.vendedorId]);

  const handleClick = async () => {
    const res = await axios.delete(
      `https://localhost:7084/Veiculo/${params.veiculoId}`
    );
    navigate('/veiculos');
  };

  return (
    <div>
      {veiculo && (
        <div className="card">
          <div className="card-body">
            <h5 className="card-title">Detalhes do veículo</h5>

            <p className="card-text text-start">
              <strong>Número de chassi</strong> {veiculo.numeroChassi}
              <br />
              <strong>Valor</strong> {veiculo.valor}
              <br />
              <strong>Modelo</strong> {veiculo.modelo}
              <br />
              <strong>Quilometragem</strong> {veiculo.quilometragem}
              <br />
              <strong>Versão sistema:</strong> {veiculo.versaoSistema}
              <br />
            </p>

            <Link to="/veiculos" className="btn btn-primary">
              Voltar
            </Link>
            <button className="btn btn-danger mx-2" onClick={handleClick}>
              Excluir
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default ExcluirVeiculo;
