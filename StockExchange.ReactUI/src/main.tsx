import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import '/styles.css'
import Solicitacao from './app/components/solicitacao/Solicitacao'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Solicitacao retorno={{ resultadoBruto: "", resultadoLiquido: "" }}></Solicitacao>
  </StrictMode>,
)
