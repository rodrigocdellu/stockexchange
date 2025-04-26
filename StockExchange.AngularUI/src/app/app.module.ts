import { DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule } from '@angular/core'; // 2025/04/22 - DEFAULT_CURRENCY_CODE, LOCALE_ID included for application culture
import ptBr from '@angular/common/locales/pt'; // 2025/04/22 - For application culture
import { registerLocaleData } from '@angular/common'; // 2025/04/22 - For application culture

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'

import { MatFormFieldModule } from '@angular/material/form-field'; // 2025/04/22 - For angular forms
import { MatInputModule } from '@angular/material/input'; // 2025/04/22 - For angular forms
import { MatButtonModule } from '@angular/material/button'; // 2025/04/22 - For angular forms
import { MatSnackBarModule } from '@angular/material/snack-bar'; // 2025/04/24 - For angular messages
import { ReactiveFormsModule } from '@angular/forms'; // 2025/04/22 - For angular forms
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask'; // 2025/04/23 - For mask angular form fields

import { SolicitacaoComponent } from './components/solicitacao/solicitacao.component';

registerLocaleData(ptBr);// 2025/04/22 - For application culture

@NgModule({
    declarations: [
        AppComponent,
        SolicitacaoComponent
    ],
    imports: [
        AppRoutingModule,
        BrowserModule,
        HttpClientModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatSnackBarModule,
        ReactiveFormsModule,
        NgxMaskDirective,
        NgxMaskPipe
    ],
    providers: [{
        provide: LOCALE_ID, useValue: 'pt' // 2025/04/22 - For application culture
    }, {
        provide: DEFAULT_CURRENCY_CODE, useValue: 'BRL' // 2025/04/22 - For application culture
    }, provideNgxMask()],
    bootstrap: [AppComponent]
})
export class AppModule {
}
