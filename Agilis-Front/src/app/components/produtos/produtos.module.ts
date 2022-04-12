import { NgModule } from '@angular/core';
import { GlobalModule } from 'src/app/modules/global.module';
import { ProdutosFormComponent } from './produtos-form/produtos-form.component';
import { ProdutosListComponent } from './produtos-list/produtos-list.component';
import { ProdutosRoutingModule } from './produtos-routing.module';


@NgModule({
  declarations: [
    ProdutosListComponent,
    ProdutosFormComponent,
  ],
  imports: [
    GlobalModule,
    ProdutosRoutingModule,
  ]
})
export class ProdutosModule { }
