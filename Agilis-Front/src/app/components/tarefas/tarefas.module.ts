import { NgModule } from '@angular/core';
import { GlobalModule } from 'src/app/modules/global.module';
import { TarefasFormComponent } from './tarefas-form/tarefas-form.component';
import { TarefasRoutingModule } from './tarefas-routing.module';
import { CheckListEditavelComponent } from './check-list-editavel/check-list-editavel.component';
import { TagsComponent } from './tags/tags.component';


@NgModule({
  declarations: [
    TarefasFormComponent,
    CheckListEditavelComponent,
    TagsComponent,
  ],
  imports: [
    GlobalModule,
    TarefasRoutingModule,
  ]
})
export class TarefasModule { }
