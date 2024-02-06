import React, { useState } from 'react';
import { Navigate, useNavigate } from 'react-router-dom';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';

const CadastrarVendedor = () => {
  const [nome, setNome] = useState('');
  const [email, setEmail] = useState('');
  const [salarioBase, setsalarioBase] = useState('');
  const [dataNascimento, setdataNascimento] = useState('');
  const [dataAdmissao, setdataAdmissao] = useState('');
  const [rua, setRua] = useState('');
  const [numero, setNumero] = useState('');
  const [bairro, setBairro] = useState('');
  const [cidade, setCidade] = useState('');
  const [tipoTelefone, setTipoTelefone] = useState('c');
  const [numeroTelefone, setNumeroTelefone] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const dados = {
        nome: nome,
        email: email,
        salarioBase: parseFloat(salarioBase),
        dataNascimento: dataNascimento,
        dataAdmissao: dataAdmissao,
        enderecos: [
          {
            rua: rua,
            numero: parseInt(numero),
            bairro: bairro,
            cidade: cidade,
          },
        ],
        telefones: [
          {
            tipo: tipoTelefone,
            numeroTelefone: numeroTelefone,
          },
        ],
      };
      const res = await axios.post('https://localhost:7084/Vendedor', dados);
      console.log(dados);
      console.log(res.status);
      console.log(res.headers);
      console.log(res.statusText);
      console.log(res.data);
      navigate('/vendedores');
    } catch (error) {
      console.log(error);
      console.log(error.message);
      console.log(error.respose);
    }
  };

  return (
    <div className="container my-4">
      <h2 className="mb-4">Cadastrar Vendedor</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="nome" className="form-label">
            Nome do vendedor
          </label>
          <input
            type="text"
            className="form-control"
            id="nome"
            required
            onChange={(e) => setNome(e.target.value)}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="email" className="form-label">
            E-mail do vendedor
          </label>
          <input
            type="email"
            className="form-control"
            id="email"
            required
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="salarioBase" className="form-label">
            Salário base do vendedor
          </label>
          <input
            type="number"
            className="form-control"
            id="salarioBase"
            required
            onChange={(e) => setsalarioBase(e.target.value)}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="dtNascimento" className="form-label">
            Data de nascimento do vendedor
          </label>
          <input
            type="datetime-local"
            className="form-control"
            id="dtNascimento"
            required
            onChange={(e) => setdataNascimento(e.target.value)}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="dtAdmissao" className="form-label">
            Data de admissão do vendedor
          </label>
          <input
            type="datetime-local"
            className="form-control"
            id="dtAdmissao"
            required
            onChange={(e) => setdataAdmissao(e.target.value)}
          />
        </div>
        <div>
          <p>Endereço </p>
          <div className="mb-3">
            <label htmlFor="rua" className="form-label">
              Rua
            </label>
            <input
              type="text"
              className="form-control"
              id="rua"
              required
              onChange={(e) => setRua(e.target.value)}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="bairro" className="form-label">
              Bairro
            </label>
            <input
              type="text"
              className="form-control"
              id="bairro"
              required
              onChange={(e) => setBairro(e.target.value)}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="numero" className="form-label">
              Número
            </label>
            <input
              type="number"
              className="form-control"
              id="numero"
              required
              onChange={(e) => setNumero(e.target.value)}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="cidade" className="form-label">
              Cidade
            </label>
            <input
              type="text"
              className="form-control"
              id="cidade"
              required
              onChange={(e) => setCidade(e.target.value)}
            />
          </div>
        </div>
        <div>
          <p>Telefone </p>
          <div className="mb-3">
            <label htmlFor="tipoTelefone" className="form-label">
              Tipo
            </label>
            <select
              className="form-select"
              required
              value={tipoTelefone}
              onChange={(e) => setTipoTelefone(e.target.value)}
            >
              <option value="c">Celular</option>
              <option value="r">Residencial</option>
            </select>
          </div>
          <div className="mb-3">
            <label htmlFor="numeroTelefone" className="form-label">
              Número
            </label>
            <input
              type="text"
              className="form-control"
              id="numeroTelefone"
              required
              onChange={(e) => setNumeroTelefone(e.target.value)}
            />
          </div>
        </div>
        <br />
        <button type="submit" className="btn btn-primary">
          Cadastrar
        </button>
      </form>
    </div>
  );
};

export default CadastrarVendedor;
