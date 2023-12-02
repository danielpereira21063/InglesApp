import './App.css';
import Home from './pages/Home/Home';
import { Button, Container } from 'react-bootstrap';
import DefaultNavbar from './components/DefaultNavbar';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import FormCadastroVocabulario from './pages/FormCadastro/FormCadastroVocabulario';
import FormCadastroUsuario from './pages/FormCadastro/FormCadastroUsuario';
import FormLogin from './pages/FormLogin';
import PagePraticar from './pages/Praticar/PagePraticar';
import DefaultFooter from './components/DefaultFooter';
import { UserProvider, useUser } from './Contexts/UserContext';
import { ToastContainer } from 'react-toastify';

const isHome = window.location.pathname == "/";

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
          <Route path="/" element={user != null ? <Home /> : <Navigate to={"/login"} />} />
          <Route path="/login" element={<FormLogin />} />
          <Route path="/usuario/novo" element={<FormCadastroUsuario />} />

          <Route path="/vocabulario/novo" element={<FormCadastroVocabulario />} />
          <Route path="/vocabulario/:id" element={<FormCadastroVocabulario />} />

          <Route path="/praticar" element={<PagePraticar />} />

        </Routes>

      </Container>
      <ToastContainer />

      <DefaultFooter />


      {/* Botões fixos na parte inferior */}
      <div className='fixed-buttons'>
        {!isHome && 
          <Button variant='light' onClick={() => window.history.back()} className='btn-lg rounded-circle me-1'><i className="fa-solid fa-arrow-left"></i></Button>
        }
        <Button onClick={() => window.location.href = "/vocabulario/novo"} className='btn-lg ml-2 rounded-circle'><i className="fa-solid fa-plus"></i></Button>
      </div>

    </div>
  );
}

export default App;
