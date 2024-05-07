const { env } = require('process');

const target = 'https://localhost:7088';  // URL fija para tu API

const PROXY_CONFIG = [
  {
    context: ["/api"],  // Asegúrate de que esto coincide con los endpoints de tu API
    target,
    secure: false,  // Cambia a `true` si estás usando HTTPS
    logLevel: 'debug'  // Opcional, para depurar problemas de proxy
  }
]

module.exports = PROXY_CONFIG;
