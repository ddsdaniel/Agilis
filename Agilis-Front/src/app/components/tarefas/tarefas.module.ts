import { NgModule } from '@angular/core';
import { GlobalModule } from 'src/app/modules/global.module';
import { TarefasFormComponent } from './tarefas-form/tarefas-form.component';
import { TarefasListComponent } from './tarefas-list/tarefas-list.component';
import { TarefasRoutingModule } from './tarefas-routing.module';


@NgModule({
  declarations: [
    TarefasListComponent,
    TarefasFormComponent,
  ],
  imports: [
    GlobalModule,
    TarefasRoutingModule,
  ]
})
export class TarefasModule { }
