import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { UsuarioLogado } from 'src/app/models/pessoas/usuario-logado';
import { LoginDados } from 'src/app/models/seguranca/login-dados';
import { UsuariosApiService } from 'src/app/services/api/pessoas/usuarios-api.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { localStorageKeys } from 'src/assets/constantes/local-storage-keys';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

    login: LoginDados = {
        email: '',
        senha: ''
    };

    constructor(
        private usuariosApiService: UsuariosApiService,
        private snackBar: MatSnackBar,
        private localStorageService: LocalStorageService,
        private router: Router,
    ) {

    }

    ngOnInit() {
        this.usuariosApiService.usuarioLogado = null;
        this.localStorageService.clear();
    }

    autenticar() {
        this.usuariosApiService.autenticar(this.login)
            .subscribe(
                (logado: UsuarioLogado) => this.tratarSucessoLogin(logado),
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
            );
    }

    tratarSucessoLogin(logado: UsuarioLogado): void {

        this.usuariosApiService.usuarioLogado = logado;
        this.localStorageService.setJSON<UsuarioLogado>(localStorageKeys.usuarioLogado, logado);

        this.router.navigate(['times']);
    }

}
