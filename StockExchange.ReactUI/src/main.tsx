import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './styles.css'
import SolicitacaoComponent from './app/components/solicitacao/SolicitacaoComponent'

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <SolicitacaoComponent />
    </StrictMode>,
)
