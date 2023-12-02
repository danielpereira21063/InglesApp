const falarTexto = (texto, velocidade = 1.0, idioma = 'en-US') => {
    if(!texto) return;

    let ut = new SpeechSynthesisUtterance(texto);
    ut.lang = idioma;
    ut.rate = velocidade;

    window.speechSynthesis.speak(ut);
}

export const VozUtil = {
    falarTexto
}