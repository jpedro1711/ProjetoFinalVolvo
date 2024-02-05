import React from 'react';

const Header = () => {
  const headerStyle = {
    position: 'fixed',
    top: 0,
    left: 0,
    width: '100%',
    zIndex: 1000,
  };

  return (
    <div>
      <nav
        className="navbar navbar-expand-lg bg-body-tertiary"
        style={headerStyle}
      >
        <div className="container-fluid">
          <a className="navbar-brand" href="/">
            Concessionária
          </a>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarNav"
            aria-controls="navbarNav"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav">
              <li className="nav-item">
                <a className="nav-link" href="/vendedores">
                  Vendedores
                </a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="/veiculos">
                  Veículos
                </a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="/vendas">
                  Vendas
                </a>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </div>
  );
};

export default Header;
