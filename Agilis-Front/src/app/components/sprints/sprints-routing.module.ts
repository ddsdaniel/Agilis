import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutenticacaoGuard } from 'src/app/guards/autenticacao.guard';

import { SprintsFormComponent } from './sprints-form/sprints-form.component';
import { SprintsListComponent } from './sprints-list/sprints-list.component';

const routes: Routes = [
  { path: '', component: SprintsListComponent, canActivate: [AutenticacaoGuard] },
  { path: 'new', component: SprintsFormComponent, canActivate: [AutenticacaoGuard] },
  { path: ':id', component: SprintsFormComponent, canActivate: [AutenticacaoGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SprintsRoutingModule { }
