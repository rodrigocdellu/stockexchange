import type { RetornoModel } from '../models/RetornoModel';

export class CdbService {
  private readonly baseURL = import.meta.env.VITE_WEBAPI_URL ?? 'http://localhost:5041';
  private readonly controller = 'Cdb';

  async solicitarCalculoInvestimento(investimento: number, meses: number): Promise<{ data: RetornoModel }> {
    const action = 'SolicitarCalculoInvestimento';
    const url = `${this.baseURL}/${this.controller}/${action}/${action}?Valor=${encodeURIComponent(String(investimento))}&Meses=${encodeURIComponent(String(meses))}`;

    const response = await fetch(url);

    if (!response.ok) {
      throw new Error('Erro ao solicitarCalculoInvestimento');
    }

    const data = (await response.json()) as RetornoModel;

    return { data };
  }
}
