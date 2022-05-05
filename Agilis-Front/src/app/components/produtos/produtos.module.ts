import { NgModule } from '@angular/core';
import { GlobalModule } from 'src/app/modules/global.module';
import { ProdutosFormComponent } from './produtos-form/produtos-form.component';
import { ProdutosListComponent } from './produtos-list/produtos-list.component';
import { ProdutosRoutingModule } from './produtos-routing.module';
import { FeaturesFormComponent } from './features-form/features-form.component';
import { FeaturesListComponent } from './features-list/features-list.component';


@NgModule({
  declarations: [
    ProdutosListComponent,
    ProdutosFormComponent,
    FeaturesFormComponent
    ,
    FeaturesListComponent
  ],
  imports: [
    GlobalModule,
    ProdutosRoutingModule,
  ]
})
export class ProdutosModule { }
