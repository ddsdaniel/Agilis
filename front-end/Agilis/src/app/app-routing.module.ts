import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './components/seguranca/login/login.component';
import { UserStoriesFormComponent } from './components/trabalho/user-stories/user-stories-form/user-stories-form.component';
import { UserStoriesComponent } from './components/trabalho/user-stories/user-stories/user-stories.component';
import { ProdutosFormComponent } from './components/trabalho/produtos/produtos-form/produtos-form.component';
import { ProdutosComponent } from './components/trabalho/produtos/produtos/produtos.component';
import { TimesFormComponent } from './components/trabalho/times/times-form/times-form.component';
import { TimesComponent } from './components/trabalho/times/times/times.component';

const routes: Routes = [
  { path: 'produtos', component: ProdutosComponent },
  { path: 'produtos/new', component: ProdutosFormComponent },
  { path: 'produtos/:produtoId', component: ProdutosFormComponent },

  { path: 'times', component: TimesComponent },
  { path: 'times/new', component: TimesFormComponent },
  { path: 'times/:timeId', component: TimesFormComponent },

  { path: 'user-stories', component: UserStoriesComponent },
  { path: 'user-stories/new', component: UserStoriesFormComponent },
  { path: 'user-stories/:userStoryId', component: UserStoriesFormComponent },

  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
