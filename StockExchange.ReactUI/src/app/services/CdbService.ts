import axios from 'axios';

import type { Retorno } from '../models/Retorno';

export class CdbService {
    // Sample URL"http://localhost:5041/Cdb/SolicitarCalculoInvestimento/SolicitarCalculoInvestimento?Valor=1&Meses=2"
    private readonly baseURL = 'http://localhost';
    private readonly port = '5041'; // change to 7300 to dockerize or 5041 to localhost
    private readonly controller = 'Cdb';

    async solicitarCalculoInvestimento(investimento: number, meses: number): Promise<{ data: Retorno }> {
        // Define the service action
        const action = 'SolicitarCalculoInvestimento';

        // Set the service url
        const url = `${this.baseURL}:${this.port}/${this.controller}/${action}/${action}?Valor=${investimento}&Meses=${meses}`;

        // Do the request
        return await axios.get<Retorno>(url)
            .catch((error) => {
                console.error('Erro na requisição:', error.message);
                console.error('Status:', error.response?.status);
                console.error('Detalhes:', error.response?.data); // Back-end may send messages here

                throw new Error('Erro ao solicitarCalculoInvestimento');
            });
    }
}
