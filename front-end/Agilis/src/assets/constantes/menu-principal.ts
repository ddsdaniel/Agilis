import { GrupoMenu } from 'src/app/models/menu/grupo-menu';

export const menuPrincipal: GrupoMenu[] = [
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
    nome: 'Pessoas',
    itens: [

      {
        nome: 'Times',
        icone: 'vpn_key',
        rota: '/times'
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
        nome: 'Releases',
        icone: 'vpn_key',
        rota: '/releases'
      },
      {
        nome: 'Produtos',
        icone: 'vpn_key',
        rota: '/produtos'
      }
    ]
  }
];
