import { useState } from 'react';
import { CdbService } from '../../services/CdbService';
import { type RetornoModel } from '../../models/RetornoModel';
import styles from './Solicitacao.module.css';
import { NumericFormat, type NumberFormatValues } from 'react-number-format';
import { useForm, Controller } from 'react-hook-form';
import { TextField, Button, Snackbar, Alert } from '@mui/material';

export default function SolicitacaoComponent() {
    const [retornoModel, setRetornoModel] = useState<RetornoModel>();
    const [snack, setSnack] = useState({ open: false, message: '', severity: 'success' });
    const { control, register, handleSubmit, reset, formState: { errors } } = useForm<FormData>();

    type FormData = {
        investimento: number,
        meses: number
    };
    
    function validaEstrutura(formData: FormData): { investimento: number, meses: number } | null {
        // Valida o parâmetro investimento
        const investimentoFormatado = String(formData.investimento);

        const investimento = typeof investimentoFormatado === 'number'
            ? investimentoFormatado
            : parseFloat(
                investimentoFormatado
                    .replace('R$', '')
                    .replace(/\./g, '')
                    .replace(',', '.')
                    .trim()
            );
        
        // Valida o parâmetro meses
        const mesesFormatado = String(formData.meses);

        const meses = typeof mesesFormatado === 'number'
            ? mesesFormatado
            : parseFloat(
                mesesFormatado
                    .replace(/\./g, '')
                    .replace('.', '')
                    .replace(',', '')
                    .trim()
            );

        // Retorna os parametros válidos
        return { investimento, meses };
    }

    function solicitarCalculoInvestimento(investimento: number, meses: number): void {
        new CdbService().solicitarCalculoInvestimento(investimento, meses)
            .then((res) => {
                setRetornoModel(res.data);
                
                setSnack({
                    open: true,
                    message: 'Investimento calculado com sucesso!',
                    severity: 'success'
                });
            })
            .catch((err) => {
                setSnack({
                    open: true,
                    message: err.message || 'Erro ao calcular investimento.',
                    severity: 'error'
                });
            });
    }

    function currencyChange(onChangeFn: (value: number | undefined) => void) {
        return function (values: NumberFormatValues): void {
            onChangeFn(values.floatValue);
        };
    }

    function blockDecimal(event: React.KeyboardEvent<HTMLInputElement>): void {
        if (event.key === '.' || event.key === ',') {
            event.preventDefault();
        }
    }

    function clearFields(): void {
        // Clear form fields
        reset({
            investimento: 0,
            meses: 0
        });
    }

    function onCloseSnack(): void {
        // Close the snack
        setSnack({
            ...snack,
            open: false
        });
    };

    function onSubmit(formData: FormData): void {
        // Valida os dados de entrada
        const dados = validaEstrutura(formData);

        // Se válidos
        if (dados) {
            // Realiza a requisição
            solicitarCalculoInvestimento(dados.investimento, dados.meses);
        }   
    }

    return (
        <div className={styles.card}>
            <h1>Bem-vindo(a) à Aplicação<br />React CDB</h1>
            <div className={styles.internalCard}>
                <h2>Solicitar o Calculo do Investimento CDB</h2>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <Controller
                        name="investimento"
                        control={control}
                        rules={{
                            required: "Campo monetário obrigatório.",
                            min: { value: 0.01, message: "O valor deve ser maior que R$ 0,00." }
                        }}
                        render={({ field }) => (
                            <NumericFormat
                                label="Investimento*"
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                className={styles.textField}
                                error={!!errors.investimento}
                                helperText={errors.investimento?.message}
                                decimalScale={2}
                                thousandSeparator="."
                                decimalSeparator=","
                                prefix="R$ "
                                placeholder="Ex: R$ 0,01"
                                value={field.value ?? ''}
                                customInput={TextField}
                                onValueChange={currencyChange(field.onChange)} />
                        )} />

                    <TextField
                        label="Meses*"
                        variant="outlined"
                        type="number"
                        fullWidth
                        margin="normal"
                        className={styles.textField}
                        error={!!errors.meses}
                        helperText={errors.meses?.message}
                        placeholder="Ex: 24"
                        onKeyDown={blockDecimal}
                        {...register("meses", {
                            required: "Campo numérico obrigatório.",
                            min: {value: 2, message: "O valor deve ser maior que 1."},
                            max: {value: 1200, message: "O valor deve ser menor que 1201."}
                        })} />
                    
                    <Button className={styles.buttonSpaceLeft} variant="outlined" color="primary" type="submit">Solicitar</Button>
                    
                    <Button className={styles.buttonSpaceRight} variant="outlined" color="primary" onClick={clearFields}>Limpar</Button>
                </form>
                <Snackbar
                    open={snack.open}
                    autoHideDuration={snack.severity === 'success' ? 3000 : 5000}
                    anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
                    onClose={onCloseSnack}>
                    <Alert
                        severity={snack.severity as any}
                        sx={{ width: '100%' }}
                        onClose={onCloseSnack}>
                        {snack.message}
                    </Alert>
                </Snackbar>
            </div>
            <div className={styles.internalCard}>
                <h2>Resultado do Investimento CDB</h2>
                <dl className={styles.grid}>
                    <dt>Retorno Bruto:</dt><dd>{retornoModel?.resultadoBruto}</dd>
                    <dt>Retorno Líquido:</dt><dd>{retornoModel?.resultadoLiquido}</dd>
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
