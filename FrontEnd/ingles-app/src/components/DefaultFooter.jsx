import React from 'react';

function DefaultFooter() {
  const currentYear = new Date().getFullYear();

  return (
    <footer className="footer pb-5 pt-5 pb-2">
      <div className="container text-center">
        <p>
          &copy; {currentYear} | Daniel Pereira Sanches
          <br/>
          <i className="fa-solid fa-envelope"></i> <a href="mailto:danielsanches6301@gmail.com">danielsanches6301@gmail.com</a>
        </p>
      </div>
    </footer>
  );
}

export default DefaultFooter;
