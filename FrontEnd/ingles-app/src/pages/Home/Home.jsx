import { Container, Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';

function Home() {
    const frases = [
        {
            id: 1,
            ingles: "Hello",
            traducao: "Olá",
            explicacao: "Saudação comum para cumprimentar alguém."
        },
        {
            id: 2,
            ingles: "Goodbye",
            traducao: "Adeus",
            explicacao: "Expressão usada para se despedir."
        },
        {
            id: 3,
            ingles: "Thank you",
            traducao: "Obrigado(a)",
            explicacao: "Expressão de gratidão após receber ajuda ou um favor."
        },
        {
            id: 4,
            ingles: "Please",
            traducao: "Por favor",
            explicacao: "Usado para fazer um pedido de maneira educada."
        },
        {
            id: 5,
            ingles: "I love you",
            traducao: "Eu te amo",
            explicacao: "Expressão de afeto e carinho."
        }
    ];

    return (
        <>
            <Form.Select aria-label="Default select example" className='mb-3'>
                <option defaultValue value="1">Frases</option>
                <option value="2">Palavras</option>
            </Form.Select>

            {frases.map((frase) => (
                <Card key={frase.id} className='mb-3'>
                    <Card.Header as="h5">
                        {frase.ingles}
                    </Card.Header>
                    <Card.Body>
                        <Card.Title>{frase.traducao}</Card.Title>
                        <Card.Text>
                            {frase.explicacao}
                        </Card.Text>

                        <div className='d-flex justify-content-between'>
                            <div>
                                <Button className='btn-sm' variant="secondary"><i className="fa-solid fa-volume-high"></i></Button>
                                <span className='mx-1'></span>
                                <Button className='btn-sm' variant="light"><i className="fa-solid fa-language"></i></Button>
                            </div>

                            <Button className='btn-sm' variant='outline-primary'><i className="fa-solid fa-pencil"></i></Button>
                        </div>

                    </Card.Body>
                </Card>
            ))}
        </>
    );
}

export default Home;
