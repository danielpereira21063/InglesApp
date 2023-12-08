import { useEffect, useState } from 'react';
import { Form, Spinner } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import api from '../../api/api';
import { toast } from 'react-toastify';
import { VozUtil } from '../../util/VozUtil';

function Home() {
    const [vocabularies, setVocabularies] = useState([]);
    const [tipoSelecionado, setTipoSelecionado] = useState(0);
    const [periodo, setPeriodo] = useState(5);
    const [loading, setLoading] = useState(true);
    const [periodoFormatado, setPeriodoFormatado] = useState("");
    const [pesquisa, setPesquisa] = useState("");
    useEffect(() => {
        setVocabularies([]);
        const fetchVocabularies = async () => {
            setLoading(true);

            try {
                const response = await api.get(`/Vocabulario/Pesquisar?pesquisa=${pesquisa}&tipo=${tipoSelecionado}&periodo=${periodo}`);
                setVocabularies(response.data);
            } catch (error) {
                toast.error(error?.response?.data ?? "Erro ao carregar dados");
            }

            setLoading(false);
        };


        switch (Number(periodo)) {
            case 1:
                setPeriodoFormatado("Hoje");
                break;
            case 2:
                setPeriodoFormatado("Essa semana");
                break;
            case 3:
                setPeriodoFormatado("Nas Últimas 2 semanas")
                break;
            case 4:
                setPeriodoFormatado("Esse mês")
                break;
            case 5:
                setPeriodoFormatado("No total")
                break;
        }

        setTimeout(() => {
            fetchVocabularies();
        }, 100);
    }, [tipoSelecionado, periodo, pesquisa]);

    const handleChangeSelecionado = (event) => {
        setTipoSelecionado(event.target.value);
    };

    const handleChangePeriodo = (event) => {
        setPeriodo(event.target.value);
        console.log(periodo)
    };

    const handleChangePesquisa = (event) => {
        setPesquisa(event.target.value);
    };

    const handleEditClick = (vocabularyId) => {
        window.location.href = "/vocabulario/" + vocabularyId;
    };
    return (
        <>
            {vocabularies.length > 0 &&
                <div className='alert alert-light p-3'>
                    <p className='p-0 m-0'><i className="fa-solid fa-hands-clapping"></i> Você aprendeu <strong>{vocabularies.length} {tipoSelecionado == 1 ? 'Frases'.toLowerCase() : (tipoSelecionado == 2 ? 'Palavras'.toLowerCase() : 'vocabulários')}</strong> {periodoFormatado.toLowerCase()}!</p>
                </div>
            }

            {(vocabularies.length == 0 && !loading) && (
                <div className='alert alert-warning'>Oooops, nenhum vocabulário foi encontrado para este filtro :(</div>
            )}

            <div className='row'>
                <div className="col">
                    <Form.Select
                        aria-label="Default select example"
                        className='mb-2'
                        onChange={handleChangeSelecionado}
                        value={tipoSelecionado}
                    >
                        <option value={0}>Tudo</option>
                        <option value={1}>Palavras</option>
                        <option value={2}>Frases</option>
                    </Form.Select>
                </div>
                <div className="col">
                    <Form.Select
                        className='mb-2'
                        onChange={handleChangePeriodo}
                        value={periodo}
                    >
                        <option value={1}>Hoje</option>
                        <option value={2}>Essa semana</option>
                        <option value={3}>Duas semanas</option>
                        <option value={4}>Esse mês</option>
                        <option value={5}>Tudo</option>
                    </Form.Select>
                </div>

            </div>

            <Form className="d-flex mb-2">
                <Form.Control
                    onChange={handleChangePesquisa}
                    type="search"
                    placeholder="Busque palavras ou frases..."
                    aria-label="Search"
                    value={pesquisa}
                />
            </Form>

            {vocabularies.map((vocab) => (
                <Card key={vocab.id} className='border shadow mb-3'>
                    <Card.Header as="h5" cla style={{ cursor: 'pointer'}} className='bg-secondary'>
                        <div className="d-flex">
                            <div className="col-11">
                                <span onClick={() => VozUtil.falarTexto(vocab.emIngles)}>{vocab.emIngles} <i className="fa-solid fa-volume-high fa-2xs"></i></span>
                            </div>
                            <div className="col-1">
                                <Button className='btn-sm text-light' variant='outline-secondary' onClick={() => handleEditClick(vocab.id)}>
                                    <i className="fa-solid fa-pencil"></i>
                                </Button>
                            </div>
                        </div>
                    </Card.Header>
                    <Card.Body>
                        <Card.Text style={{ cursor: 'pointer' }} onClick={() => VozUtil.falarTexto(vocab.traducao, 1.0, 'pt_BR')}>
                            <strong>{vocab.traducao}</strong> <i className="fa-solid fa-volume-high fa-2xs"></i>
                        </Card.Text>
                        <Card.Text>
                            <small>{vocab.explicacao}</small>
                        </Card.Text>
                    </Card.Body>

                    {/* <Card.Footer>
                        <div className='d-flex justify-content-between'>

                        </div>
                    </Card.Footer> */}
                </Card>
            ))}

            {loading && (
                <div className='d-flex justify-content-center'>
                    <Spinner animation="border" role="status" variant='white'>
                        <span className="visually-hidden">Loading...</span>
                    </Spinner>
                </div>
            )}
        </>
    );
}

export default Home;
