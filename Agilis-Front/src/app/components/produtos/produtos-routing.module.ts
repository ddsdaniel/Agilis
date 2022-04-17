﻿import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutenticacaoGuard } from 'src/app/guards/autenticacao.guard';
import { EpicosFormComponent } from './epicos-form/epicos-form.component';
import { ProductBacklogComponent } from './product-backlog/product-backlog.component';

import { ProdutosFormComponent } from './produtos-form/produtos-form.component';
import { ProdutosListComponent } from './produtos-list/produtos-list.component';

const routes: Routes = [
  { path: '', component: ProdutosListComponent, canActivate: [AutenticacaoGuard] },
  { path: 'new', component: ProdutosFormComponent, canActivate: [AutenticacaoGuard] },
  { path: ':id', component: ProdutosFormComponent, canActivate: [AutenticacaoGuard] },

  { path: ':id/backlog', component: ProductBacklogComponent, canActivate: [AutenticacaoGuard] },

  { path: 'epico/:id', component: EpicosFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'epicos/new', component: EpicosFormComponent, canActivate: [AutenticacaoGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProdutosRoutingModule { }
