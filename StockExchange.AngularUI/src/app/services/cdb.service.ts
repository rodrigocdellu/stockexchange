import { environment } from '../../environments/environment'; // 2025/05/31 - To parameterize for Docker Compose
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators'; // 2025/04/24 - To handle errors

import { RetornoModel } from '../models/retorno.model';

@Injectable({
    providedIn: 'root'
})
export class CdbService {
    // Sample URL"http://localhost:5041/Cdb/SolicitarCalculoInvestimento/SolicitarCalculoInvestimento?Valor=1&Meses=2"
    private readonly baseURL = environment.ANGULAR_WEBAPI_URL ?? 'http://localhost:5041';
    private readonly controller = "Cdb";

    constructor(private readonly http: HttpClient) {
    }

    solicitarCalculoInvestimento(investimento: number, meses: number): Observable<RetornoModel> {
        // Define the service action
        const action = "SolicitarCalculoInvestimento";

        // Set the service url
        const url = `${this.baseURL}/${this.controller}/${action}/${action}?Valor=${investimento}&Meses=${meses}`;

        // Do the request
        return this.http.get<RetornoModel>(url).pipe(
            catchError((error: HttpErrorResponse) => {
                console.error("Erro na requisição:", error.message);
                console.error("Status:", error.status);
                console.error("Detalhes:", error.error); // Back-end may send messages here

                return throwError(() => new Error('Erro ao solicitarCalculoInvestimento'));
            })
        );
    }
}
