import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';

const VendedoresList = () => {
  const [vendedores, setVendedores] = useState(null);

  useEffect(() => {
    setVendedores(null);
    axios.get('https://localhost:7084/Vendedor').then((res) => {
      setVendedores(res.data);
    });
  }, []);

  useEffect(() => {
    console.log(vendedores);
  }, [vendedores]);

  return (
    <div className="p-3 mt-5" style={{ width: '75%' }}>
      <h2>Vendedores</h2>
      <div className="mt-2">
        <a href={'/cadastrarVendedor'} className="btn btn-primary" id="btn-cad">
          Cadastrar vendedor
        </a>
      </div>
      <div className="d-flex justify-content-center mt-2">
        <table className="table table-striped" style={{ maxWidth: '100%' }}>
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Nome</th>
              <th scope="col">E-mail</th>
              <th scope="col">Salário Base</th>
              <th scope="col">Data de nascimento</th>
              <th scope="col">Data de admissão</th>
              <th scope="col">Ações</th>
            </tr>
          </thead>
          <tbody>
            {vendedores &&
              vendedores.map((vendedor, index) => (
                <tr key={index}>
                  <th scope="row">{vendedor.vendedorId}</th>
                  <td>{vendedor.nome}</td>
                  <td>{vendedor.email}</td>
                  <td>R$ {parseFloat(vendedor.salarioBase).toFixed(2)}</td>
                  <td>{vendedor.dataNascimento.substring(0, 10)}</td>
                  <td>{vendedor.dataAdmissao.substring(0, 10)}</td>
                  <td>
                    <div className="d-flex justify-content-center">
                      <div className="d-flex">
                        <a
                          className="btn  btn-secondary mx-2"
                          href={`/vendedor/${vendedor.vendedorId}`}
                        >
                          Visualizar
                        </a>
                        <a
                          className="btn btn-danger mx-2"
                          href={`/excluir/${vendedor.vendedorId}`}
                        >
                          Excluir
                        </a>
                      </div>
                    </div>
                  </td>
                </tr>
              ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default VendedoresList;
