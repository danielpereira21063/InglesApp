import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';
import Row from 'react-bootstrap/Row';
import api from '../../api/api';
import { Link } from 'react-router-dom';

function FormCadastroUsuario() {
  const [validated, setValidated] = useState(false);
  const [senha, setSenha] = useState('');
  const [confirmarSenha, setConfirmarSenha] = useState('');
  const [nome, setNome] = useState('');
  const [usuario, setUsuario] = useState('');
  const [email, setEmail] = useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();
    const form = event.currentTarget;

    if (senha !== confirmarSenha) {
      setValidated(true);
      return;
    }

    if (form.checkValidity() === false) {
      event.stopPropagation();
    } else {
      const userData = {
        nome: nome,
        usuario: usuario,
        senha: senha,
        email: email,
        token: ""
      };

      try {
        const response = await api.post('/Account/CreateAccount', userData);
        window.localStorage.setItem("usuario", JSON.stringify(response.data));

        window.location.href = "/";
      } catch (error) {
        console.error('Erro ao enviar dados:', error);
      }
    }

    setValidated(true);
  };

  const handleSenhaChange = (event) => {
    setSenha(event.target.value);
  };

  const handleConfirmarSenhaChange = (event) => {
    setConfirmarSenha(event.target.value);
  };

  const handleNomeChange = (event) => {
    setNome(event.target.value);
  };

  const handleUsuarioChange = (event) => {
    setUsuario(event.target.value);
  };

  const handleEmailChange = (event) => {
    setEmail(event.target.value);
  };

  return (
    <Form noValidate validated={validated} onSubmit={handleSubmit}>
      <Row className="mb-3">
        <Form.Group as={Col} md="4" className='mb-2' controlId="validationCustom01">
          <Form.Label>Nome</Form.Label>
          <Form.Control
            required
            type="text"
            placeholder="Nome"
            minLength={3}
            value={nome}
            onChange={handleNomeChange}
          />
          <Form.Control.Feedback type="invalid">
            Por favor, insira um nome válido.
          </Form.Control.Feedback>
        </Form.Group>
        <Form.Group as={Col} md="4" className='mb-2' controlId="validationCustomUsername">
          <Form.Label>Usuário</Form.Label>
          <InputGroup hasValidation>
            <Form.Control
              type="text"
              minLength={3}
              placeholder="Nome de usuário"
              value={usuario}
              onChange={handleUsuarioChange}
              required
            />
            <Form.Control.Feedback type="invalid">
              Por favor, escolha um nome de usuário válido.
            </Form.Control.Feedback>
          </InputGroup>
        </Form.Group>
        <Form.Group as={Col} md="4" controlId="validationCustomEmail">
          <Form.Label>Email</Form.Label>
          <InputGroup>

            <InputGroup.Text id="inputGroupPrepend">@</InputGroup.Text>
            <Form.Control
              type="email"
              placeholder="Seu endereço de e-mail"
              value={email}
              onChange={handleEmailChange}
              required
            />
            <Form.Control.Feedback type="invalid">
              Por favor, insira um endereço de e-mail válido.
            </Form.Control.Feedback>
          </InputGroup>
        </Form.Group>
      </Row>
      <Row className="mb-3">
        <Form.Group as={Col} md="6" className='mb-2' controlId="validationCustomSenha">
          <Form.Label>Senha</Form.Label>
          <Form.Control
            type="password"
            placeholder="Senha"
            required
            value={senha}
            minLength={6}
            onChange={handleSenhaChange}
          />
        </Form.Group>
        <Form.Group as={Col} md="6" controlId="validationCustomConfirmarSenha">
          <Form.Label>Confirmar Senha</Form.Label>
          <Form.Control
            type="password"
            placeholder="Confirmar Senha"
            required
            value={confirmarSenha}
            onChange={handleConfirmarSenhaChange}
          />
          <Form.Control.Feedback type="invalid">
            As senhas não coincidem.
          </Form.Control.Feedback>
        </Form.Group>
      </Row>
      <Button type="submit">Cadastrar</Button>

      <Link to={"/login"} className='d-flex mt-3'>Já tem uma conta? Entrar</Link >
    </Form>
  );
}

export default FormCadastroUsuario;
