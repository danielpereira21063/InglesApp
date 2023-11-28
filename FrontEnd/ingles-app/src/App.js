import './App.css';
import Home from './pages/Home/Home';
import DefaultNavbar from './components/DefaultNavbar';
import { Button, Container } from 'react-bootstrap';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import FormCadastroVocabulario from './pages/FormCadastro/FormCadastroVocabulario';
import FormCadastroUsuario from './pages/FormCadastro/FormCadastroUsuario';
import FormLogin from './pages/FormCadastro/FormLogin';

function App() {

  return (
    <BrowserRouter>
      <div color='white' bg="dark" data-bs-theme="dark" className='App'>
        <DefaultNavbar />

        <Container>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/vocabulario/novo" element={<FormCadastroVocabulario />} />
            <Route path="/usuario/novo" element={<FormCadastroUsuario />} />
            <Route path="/login" element={<FormLogin />} />
          </Routes>
        </Container>


        <Button onClick={() => window.location.href="/vocabulario/novo"} className='flutuante btn-lg rounded-circle'><i className="fa-solid fa-plus"></i></Button>
      </div>
    </BrowserRouter>
  );
}

export default App;
