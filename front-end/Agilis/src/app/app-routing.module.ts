import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProdutosFormComponent } from './components/trabalho/produtos/produtos-form/produtos-form.component';
import { ProdutosComponent } from './components/trabalho/produtos/produtos/produtos.component';
import { TemasFormComponent } from './components/trabalho/temas/temas-form/temas-form.component';
import { TemasComponent } from './components/trabalho/temas/temas/temas.component';
import { AtoresFormComponent } from './components/pessoas/atores/atores-form/atores-form.component';
import { AtoresComponent } from './components/pessoas/atores/atores/atores.component';
import { EpicosFormComponent } from './components/trabalho/epicos/epicos-form/epicos-form.component';
import { EpicosComponent } from './components/trabalho/epicos/epicos/epicos.component';
import { TimesFormComponent } from './components/pessoas/times/times-form/times-form.component';
import { TimesComponent } from './components/pessoas/times/times/times.component';
import { UsuariosFormComponent } from './components/pessoas/usuarios/usuarios-form/usuarios-form.component';
import { LoginComponent } from './components/seguranca/login/login.component';
import { ReleasesFormComponent } from './components/trabalho/releases/releases-form/releases-form.component';
import { SprintsFormComponent } from './components/trabalho/sprints/sprints-form/sprints-form.component';
import { UserStoriesFormComponent } from './components/trabalho/user-stories/user-stories-form/user-stories-form.component';
import { UserStoriesComponent } from './components/trabalho/user-stories/user-stories/user-stories.component';
import { AutenticacaoGuard } from './guards/autenticacao.guard';
import { NavMapComponent } from './components/nav-map/nav-map.component';

const routes: Routes = [

  { path: 'times', component: TimesComponent, canActivate: [AutenticacaoGuard] },
  { path: 'times/:id', component: TimesFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'epicos', component: EpicosComponent, canActivate: [AutenticacaoGuard] },
  { path: 'epicos/new', component: EpicosFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'epicos/:id', component: EpicosFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'atores', component: AtoresComponent, canActivate: [AutenticacaoGuard] },
  { path: 'atores/new', component: AtoresFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'atores/:id', component: AtoresFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'temas', component: TemasComponent, canActivate: [AutenticacaoGuard] },
  { path: 'temas/new', component: TemasFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'temas/:id', component: TemasFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'produtos', component: ProdutosComponent, canActivate: [AutenticacaoGuard] },
  { path: 'produtos/new', component: ProdutosFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'produtos/:id', component: ProdutosFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'releases/:timeId/:releaseId', component: ReleasesFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'sprints/:id', component: SprintsFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'user-stories', component: UserStoriesComponent, canActivate: [AutenticacaoGuard] },
  { path: 'user-stories/new', component: UserStoriesFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'user-stories/:id', component: UserStoriesFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'map', component: NavMapComponent, canActivate: [AutenticacaoGuard] },

  { path: 'usuarios/new', component: UsuariosFormComponent },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
