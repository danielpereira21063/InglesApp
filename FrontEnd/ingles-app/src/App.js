import './App.css';
import Home from './pages/Home/Home';
import DefaultNavbar from './components/DefaultNavbar';
import { Button, Container } from 'react-bootstrap';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import FormCadastro from './pages/FormCadastro/FormCadastro';

function App() {

  return (
    <BrowserRouter>
      <div color='white' bg="dark" data-bs-theme="dark" className='App'>
        <DefaultNavbar />

        <Container>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/novo" element={<FormCadastro />} />
          </Routes>
        </Container>


        <Button onClick={() => window.location.href="/novo"} className='flutuante btn-lg rounded-circle'><i className="fa-solid fa-plus"></i></Button>
      </div>
    </BrowserRouter>
  );
}

export default App;
