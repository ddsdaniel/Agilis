import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NavMapComponent } from './components/nav-map/nav-map.component';
import { AtoresFormComponent } from './components/pessoas/atores/atores-form/atores-form.component';
import { AtoresComponent } from './components/pessoas/atores/atores/atores.component';
import { TimesFormComponent } from './components/pessoas/times/times-form/times-form.component';
import { TimesComponent } from './components/pessoas/times/times/times.component';
import { UsuariosFormComponent } from './components/pessoas/usuarios/usuarios-form/usuarios-form.component';
import { LoginComponent } from './components/seguranca/login/login.component';
import { ProdutosFormComponent } from './components/trabalho/produtos/produtos-form/produtos-form.component';
import { ProdutosComponent } from './components/trabalho/produtos/produtos/produtos.component';
import { StoryMappingComponent } from './components/trabalho/story-mapping/story-mapping.component';
import { AutenticacaoGuard } from './guards/autenticacao.guard';
import { UserStoriesFormComponent } from './components/trabalho/user-stories/user-stories-form/user-stories-form.component';

const routes: Routes = [

  { path: 'times', component: TimesComponent, canActivate: [AutenticacaoGuard] },
  { path: 'times/:id', component: TimesFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'story-mapping/:produtoId', component: StoryMappingComponent, canActivate: [AutenticacaoGuard] },
  { path: 'user-stories/:id', component: UserStoriesFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'atores', component: AtoresComponent, canActivate: [AutenticacaoGuard] },
  { path: 'atores/new', component: AtoresFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'atores/:id', component: AtoresFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'produtos', component: ProdutosComponent, canActivate: [AutenticacaoGuard] },
  { path: 'produtos/new', component: ProdutosFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'produtos/:id', component: ProdutosFormComponent, canActivate: [AutenticacaoGuard] },

  { path: 'map', component: NavMapComponent, canActivate: [AutenticacaoGuard] },

  { path: 'usuarios/new', component: UsuariosFormComponent },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
