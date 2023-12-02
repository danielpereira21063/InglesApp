import { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';
import Row from 'react-bootstrap/Row';
import { toast } from 'react-toastify';
import api from '../../api/api';
import { useParams } from 'react-router-dom';

function FormCadastroVocabulario() {
    const [validated, setValidated] = useState(false);
    const [tipoSelecionado, setTipoSelecionado] = useState("Palavra");
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [vocabulario, setVocabulario] = useState(null);

    const { id } = useParams();

    useEffect(() => {
        const fetchVocabulario = async () => {
            try {
                const response = await api.get(`/vocabulario/${id}`);
                setVocabulario(response.data);
                setTipoSelecionado(response.data.tipoVocabulario);
            } catch (error) {
                toast.error(error?.response?.data ?? "Erro")
            }
        };

        if (id > 0) {
            fetchVocabulario();
        }
    }, [id]);

    const deletarVocabulario = async (confirmado = false) => {
        if (confirmado) {
            try {
                const response = await api.delete(`/vocabulario/${id}`);
                toast.success("Deletado com sucesso");
                setTimeout(() => {
                    window.location.href = "/";
                }, 1000);
            } catch (error) {
                toast.error(error?.response?.data ?? "Erro")
            }
            return;
        }

        const deletar = window.confirm("Tem certeza que deseja deletar?");
        if (deletar) deletarVocabulario(true);
    }

    const handleSubmit = async (event) => {
        event.preventDefault();
        const form = event.currentTarget;

        if (!form.checkValidity()) {
            event.stopPropagation();
            setValidated(true);
            return;
        }

        setIsSubmitting(true);

        const data = {
            id: id,
            userId: vocabulario?.userId ?? 0,
            tipoVocabulario: tipoSelecionado,
            emIngles: form.elements.palavra.value,
            traducao: form.elements.traducao.value,
            explicacao: form.elements.explicacao.value,
        };

        try {
            const response = await api.post('/vocabulario', data);

            toast.success(response.data);

            const isNovo = window.location.pathname.toLowerCase().includes("novo")
            if (isNovo) {
                setTimeout(() => {

                    window.location.href = "/";
                }, 1000);
            }

            setTipoSelecionado("Palavra");
        } catch (error) {
            toast.error(error?.response?.data ?? "Erro");
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <Form noValidate validated={validated} onSubmit={handleSubmit} className='px-2'>
            <h5 className='mb-3 text-center'>Novo vocabulário</h5>
            <Row className="mb-3">
                <Form.Group as={Col} md="4" controlId="validationCustom01" className='mb-3'>
                    <Form.Label>Tipo</Form.Label>
                    <div className='d-flex justify-content-between'>
                        <InputGroup onClick={() => setTipoSelecionado('Palavra')} hasValidation>
                            <InputGroup.Radio name='checkboxPalavra' readOnly checked={tipoSelecionado === "Palavra"} />
                            <Form.Control value={"Palavra"} readOnly style={{ cursor: 'pointer' }} />
                        </InputGroup>
                        <span className='ms-2'></span>
                        <InputGroup onClick={() => setTipoSelecionado('Frase')} hasValidation>
                            <InputGroup.Radio name='checkboxFrase' readOnly checked={tipoSelecionado === "Frase"} />
                            <Form.Control value={"Frase"} readOnly style={{ cursor: 'pointer' }} />
                        </InputGroup>
                    </div>
                </Form.Group>
                <Form.Group as={Col} md="4" controlId="validationCustom01" className='mb-3'>
                    <Form.Label>Palavra</Form.Label>
                    <Form.Control
                        required
                        type="text"
                        name="palavra"
                        placeholder={`${tipoSelecionado} em inglês`}
                        defaultValue={vocabulario?.emIngles || ''}
                    />
                    <Form.Control.Feedback type="invalid">
                        Por favor, insira a palavra.
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group as={Col} md="4" controlId="validationCustom02" className='mb-3'>
                    <Form.Label>Tradução</Form.Label>
                    <Form.Control
                        required
                        type="text"
                        name="traducao"
                        placeholder="Tradução em português"
                        defaultValue={vocabulario?.traducao || ''}
                    />
                    <Form.Control.Feedback type="invalid">
                        Por favor, insira a tradução.
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group as={Col} md="4" controlId="validationCustom02" className='mb-3'>
                    <Form.Label>Explicação</Form.Label>
                    <Form.Control
                        type="text"
                        name="explicacao"
                        placeholder={`Como e quando usar a ${tipoSelecionado.toLowerCase()}?`}
                        defaultValue={vocabulario?.explicacao || ''}
                    />
                </Form.Group>
            </Row>
            <div className='d-flex justify-content-between'>
                <Button type="submit" disabled={isSubmitting}>Salvar</Button>
                {id > 0 &&
                    <Button onClick={() => deletarVocabulario()} disabled={isSubmitting} type='button' variant='danger' className='ms-2'><i className="fa-solid fa-trash"></i></Button>
                }
            </div>
        </Form>
    );
}

export default FormCadastroVocabulario;
