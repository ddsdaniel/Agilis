import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AlterarMinhaSenhaComponent } from './components/alterar-minha-senha/alterar-minha-senha.component';
import { BemVindoComponent } from './components/bem-vindo/bem-vindo.component';
import { EsqueciMinhaSenhaComponent } from './components/esqueci-senha/esqueci-minha-senha/esqueci-minha-senha.component';
import {
  RedefinirMinhaSenhaComponent,
} from './components/esqueci-senha/redefinir-minha-senha/redefinir-minha-senha.component';
import { ExcluirMinhaContaComponent } from './components/excluir-minha-conta/excluir-minha-conta.component';
import { IntroComponent } from './components/intro/intro.component';
import { LoginComponent } from './components/login/login.component';
import { NovoUsuarioComponent } from './components/usuarios/novo-usuario/novo-usuario.component';
import { AutenticacaoGuard } from './guards/autenticacao.guard';


const routes: Routes = [

  { path: 'bem-vindo', component: BemVindoComponent },
  { path: 'usuarios/new', component: NovoUsuarioComponent },
  { path: 'login', component: LoginComponent },
  { path: 'esqueci-minha-senha', component: EsqueciMinhaSenhaComponent },
  { path: 'redefinir-minha-senha/:email/:chave', component: RedefinirMinhaSenhaComponent },
  { path: 'excluir-minha-conta', component: ExcluirMinhaContaComponent, canActivate: [AutenticacaoGuard] },
  { path: 'intro', component: IntroComponent, canActivate: [AutenticacaoGuard] },
  { path: 'alterar-minha-senha', component: AlterarMinhaSenhaComponent, canActivate: [AutenticacaoGuard] },

  { path: 'times', loadChildren: () => import('./components/times/times.module').then(m => m.TimesModule) },
  { path: 'tarefas', loadChildren: () => import('./components/tarefas/tarefas.module').then(m => m.TarefasModule) },
  { path: 'produtos', loadChildren: () => import('./components/produtos/produtos.module').then(m => m.ProdutosModule) },
  { path: 'sprints', loadChildren: () => import('./components/sprints/sprints.module').then(m => m.SprintsModule) },
  { path: 'clientes', loadChildren: () => import('./components/clientes/clientes.module').then(m => m.ClientesModule) },

  // { path: '', pathMatch: 'full', redirectTo: 'tarefas' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
