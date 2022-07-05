import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutenticacaoGuard } from 'src/app/guards/autenticacao.guard';

import { ReleasesFormComponent } from './releases-form/releases-form.component';
import { ReleasesListComponent } from './releases-list/releases-list.component';

const routes: Routes = [
  { path: '', component: ReleasesListComponent, canActivate: [AutenticacaoGuard] },
  { path: 'new', component: ReleasesFormComponent, canActivate: [AutenticacaoGuard] },
  { path: ':id', component: ReleasesFormComponent, canActivate: [AutenticacaoGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReleasesRoutingModule { }
