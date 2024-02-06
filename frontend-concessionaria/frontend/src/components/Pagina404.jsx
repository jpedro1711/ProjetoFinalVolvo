import React from 'react';

const Pagina404 = () => {
  const appStyle = {
    minHeight: '100vh',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  };

  return (
    <div className="p-5 mt-5" style={appStyle}>
      <strong>404 - Erro</strong>
      <br />
      <a href="/" className="btn btn-secondary">
        Voltar ao começo
      </a>
    </div>
  );
};

export default Pagina404;
