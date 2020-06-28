import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TimesFormComponent } from './components/pessoas/times/times-form/times-form.component';
import { TimesComponent } from './components/pessoas/times/times/times.component';
import { UsuariosFormComponent } from './components/pessoas/usuarios/usuarios-form/usuarios-form.component';
import { LoginComponent } from './components/seguranca/login/login.component';
import { ProdutosFormComponent } from './components/trabalho/produtos/produtos-form/produtos-form.component';
import { ReleasesFormComponent } from './components/trabalho/releases/releases-form/releases-form.component';
import { SprintsFormComponent } from './components/trabalho/sprints/sprints-form/sprints-form.component';
import { UserStoriesFormComponent } from './components/trabalho/user-stories/user-stories-form/user-stories-form.component';
import { UserStoriesComponent } from './components/trabalho/user-stories/user-stories/user-stories.component';
import { AutenticacaoGuard } from './guards/autenticacao.guard';
import { JornadasFormComponent } from './components/trabalho/produtos/jornadas-form/jornadas-form.component';

const routes: Routes = [

  { path: 'times', component: TimesComponent, canActivate: [AutenticacaoGuard] },
  { path: 'times/:id', component: TimesFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'releases/:timeId/:releaseId', component: ReleasesFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'produtos/:timeId/:produtoId', component: ProdutosFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'jornadas/:produtoId/:jornadaPosicao', component: JornadasFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'sprints/:id', component: SprintsFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'user-stories', component: UserStoriesComponent, canActivate: [AutenticacaoGuard] },
  { path: 'user-stories/new', component: UserStoriesFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'user-stories/:id', component: UserStoriesFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'usuarios/new', component: UsuariosFormComponent },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
