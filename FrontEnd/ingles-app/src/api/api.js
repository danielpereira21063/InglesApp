import axios from "axios";

const emProducao = process.env.NODE_ENV != "development";

let baseUrl = "https://localhost:7014/api";

if (emProducao) baseUrl = "https://ingles-app.api.danielsanchesdev.com.br/api";


const getUsuarioLocal = () => {
  const usuarioJSON = localStorage.getItem("usuario");
  return usuarioJSON ? JSON.parse(usuarioJSON) : null;
};

const adicionarTokenAoCabecalho = () => {
  const usuario = getUsuarioLocal();
  
  if (usuario && usuario?.token) {
    return {
      Authorization: `Bearer ${usuario.token}`
    };
  }
  
  return {};
};
const api = axios.create({
  baseURL: baseUrl,
  headers: adicionarTokenAoCabecalho()
});

api.interceptors.request.use((config) => {
  config.headers = {
    ...config.headers,
    ...adicionarTokenAoCabecalho()
  };
  
  return config;
});

export default api;
