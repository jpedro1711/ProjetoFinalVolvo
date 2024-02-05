import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Navigate, useNavigate } from 'react-router-dom';

const ExcluirVenda = () => {
  const [venda, setVenda] = useState(null);
  const navigate = useNavigate();
  const params = useParams();

  useEffect(() => {
    axios.get(`https://localhost:7084/Venda/${params.vendaId}`).then((res) => {
      setVenda(res.data);
    });
  }, [params.vendedorId]);

  const handleClick = async () => {
    const res = await axios.delete(
      `https://localhost:7084/Venda/${params.vendaId}`
    );
    navigate('/vendas');
  };

  return (
    <div>
      {venda && (
        <div className="card">
          <div className="card-body">
            <h5 className="card-title">Detalhes da venda</h5>

            <p className="card-text text-start">
              <strong>Data de venda:</strong> {venda.dataVenda}
              <br />
              <strong>Veículo:</strong> {venda.veiculo.modelo}
              <br />
              <strong>Valor:</strong> {venda.veiculo.valor}
              <br />
              <strong>Nome do vendedor:</strong> {venda.vendedor.nome}
              <br />
            </p>

            <Link to="/vendas" className="btn btn-primary">
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

export default ExcluirVenda;
