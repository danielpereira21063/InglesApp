import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';
import Row from 'react-bootstrap/Row';

function FormCadastroVocabulario() {
    const [validated, setValidated] = useState(false);

    const [tipoSelecionado, setTipoSelecionado] = useState("palavra");

    const handleSubmit = (event) => {
        const form = event.currentTarget;
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        }

        setValidated(true);
    };

    return (
        <Form noValidate validated={validated} onSubmit={handleSubmit}>
            <Row className="mb-3">
                <Form.Group as={Col} md="4" controlId="validationCustom01" className='mb-3'>
                    <Form.Label>Tipo</Form.Label>
                    <div className='d-flex justify-content-between'>
                        <InputGroup onClick={() => setTipoSelecionado('palavra')}>
                            <InputGroup.Radio name='checkboxPalavra' readOnly checked={tipoSelecionado == "palavra"} />
                            <Form.Control value={"Palavra"} readOnly style={{cursor: 'pointer'}}/>
                        </InputGroup>
                        <span className='ms-2'></span>
                        <InputGroup onClick={() => setTipoSelecionado('frase')}>
                            <InputGroup.Radio name='checkboxFrase' readOnly checked={tipoSelecionado == "frase"} />
                            <Form.Control value={"Frase"} readOnly style={{cursor: 'pointer'}}/>
                        </InputGroup>
                    </div>
                </Form.Group>
                <Form.Group as={Col} md="4" controlId="validationCustom01" className='mb-3'>
                    <Form.Label>Palavra</Form.Label>
                    <Form.Control
                        required
                        type="text"
                        placeholder="Palavra em inglês"
                    />
                </Form.Group>
                <Form.Group as={Col} md="4" controlId="validationCustom02" className='mb-3'>
                    <Form.Label>Tradução</Form.Label>
                    <Form.Control
                        required
                        type="text"
                        placeholder="Tradução em português"
                    />
                </Form.Group>

                <Form.Group as={Col} md="4" controlId="validationCustom02" className='mb-3'>
                    <Form.Label>Explicação</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder={`Como e quando usar a ${tipoSelecionado}?`}
                    />
                </Form.Group>
            </Row>

            <Button type="submit">Salvar</Button>
        </Form>
    );
}

export default FormCadastroVocabulario;