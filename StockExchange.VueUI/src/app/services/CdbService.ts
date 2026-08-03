import type { RetornoModel } from '../models/RetornoModel';

export class CdbService {
    // Sample URL"http://localhost:5041/Cdb/SolicitarCalculoInvestimento/SolicitarCalculoInvestimento?Valor=1&Meses=2"
    private readonly baseURL = import.meta.env.VITE_WEBAPI_URL ?? 'http://localhost:5041';
    private readonly controller = 'Cdb';

    async solicitarCalculoInvestimento(investimento: number, meses: number): Promise<{ data: RetornoModel }> {
        // Define the service action
        const action = 'SolicitarCalculoInvestimento';

        // Set the service url (encode params)
        const url = `${this.baseURL}/${this.controller}/${action}/${action}?Valor=${encodeURIComponent(String(investimento))}&Meses=${encodeURIComponent(String(meses))}`;

        try {
            const response = await fetch(url);

            if (!response.ok) {
                console.error('Erro na requisição:', response.status, await response.text());
                throw new Error('Erro ao solicitarCalculoInvestimento');
            }

            const data = (await response.json()) as RetornoModel;

            return { data };
        } catch (error: any) {
            console.error('Erro na requisição:', error?.message ?? error);
            
            throw new Error('Erro ao solicitarCalculoInvestimento');
        }
    }
}
