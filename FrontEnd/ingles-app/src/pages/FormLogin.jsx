import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import api from './../api/api';
import { Link } from 'react-router-dom';

function FormLogin() {
  const [usuario, setUsuario] = useState('');
  const [senha, setSenha] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false); // New state variable

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (isSubmitting) {
      return; // Avoid multiple submissions
    }

    setIsSubmitting(true); // Set isSubmitting to true when the form is submitted

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
    } finally {
      setIsSubmitting(false); // Reset isSubmitting to false after the form submission
    }
  };

  return (
    <div>
      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Usuário</Form.Label>
          <Form.Control
            type="text"
            placeholder="Nome de usuário ou email"
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
        <Button variant="primary" type="submit" disabled={isSubmitting}>
          Entrar
        </Button>

        <Link to={"/usuario/novo"} className='d-flex mt-3'>Criar conta</Link >
      </Form>
    </div>
  );
}

export default FormLogin;
