import { Component } from '@angular/core';
import { menuPrincipal } from 'src/assets/constantes/menu-principal';

import { GrupoMenu } from './models/menu/grupo-menu';
import { UsuarioApiService } from './services/api/pessoas/usuario-api.service';
import { ProcessandoService } from './services/processando.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {

    menu: GrupoMenu[] = menuPrincipal;

    constructor(
        public usuarioApiService: UsuarioApiService,
        public processandoService: ProcessandoService,
    ) {

    }

}
