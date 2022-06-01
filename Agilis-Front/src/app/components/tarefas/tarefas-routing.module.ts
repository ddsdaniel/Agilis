import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutenticacaoGuard } from 'src/app/guards/autenticacao.guard';
import { ImportacaoTrelloComponent } from './importacao-trello/importacao-trello.component';

import { TarefasFormComponent } from './tarefas-form/tarefas-form.component';
import { TarefasListComponent } from './tarefas-list/tarefas-list.component';

const routes: Routes = [
  { path: '', component: TarefasListComponent, canActivate: [AutenticacaoGuard] },
  { path: 'new', component: TarefasFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'trello', component: ImportacaoTrelloComponent, canActivate: [AutenticacaoGuard] },
  { path: ':id', component: TarefasFormComponent, canActivate: [AutenticacaoGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TarefasRoutingModule { }
