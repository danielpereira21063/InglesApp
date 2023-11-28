import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import api from '../../api/api';

function FormLogin() {
  const [usuario, setUsuario] = useState('');
  const [senha, setSenha] = useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();

    const dadosLogin = {
      login: usuario,
      senha: senha,
    };

    try {
      const resposta = await api.post('/Account/Login', dadosLogin);
      
      localStorage.setItem('usuario', JSON.stringify(resposta.data));
    
      window.location.href = "/";
    } catch (erro) {
      if (erro.response) {
        toast.error('Usuário ou senha incorretos');
      }
    }
  };

  return (
    <div>
      <Form onSubmit={handleSubmit} className='mt-5'>
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Usuário</Form.Label>
          <Form.Control
            type="text"
            placeholder="Nome de usuário"
            value={usuario}
            onChange={(e) => setUsuario(e.target.value)}
          />
        </Form.Group>

        <Form.Group className="mb-3" controlId="formBasicPassword">
          <Form.Label>Senha</Form.Label>
          <Form.Control
            type="password"
            placeholder="Digite sua senha"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
          />
        </Form.Group>
        <Button variant="primary" type="submit">
          Entrar
        </Button>
      </Form>
      <ToastContainer />
    </div>
  );
}

export default FormLogin;
