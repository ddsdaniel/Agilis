import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutenticacaoGuard } from 'src/app/guards/autenticacao.guard';

import { TimesFormComponent } from './times-form/times-form.component';
import { TimesListComponent } from './times-list/times-list.component';

const routes: Routes = [
  { path: '', component: TimesListComponent, canActivate: [AutenticacaoGuard] },
  { path: 'new', component: TimesFormComponent, canActivate: [AutenticacaoGuard] },
  { path: ':id', component: TimesFormComponent, canActivate: [AutenticacaoGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TimesRoutingModule { }
