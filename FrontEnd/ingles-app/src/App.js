import './App.css';
import Home from './pages/Home/Home';
import DefaultNavbar from './components/DefaultNavbar';
import { Button, Container } from 'react-bootstrap';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import FormCadastroVocabulario from './pages/FormCadastro/FormCadastroVocabulario';
import FormCadastroUsuario from './pages/FormCadastro/FormCadastroUsuario';
import FormLogin from './pages/FormCadastro/FormLogin';
import DefaultFooter from './components/DefaultFooter';
import { UserProvider, useUser } from './Contexts/UserContext';

function App() {
  return (
    <BrowserRouter>
      <UserProvider>
        <InnerApp />
      </UserProvider>
    </BrowserRouter>
  );
}

function InnerApp() {
  const { user } = useUser();

  return (
    <div color='white' bg="dark" data-bs-theme="dark" className='App'>
      <DefaultNavbar />

      <Container>
        <Routes>

          <Route path="/" element={user != null ? <Home /> : <Navigate to={"/login"}/>} />
          <Route path="/vocabulario/novo" element={<FormCadastroVocabulario />} />

          <Route path="/usuario/novo" element={<FormCadastroUsuario />} />
          <Route path="/login" element={<FormLogin />} />
        </Routes>
      </Container>

      <DefaultFooter />

      <Button onClick={() => window.location.href = "/vocabulario/novo"} className='flutuante btn-lg rounded-circle'><i className="fa-solid fa-plus"></i></Button>
    </div>
  );
}

export default App;
