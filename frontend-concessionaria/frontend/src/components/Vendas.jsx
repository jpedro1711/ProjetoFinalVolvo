import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';

const Vendas = () => {
  const [vendas, setVendas] = useState(null);

  useEffect(() => {
    axios.get('https://localhost:7084/Venda').then((res) => {
      setVendas(res.data);
      console.log(res.data);
    });
  }, []);

  return (
    <div className="fixed-top fixed-left p-3 mt-5">
      <h2>Vendas</h2>
      <div className="mt-2">
        <a href={'/cadastrarVenda'} className="btn btn-primary" id="btn-cad">
          Cadastrar venda
        </a>
      </div>
      <div className="d-flex justify-content-center mt-2">
        <table className="table table-striped" style={{ maxWidth: '75%' }}>
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Data da venda</th>
              <th scope="col">Veículo</th>
              <th scope="col">Valor</th>
              <th scope="col">Vendedor</th>
              <th scope="col">Ações</th>
            </tr>
          </thead>
          <tbody>
            {vendas &&
              vendas.map((venda, index) => (
                <tr key={index}>
                  <th scope="row">{venda.vendaId}</th>
                  <td>{venda.dataVenda}</td>
                  <td>{venda.veiculo.modelo}</td>
                  <td>{venda.veiculo.valor}</td>
                  <td>{venda.vendedor.nome}</td>
                  <td>
                    <a
                      className="btn btn-danger"
                      href={`excluirVenda/${venda.vendaId}`}
                    >
                      Excluir
                    </a>
                  </td>
                </tr>
              ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Vendas;
