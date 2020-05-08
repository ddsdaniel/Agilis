import { Component } from '@angular/core';
import { GrupoMenu } from './models/menu/grupo-menu';
import { UsuarioApiService } from './services/api/pessoas/usuario-api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  menu: GrupoMenu[] = [
    {
      nome: 'Segurança',
      itens: [
        {
          nome: 'Login',
          icone: 'vpn_key',
          rota: '/login'
        }
      ]
    },
    {
      nome: 'Trabalho',
      itens: [
        {
          nome: 'User Stories',
          icone: 'vpn_key',
          rota: '/user-stories'
        },
        {
          nome: 'User Stories',
          icone: 'vpn_key',
          rota: '/user-stories'
        },
        {
          nome: 'User Stories',
          icone: 'vpn_key',
          rota: '/user-stories'
        }
      ]
    }
  ];

  constructor(
    public usuarioApiService: UsuarioApiService
  ) {

  }

}
