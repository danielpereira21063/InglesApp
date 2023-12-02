import React, { useEffect, useState } from 'react';
import { Button, Form } from 'react-bootstrap';

const PagePraticar = () => {
  const [praticarPalavras, setPraticarPalavras] = useState(true);
  const [praticarFrases, setPraticarFrases] = useState(true);
  const [limite, setLimite] = useState(10);
  const [periodo, setPeriodo] = useState(1);
  const [praticando, setPraticando] = useState(false);
  const [vocabularios, setVocabularios] = useState([]);
  const [perguntaAtual, setPerguntaAtual] = useState(null);
  const [respostaSelecionada, setRespostaSelecionada] = useState(null);

  useEffect(() => {
    const listaVocabularios = [
      { Id: 1, EmIngles: 'Hello', Traducao: 'Olá', Explicacao: 'Saudação em inglês', TipoVocabulario: 'Palavra' },
      { Id: 2, EmIngles: 'World', Traducao: 'Mundo', Explicacao: 'Planeta Terra', TipoVocabulario: 'Palavra' }
    ];

    setVocabularios(listaVocabularios);
  }, []);

  const gerarPergunta = () => {
    const perguntaAleatoria = vocabularios[Math.floor(Math.random() * vocabularios.length)];
    setPerguntaAtual(perguntaAleatoria);
    setRespostaSelecionada(null);
  };

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

  const handleRespostaChange = (resposta) => {
    setRespostaSelecionada(resposta);
  };

  const verificarResposta = () => {
    if (respostaSelecionada === perguntaAtual.Traducao) {
      console.log('Resposta correta!');
    } else {
      console.log('Resposta incorreta!');
    }

    gerarPergunta();
  };

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
              <label htmlFor='periodo' className='m-0 p-0'>Em qual período?</label>
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
              <label htmlFor='limite' className='m-0 p-0'>Quantos vocabulários?</label>
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




        {praticando && perguntaAtual && (
          <div>
            <h5 className="mt-4">Pergunta:</h5>
            <p>{perguntaAtual.EmIngles}</p>

            <h5>Opções de Resposta:</h5>
            {/* Aqui você pode gerar opções de resposta com base na lista de vocabulários */}
            {vocabularios.map((vocabulario) => (
              <Form.Check
                key={vocabulario.Id}
                type="radio"
                name="resposta"
                label={vocabulario.Traducao}
                checked={respostaSelecionada === vocabulario.Traducao}
                onChange={() => handleRespostaChange(vocabulario.Traducao)}
                />
                ))}
          </div>
        )}
        {!praticando &&
        <>
            <Button
              className='w-100 mt-2'
              variant='primary'
              onClick={() => {
                setPraticando(true);
                gerarPergunta();
              }}
            >
              Iniciar
            </Button>
        </>
        }

        {praticando && (
          <>
            <Button className='w-100 mt-2' variant='success' onClick={verificarResposta}>
              Verificar Resposta
            </Button>
            <Button
            className='mt-3'
            variant='danger'
            onClick={() => {
              setPraticando(false);
            }}
          >
            Sair
          </Button>
          </>
        )}
      </Form>
    </>
  );
};

export default PagePraticar;
