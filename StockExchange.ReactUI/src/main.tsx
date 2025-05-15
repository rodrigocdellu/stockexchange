import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import '/styles.css'
import Solicitacao from './app/components/solicitacao/solicitacao.component'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Solicitacao></Solicitacao>
  </StrictMode>,
)
