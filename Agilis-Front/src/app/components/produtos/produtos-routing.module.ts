import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutenticacaoGuard } from 'src/app/guards/autenticacao.guard';
import { FeaturesFormComponent } from './features-form/features-form.component';
import { FeaturesListComponent } from './features-list/features-list.component';

import { ProdutosFormComponent } from './produtos-form/produtos-form.component';
import { ProdutosListComponent } from './produtos-list/produtos-list.component';

const routes: Routes = [

  { path: 'features', component: FeaturesListComponent, canActivate: [AutenticacaoGuard] },
  { path: 'features/new', component: FeaturesFormComponent, canActivate: [AutenticacaoGuard] },
  { path: 'features/:id', component: FeaturesFormComponent, canActivate: [AutenticacaoGuard] },

  { path: '', component: ProdutosListComponent, canActivate: [AutenticacaoGuard] },
  { path: 'new', component: ProdutosFormComponent, canActivate: [AutenticacaoGuard] },
  { path: ':id', component: ProdutosFormComponent, canActivate: [AutenticacaoGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProdutosRoutingModule { }
