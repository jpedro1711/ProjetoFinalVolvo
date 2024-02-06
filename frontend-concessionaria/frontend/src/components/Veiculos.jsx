import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';

const Veiculos = () => {
  const [veiculos, setVeiculos] = useState(null);

  useEffect(() => {
    setVeiculos(null);
    axios.get('https://localhost:7084/Veiculo').then((res) => {
      setVeiculos(res.data);
    });
  }, []);

  return (
    <div className="p-3 mt-5" style={{ width: '75%' }}>
      <h2>Veículos</h2>
      <div className="mt-2">
        <a href={'/cadastrarVeiculo'} className="btn btn-primary" id="btn-cad">
          Cadastrar veículo
        </a>
      </div>
      <div className="d-flex justify-content-center mt-2">
        <table className="table table-striped">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Número do chassi</th>
              <th scope="col">Valor</th>
              <th scope="col">Modelo</th>
              <th scope="col">Quilometragem</th>
              <th scope="col">Versão do sistema</th>
              <th scope="col">Ações</th>
            </tr>
          </thead>
          <tbody>
            {veiculos &&
              veiculos.map((veiculo, index) => (
                <tr key={index}>
                  <th scope="row">{veiculo.veiculoId}</th>
                  <td>{veiculo.numeroChassi}</td>
                  <td>R$ {parseFloat(veiculo.valor).toFixed(2)}</td>
                  <td>{veiculo.modelo}</td>
                  <td>{veiculo.quilometragem}</td>
                  <td>{veiculo.versaoSistema}</td>
                  <td>
                    <a
                      className="btn btn-danger"
                      href={`excluirVeiculo/${veiculo.veiculoId}`}
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

export default Veiculos;
