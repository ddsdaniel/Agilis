import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutenticacaoGuard } from 'src/app/guards/autenticacao.guard';

import { TarefasFormComponent } from './tarefas-form/tarefas-form.component';

const routes: Routes = [
  { path: 'new', component: TarefasFormComponent, canActivate: [AutenticacaoGuard] },
  { path: ':id', component: TarefasFormComponent, canActivate: [AutenticacaoGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TarefasRoutingModule { }
