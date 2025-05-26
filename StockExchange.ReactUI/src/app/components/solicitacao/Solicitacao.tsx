import type { Retorno } from '../../models/Retorno'
import styles from './Solicitacao.module.css'
import { useForm } from 'react-hook-form'
import { TextField, Button } from '@mui/material';

export default function Solicitacao({ retorno }: Retorno) {
    const { register, handleSubmit, formState: { errors }, reset } = useForm<FormData>();

    type FormData = {
        investimento: number;
        meses: number;
    };

    const onSubmit = (data: FormData) => {
        console.log(data);
        // fazer chamada para API etc.
    };

    return (
        <div className={styles.card}>
            <h1>Bem-vindo(a) à Aplicação<br />React CDB</h1>
            <div className={styles.internalCard}>
                <h2>Solicitar o Calculo do Investimento CDB</h2>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <TextField
                        label="Investimento*"
                        variant="outlined"
                        fullWidth
                        margin="normal"
                        {...register("investimento", { required: "Campo obrigatório" })}
                        error={!!errors.investimento}
                        helperText={errors.investimento?.message}
                        className={styles.textField}
                    />

                    <TextField
                        label="Meses*"
                        variant="outlined"
                        type="number"
                        fullWidth
                        margin="normal"
                        {...register("meses", {
                        required: "Campo obrigatório",
                        min: { value: 1, message: "Mínimo de 1 mês" },
                        max: { value: 1200, message: "Máximo de 1200 meses" }
                        })}
                        error={!!errors.meses}
                        helperText={errors.meses?.message}
                        className={styles.textField}
                    />
                    
                    <Button className={styles.buttonSpaceLeft} variant="outlined" color="primary" type="submit">Solicitar</Button>
                    
                    <Button className={styles.buttonSpaceRight} variant="outlined" color="primary" onClick={() => reset()}>Limpar</Button>
                </form>
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
