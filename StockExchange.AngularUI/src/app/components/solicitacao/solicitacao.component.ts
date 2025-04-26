import { Component, ViewEncapsulation } from '@angular/core';
import { CdbserviceService } from '../../services/cdbservice.service';
import { Retorno } from '../../models/retorno.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-solicitacao',
    standalone: false,
    templateUrl: './solicitacao.component.html',
    styleUrl: './solicitacao.component.css',
    encapsulation: ViewEncapsulation.None // // 2025/04/22 - To view the component CSS
})
export class SolicitacaoComponent {
    solicitacaoForm: FormGroup;
    retorno: Retorno = {} as Retorno;
    validationMessage: string = "";

    constructor(private readonly formBuilder: FormBuilder, private readonly cdbserviceService: CdbserviceService, private readonly snackBar: MatSnackBar) {
        this.solicitacaoForm = this.formBuilder.group({
            investimento: ['', [Validators.required, Validators.min(0.01)]], // Only values greater than 0.01
            meses: ['', [Validators.required, Validators.pattern('^[0-9]+$'), Validators.min(1)]] // Only positive integers greater than 0
        });
    }

    private validaEstrutura(): { investimento: number, meses: number } | null {
        if (this.solicitacaoForm.valid) {
            // Valida o parâmetro investimento
            const investimentoFormatado = this.solicitacaoForm.get('investimento')?.value;
    
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
            const meses = this.solicitacaoForm.value["meses"];

            // Retorna os parametros válidos
            return { investimento, meses };
        }
    
        // Retorna os parametros inválidos
        return null;
    }
    
    private solicitarCalculoInvestimento(investimento: number, meses: number): void {
        this.cdbserviceService.solicitarCalculoInvestimento(investimento, meses).subscribe({
            next: (res: Retorno) => {
                this.retorno = res;
    
                this.snackBar.open('Investimento calculado com sucesso!', 'Fechar', {
                    duration: 3000,
                    panelClass: ['mat-primary'],
                    horizontalPosition: 'center',
                    verticalPosition: 'top',
                });
            },
            error: (err) => {
                this.snackBar.open(err.message, 'Fechar', {
                    duration: 5000,
                    panelClass: ['mat-warn'],
                    horizontalPosition: 'center',
                    verticalPosition: 'top',
                });
            }
        });
    }

    blockDecimal(event: KeyboardEvent): void {
        if (event.key === '.' || event.key === ',') {
            event.preventDefault();
        }
    }

    clearFields(): void {
        // Clear form fields
        this.solicitacaoForm.patchValue({
            investimento: '',
            meses: ''
        });

        // Clear retorno fields
        this.retorno = {} as Retorno;
    }

    onSubmit() {
        // Valida os dados de entrada
        const dados = this.validaEstrutura();

        // Se válidos
        if (dados) {
            
            // Realiza a requisição
            this.solicitarCalculoInvestimento(dados.investimento, dados.meses);
        }
    }    
}
