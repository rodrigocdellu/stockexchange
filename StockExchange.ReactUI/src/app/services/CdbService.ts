import axios from 'axios';

import type { RetornoModel } from '../models/RetornoModel';

export class CdbService {
    // Sample URL"http://localhost:5041/Cdb/SolicitarCalculoInvestimento/SolicitarCalculoInvestimento?Valor=1&Meses=2"
    private readonly baseURL = import.meta.env.VITE_WEBAPI_URL ?? 'http://localhost:5041';
    private readonly controller = 'Cdb';

    async solicitarCalculoInvestimento(investimento: number, meses: number): Promise<{ data: RetornoModel }> {
        // Define the service action
        const action = 'SolicitarCalculoInvestimento';

        // Set the service url
        const url = `${this.baseURL}/${this.controller}/${action}/${action}?Valor=${investimento}&Meses=${meses}`;

        // Do the request
        return await axios.get<RetornoModel>(url)
            .catch((error) => {
                console.error('Erro na requisição:', error.message);
                console.error('Status:', error.response?.status);
                console.error('Detalhes:', error.response?.data); // Back-end may send messages here

                throw new Error('Erro ao solicitarCalculoInvestimento');
            });
    }
}
