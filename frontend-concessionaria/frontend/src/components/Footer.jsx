import React from 'react';

const Footer = () => {
  const footerStyle = {
    width: '100%',
    marginTop: 'auto',
  };

  return (
    <div style={footerStyle}>
      <footer className="bg-body-dark text-secondary bg-dark text-center text-lg-start">
        <div className="text-center p-3">Projeto Final</div>
      </footer>
    </div>
  );
};

export default Footer;
