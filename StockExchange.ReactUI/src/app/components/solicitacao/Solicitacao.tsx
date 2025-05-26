import type { Retorno } from '../../models/Retorno'
import styles from './Solicitacao.module.css'

export default function Solicitacao({ retorno }: Retorno) {
    return (
        <div className={styles.card}>
            <h1>Bem-vindo(a) à Aplicação Angular CDB</h1>
            <div className={styles.internalCard}>
                <h2>Solicitar o Calculo do Investimento CDB</h2>
                <h3>###FORMULÁRIO AQUI###</h3>
            </div>
            <div className={styles.internalCard}>
                <h2>Resultado do Investimento CDB</h2>
                <dl className={styles.grid}>
                    <dt>Retorno Bruto:</dt><dd>{ retorno.resultadoBruto }</dd>
                    <dt>Retorno Líquido:</dt><dd>{ retorno.resultadoLiquido }</dd>
                </dl>
            </div>
            <p>
                <a className={styles.link} target="_blank" href="https://github.com/rodrigocdellu/stockexchange.front-end">Para <strong>Maiores Informações</strong> veja meu GitHub!</a>
            </p>
            <figure>
                <figcaption>Desenvolvido Com</figcaption>
                <a className={styles.link} target="_blank" href="https://react.dev"><img src="React.png" alt="React" /></a>
                &nbsp;&nbsp;
                <a className={styles.link} target="_blank" href="https://vite.dev"><img src="Vite.png" alt="Vite" /></a>
                &nbsp;&nbsp;
                <a className={styles.link} target="_blank" href="https://www.typescriptlang.org"><img src="TS.png" alt="TypeScript" /></a>
                &nbsp;&nbsp;
                <a className={styles.link} target="_blank" href="https://developer.mozilla.org/pt-BR/docs/Web/JavaScript"><img src="JS.png" alt="JavaScrit" /></a>
                &nbsp;&nbsp;
                <a className={styles.link} target="_blank" href="https://www.w3.org/html"><img src="HTML5.png" alt="HTML 5" /></a>
                &nbsp;&nbsp;
                <a className={styles.link} target="_blank" href="https://www.w3.org/Style/CSS"><img src="CSS3.png" alt="CSS 3" /></a>
            </figure>
        </div>
    )
}
