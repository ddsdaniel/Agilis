import { NgModule } from '@angular/core';
import { GlobalModule } from 'src/app/modules/global.module';
import { ProdutosFormComponent } from './produtos-form/produtos-form.component';
import { ProdutosListComponent } from './produtos-list/produtos-list.component';
import { ProdutosRoutingModule } from './produtos-routing.module';
import { ProductBacklogComponent } from './product-backlog/product-backlog.component';


@NgModule({
  declarations: [
    ProdutosListComponent,
    ProdutosFormComponent,
    ProductBacklogComponent,
  ],
  imports: [
    GlobalModule,
    ProdutosRoutingModule,
  ]
})
export class ProdutosModule { }
