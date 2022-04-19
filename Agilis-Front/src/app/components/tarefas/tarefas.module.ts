import { NgModule } from '@angular/core';
import { GlobalModule } from 'src/app/modules/global.module';
import { TarefasFormComponent } from './tarefas-form/tarefas-form.component';
import { TarefasRoutingModule } from './tarefas-routing.module';


@NgModule({
  declarations: [
    TarefasFormComponent,
  ],
  imports: [
    GlobalModule,
    TarefasRoutingModule,
  ]
})
export class TarefasModule { }
