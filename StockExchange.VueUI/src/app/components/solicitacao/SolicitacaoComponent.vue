<script setup lang="ts">
import { reactive, ref } from 'vue';
import { CdbService } from '../../services/CdbService';
import type { RetornoModel } from '../../models/RetornoModel';
import VueImg from '../../../../public/Vue.png';
import TSImg from '../../../../public/TS.png';
import JSImg from '../../../../public/JS.png';
import HTML5Img from '../../../../public/HTML5.png';
import CSS3Img from '../../../../public/CSS3.png';

type FormData = {
  investimento: string;
  meses: string;
};

const retornoModel = ref<RetornoModel>({ resultadoBruto: '0', resultadoLiquido: '0' });
const snack = reactive({ open: false, message: '', severity: 'success' as 'success' | 'error' });

const form = reactive<FormData>({
  investimento: '',
  meses: ''
});

const errors = reactive<Record<keyof FormData, string>>({
  investimento: '',
  meses: ''
});

function currency(value: string | undefined): string {
  const number = Number(value);

  if (Number.isNaN(number)) {
    return 'R$ 0,00';
  }

  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  }).format(number);
}

function parseValue(value: string): number {
  const normalized = value
    .replace('R$', '')
    .replace(/\./g, '')
    .replace(',', '.')
    .trim();

  return Number(normalized);
}

function clearFields(): void {
  form.investimento = '';
  form.meses = '';
  errors.investimento = '';
  errors.meses = '';
  retornoModel.value = { resultadoBruto: '0', resultadoLiquido: '0' };
}

function blockDecimal(event: KeyboardEvent): void {
  if (event.key === '.' || event.key === ',') {
    event.preventDefault();
  }
}

function validarFormulario(): boolean {
  let isValid = true;

  errors.investimento = '';
  errors.meses = '';

  if (!form.investimento.trim()) {
    errors.investimento = 'Campo monetário obrigatório.';
    isValid = false;
  } else {
    const valor = parseValue(form.investimento);
    if (Number.isNaN(valor) || valor <= 0) {
      errors.investimento = 'O valor deve ser maior que R$ 0,00.';
      isValid = false;
    }
  }

  if (!form.meses.trim()) {
    errors.meses = 'Campo numérico obrigatório.';
    isValid = false;
  } else {
    const meses = Number(form.meses);
    if (!Number.isInteger(meses) || meses < 2 || meses > 1200) {
      errors.meses = 'O valor deve estar entre 2 e 1200 meses.';
      isValid = false;
    }
  }

  return isValid;
}

async function onSubmit(): Promise<void> {
  if (!validarFormulario()) {
    return;
  }

  try {
    const investimento = parseValue(form.investimento);
    const meses = Number(form.meses);

    const response = await new CdbService().solicitarCalculoInvestimento(investimento, meses);
    retornoModel.value = response.data;

    snack.message = 'Investimento calculado com sucesso!';
    snack.severity = 'success';
    snack.open = true;
  } catch (error) {
    snack.message = error instanceof Error ? error.message : 'Erro ao calcular investimento.';
    snack.severity = 'error';
    snack.open = true;
  }
}

function onCloseSnack(): void {
  snack.open = false;
}
</script>

<style scoped>
.card, .internalCard {
    box-shadow: 0 4px 8px rgba(183, 28, 28, 0.1);
    border-radius: 12px;
}

.card {
    padding: 2rem 3rem;
    max-width: 500px;
    background-color: var(--primary-white);
}

.internalCard {
    margin-bottom: 25px;
    padding-bottom: 29px;
    border: 1px dashed var(--primary-color);
}

.grid {
    display: grid;
    grid-template-columns: 130px max-content;
    row-gap: 0.5rem;
    column-gap: 0.5rem;
    justify-content: center;
}

.grid dt, .grid dd {
    margin: 0;
    display: block;
    text-align: left;
}

.grid dt {
    font-weight: bold;
    color: var(--secondary-color);
}

.link {
    text-decoration: none;
    color: var(--font-color);
}

.link:hover {
    text-decoration: underline;
}

.textField {
    width: 212px !important;
}

.buttonSpaceLeft, .buttonSpaceRight {
    margin-top: 42px !important;
}

.buttonSpaceLeft {
    margin-left: -23px !important;
    margin-right: 40px !important;
}

h1, h2 {
    color: var(--primary-color);
}

p, figure {
    font-size: 0.9rem;
}

h1 {
    margin: 6px 0 1rem 0;
}

strong {
    color: var(--secondary-color);
}

button {
    margin-top: 30px;
}

figure {
    margin-top: 25px;
}

figure figcaption {
    margin-bottom: 0.3rem;
}
</style>

<template>
    <div class="card">
        <h1>Bem-vindo(a) à Aplicação<br />Vue CDB</h1>
        <div class="internalCard">
            <h2>Solicitar o Calculo do Investimento CDB</h2>
            <v-form @submit.prevent="onSubmit">
                <v-text-field v-model="form.investimento"
                    label="Investimento *"
                    placeholder="Ex: R$ 0,01"
                    prefix="R$ "
                    variant="outlined"
                    density="comfortable"
                    :error-messages="errors.investimento ? [errors.investimento] : []"
                    @keydown="blockDecimal"
                />

                <v-text-field v-model="form.meses"
                    label="Meses *"
                    type="number"
                    placeholder="Ex: 24"
                    variant="outlined"
                    density="comfortable"
                    :error-messages="errors.meses ? [errors.meses] : []"
                    @keydown="blockDecimal"
                />

                <v-btn color="primary" type="submit">Solicitar</v-btn>

                <v-btn class="ml-2" variant="outlined" color="primary" type="button" @click="clearFields">
                    Limpar
                </v-btn>
            </v-form>
        </div>
        <div class="internalCard">
            <h2>Resultado do Investimento CDB</h2>
            <dl class="grid">
                <dt>Retorno Bruto:</dt><dd>{{currency(retornoModel?.resultadoBruto)}}</dd>
                <dt>Retorno Líquido:</dt><dd>{{currency(retornoModel?.resultadoLiquido)}}</dd>
            </dl>
        </div>
        <p>
            <a class="link" target="_blank" href="https://github.com/rodrigocdellu/stockexchange.front-end">Para <strong>Maiores Informações</strong> veja meu GitHub!</a>
        </p>
        <figure>
            <figcaption>Desenvolvido Com</figcaption>
            <a class="link" target="_blank" href="https://vuejs.org"><img :src="VueImg" alt="Vue" /></a>
            &nbsp;&nbsp;
            <a class="link" target="_blank" href="https://www.typescriptlang.org"><img :src="TSImg" alt="TypeScript" /></a>
            &nbsp;&nbsp;
            <a class="link" target="_blank" href="https://developer.mozilla.org/pt-BR/docs/Web/JavaScript"><img :src="JSImg" alt="JavaScrit" /></a>
            &nbsp;&nbsp;
            <a class="link" target="_blank" href="https://www.w3.org/html"><img :src="HTML5Img" alt="HTML 5" /></a>
            &nbsp;&nbsp;
            <a class="link" target="_blank" href="https://www.w3.org/Style/CSS"><img :src="CSS3Img" alt="CSS 3" /></a>
        </figure>

        <v-snackbar v-model="snack.open" :timeout="snack.severity === 'success' ? 3000 : 5000" location="top">
            <v-alert :type="snack.severity" closable @click:close="onCloseSnack">
                {{ snack.message }}
            </v-alert>
        </v-snackbar>
    </div>
</template>
