import { NgModule } from '@angular/core';
import { GlobalModule } from 'src/app/modules/global.module';
import { ClientesFormComponent } from './clientes-form/clientes-form.component';
import { ClientesListComponent } from './clientes-list/clientes-list.component';
import { ClientesRoutingModule } from './clientes-routing.module';


@NgModule({
  declarations: [
    ClientesListComponent,
    ClientesFormComponent,
  ],
  imports: [
    GlobalModule,
    ClientesRoutingModule,
  ]
})
export class ClientesModule { }
