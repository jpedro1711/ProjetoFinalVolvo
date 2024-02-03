import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';

const Vendedor = () => {
  const [vendedor, setVendedor] = useState(null);
  const params = useParams();

  useEffect(() => {
    axios
      .get(`https://localhost:7084/Vendedor/${params.vendedorId}`)
      .then((res) => {
        setVendedor(res.data);
      });
  }, [params.vendedorId]);

  return (
    <div>
      {vendedor && (
        <div className="card">
          <div className="card-body">
            <h5 className="card-title">Detalhes do vendedor</h5>

            <p className="card-text text-start">
              <strong>Nome:</strong> {vendedor.nome}
              <br />
              <strong>E-mail:</strong> {vendedor.email}
              <br />
              <strong>Salário Base:</strong> {vendedor.salarioBase}
              <br />
              <strong>Data de Nascimento:</strong> {vendedor.dataNascimento}
              <br />
              <strong>Data de Admissão:</strong> {vendedor.dataAdmissao}
            </p>

            <h6 className="card-subtitle mb-2 text-muted text-start">
              Endereços:
            </h6>
            <ul>
              {vendedor.enderecos.map((endereco) => (
                <p key={endereco.enderecoId} className="text-start">
                  <strong>Rua:</strong> {endereco.rua}, <strong>Número:</strong>{' '}
                  {endereco.numero}, <strong>Bairro:</strong> {endereco.bairro},{' '}
                  <strong>Cidade:</strong> {endereco.cidade}
                </p>
              ))}
            </ul>

            <h6 className="card-subtitle mb-2 text-muted text-start">
              Telefones:
            </h6>
            <ul>
              {vendedor.telefones.map((telefone) => (
                <p key={telefone.telefoneId} className="text-start">
                  <strong>Tipo:</strong>{' '}
                  {telefone.tipo === 'c'
                    ? 'Celular'
                    : telefone.tipo === 'r'
                    ? 'Residencial'
                    : 'Outro'}
                  , <strong>Número:</strong> {telefone.numeroTelefone}
                </p>
              ))}
            </ul>

            <Link to="/" className="btn btn-primary">
              Voltar
            </Link>
          </div>
        </div>
      )}
    </div>
  );
};

export default Vendedor;
