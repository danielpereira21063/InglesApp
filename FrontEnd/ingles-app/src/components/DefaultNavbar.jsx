import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import Offcanvas from 'react-bootstrap/Offcanvas';
import { useUser } from '../Contexts/UserContext';
import { NavDropdown } from 'react-bootstrap';


const DefaultNavbar = () => {
  const { user, logout } = useUser();

  return (
    <>
      <Navbar expand={'md'} bg="dark" data-bs-theme="dark" className="bg-body-tertiary mb-3">
        <Container fluid >
          <Navbar.Brand href="/">Inglês App</Navbar.Brand>
          <Navbar.Toggle aria-controls={`offcanvasNavbar-expand-md`} />
          <Navbar.Offcanvas
            id={`offcanvasNavbar-expand-md`}
            aria-labelledby={`offcanvasNavbarLabel-expand-md`}
            placement="end"
            bg="dark" data-bs-theme="dark"
          >
            <Offcanvas.Header closeButton>
              <Offcanvas.Title id={`offcanvasNavbarLabel-expand-md`}>
                Inglês App
              </Offcanvas.Title>
            </Offcanvas.Header>
            <Offcanvas.Body>
              <Nav className="justify-content-end flex-grow-1 pe-3">
                <Nav.Link href="/"><i className="fa-solid fa-house"></i> Início</Nav.Link>

                {user && (
                  <Nav.Link href="/praticar"><i className="fa-solid fa-book-open-reader"></i> Praticar</Nav.Link>
                )}

                <Nav.Link href="/usuario/novo"><i className="fa-solid fa-plus"></i> Nova conta</Nav.Link>

                {
                  user ?
                    <NavDropdown
                      title={user.nome}
                      id={`offcanvasNavbarDropdown-expand-md`}
                    >
                      <NavDropdown.Item onClick={logout} href="/login" className='text-danger'>
                        <i className="fa-solid fa-right-from-bracket"></i> Sair
                      </NavDropdown.Item>
                    </NavDropdown>
                    : (<Nav.Link href="/login"><i className="fa-solid fa-right-to-bracket"></i> Login</Nav.Link>)
                }


              </Nav>

              {user && (

                <Form className="d-flex">
                  <Form.Control
                    type="search"
                    placeholder="Busque palavras ou frases..."
                    className="me-2 form-control-sm"
                    aria-label="Search"
                  />
                  <Button variant="outline-light"><i className="fa-solid fa-magnifying-glass"></i></Button>
                </Form>

              )}
            </Offcanvas.Body>
          </Navbar.Offcanvas>
        </Container>
      </Navbar>

    </>
  )
}

export default DefaultNavbar;