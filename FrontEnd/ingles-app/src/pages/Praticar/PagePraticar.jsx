import React, { useEffect, useState } from 'react';
import { Button, Form, Spinner } from 'react-bootstrap';
import api from '../../api/api';
import { toast } from 'react-toastify';

const PagePraticar = () => {
  const [praticarPalavras, setPraticarPalavras] = useState(true);
  const [praticarFrases, setPraticarFrases] = useState(true);
  const [limite, setLimite] = useState(10);
  const [periodo, setPeriodo] = useState(1);
  const [praticando, setPraticando] = useState(false);
  const [questoes, setQuestoes] = useState([]);
  const [idxQuestaoAtual, setIdxQuestaoAtual] = useState(-1);
  const [respostaSelecionada, setRespostaSelecionada] = useState(null);
  const [loading, setLoading] = useState(true);
  const [verificado, setVerificado] = useState(false);
  const [acertou, setAcertou] = useState(null);
  const [mensagem, setMensagem] = useState(null);

  const fetchQuestoes = async () => {
    setLoading(true);

    let tipo;
    if (praticarPalavras && praticarFrases) {
      tipo = 0;
    } else if (praticarPalavras) {
      tipo = 1;
    } else {
      tipo = 2;
    }

    try {
      const response = await api.get(`/Vocabulario/Pesquisar?pesquisa=&tipo=${tipo}&periodo=${periodo}&praticando=true`);
      setQuestoes(response.data);
      setIdxQuestaoAtual(0);
    } catch (error) {
      toast.error(error?.response?.data ?? "Erro ao carregar dados");
    } finally {
      setLoading(false);
    }
  };


  const postRespostaUsuario = async () => {
    setLoading(true);
    const questaoAtual = questoes[idxQuestaoAtual];

    const respostaUsuario = {
      id: 0,
      vocabularioId: questaoAtual.vocabularioId,
      userId: 0,
      resposta: respostaSelecionada,
      similaridadeDeAcerto: 0.00,
      acertou: false,
      createdAt: "2023-12-04T23:20:22.004Z"
    };

    try {
      api.post("/Pratica/", respostaUsuario).then(response => {
        const { data } = response;

        console.log(data);
  
        const praticaUsuario = data;


  
        setAcertou(praticaUsuario.acertou);
        console.log(acertou);
        if (acertou) {
          setMensagem(`Parabéns, você acertou ${data.similaridadeDeAcerto}% :)`);
        } else {
          setMensagem(`Que pena, você errou! A resposta correta era "${data.resposta}"`)
        }
      });
    } catch (error) {
      toast.error(error?.response?.data ?? "Erro ao carregar dados");
    } finally {
      setLoading(false);
      setVerificado(true);
    }
  }

  useEffect(() => {
    if (praticando && !loading) {
      iniciarDados();
    }
  }, [praticando]);

  const iniciarDados = async () => {
    if (!praticando) setPraticando(true);
    await fetchQuestoes();
  };


  const proximaQuestao = () => {
    setRespostaSelecionada(null);
    setMensagem(null);
    setVerificado(false);
    setAcertou(null);
    if (idxQuestaoAtual == -1) {
      setIdxQuestaoAtual(0);
    } else {
      setIdxQuestaoAtual(idxQuestaoAtual + 1);
    }
  }

  const handlePalavrasChange = () => {
    setPraticarPalavras(!praticarPalavras);
  };

  const handleFrasesChange = () => {
    setPraticarFrases(!praticarFrases);
  };

  const handleChangePeriodo = (event) => {
    setPeriodo(event.target.value);
  };

  const handleChangeLimite = (event) => {
    setLimite(event.target.value);
  };

  const verificarResposta = async () => {
    if (respostaSelecionada == null) return;
    await postRespostaUsuario();
  }

  return (
    <>
      <Form>
        {!praticando &&
          <>
            <h5 className='mb-3'>O que deseja praticar?</h5>
            <Form.Check
              type="switch"
              id="custom-switch-palavras"
              label="Palavras"
              checked={praticarPalavras}
              onChange={handlePalavrasChange}
            />
            <Form.Check
              type="switch"
              label="Frases"
              id="custom-switch-frases"
              checked={praticarFrases}
              onChange={handleFrasesChange}
            />
            <div className="row mt-4">
              <div className="col-6">
                <label htmlFor='periodo' className='m-0 p-0 text-truncate'>Em qual período?</label>
                <Form.Select
                  name='periodo'
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
              <div className="col-6">
                <label htmlFor='limite' className='m-0 p-0 text-truncate'>Núm. vocabulários?</label>
                <Form.Select
                  name='periodo'
                  className='mb-2'
                  onChange={handleChangeLimite}
                  value={limite}
                >
                  <option value={1}>10</option>
                  <option value={2}>20</option>
                  <option value={3}>30</option>
                  <option value={4}>40</option>
                  <option value={5}>Sem limite</option>
                </Form.Select>
              </div>
            </div>
          </>
        }

        {praticando && idxQuestaoAtual != -1 && (
          <div>
            <h5 className='mb-3'>{questoes[idxQuestaoAtual]?.questao}</h5>

            {questoes[idxQuestaoAtual]?.opcoes &&
              questoes[idxQuestaoAtual].opcoes?.map((op, idx) => (
                <label className={`d-flex p-2 mb-2 w-100 bg-dark border rounded-3 ${respostaSelecionada == op && verificado && (acertou ? 'border-success' : 'border-danger')}`} htmlFor={idx + 1} key={idx}>
                  <Form.Check className='ms-2'
                    disabled={verificado && acertou !== null}
                    type="radio"
                    id={idx + 1}
                    name="resposta"
                    label={op}
                    onChange={() => setRespostaSelecionada(op)}
                  />
                </label>
              ))
            }

            {(questoes[idxQuestaoAtual]?.opcoes?.length ?? 0) == 0 &&
              <>
                <Form.Group className="mb-3" controlId="exampleForm.ControlTextarea1">
                  <Form.Label>Sua resposta</Form.Label>
                  <Form.Control placeholder='Digite sua resposta aqui...' readOnly={verificado && acertou !== null} className={`${verificado && (acertou ? 'border-success' : 'border-danger')}`} as="textarea" rows={2} onChange={(e) => setRespostaSelecionada(e.target.value)} />
                </Form.Group>
              </>
            }
          </div>
        )}

        {(verificado && acertou !== null) &&
          <div className={`alert ${acertou ? 'alert-success' : 'alert-danger'}`}>
            {mensagem}
          </div>
        }

        {!praticando &&
          <>
            <Button
              className='w-100 mt-2'
              variant='primary'
              onClick={() => {
                iniciarDados();
              }}
            >
              Iniciar
            </Button>
          </>
        }

        {praticando && (
          <>
            {!verificado && acertou === null &&
              <Button className='w-100 mt-2' variant='warning' onClick={verificarResposta}>
                {loading && <Spinner
                  className='text-dark'
                  as="span"
                  animation="border"
                  size="sm"
                  role="status"
                  aria-hidden="true"
                />} Verificar Resposta
              </Button>
            }

            {(verificado && acertou !== null) &&
              <Button className='w-100 mt-2' variant='light' onClick={proximaQuestao}>
                Próxima questão
              </Button>
            }

            <Button
              className='btn-sm mt-5'
              variant='danger'
              onClick={() => {
                setPraticando(false);
                setVerificado(false);
              }}
            >
              Encerrar prática
            </Button>
          </>
        )}
      </Form>
    </>
  );
};

export default PagePraticar;
