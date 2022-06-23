import { NgModule } from '@angular/core';
import { GlobalModule } from 'src/app/modules/global.module';
import { TarefasFormComponent } from './tarefas-form/tarefas-form.component';
import { TarefasRoutingModule } from './tarefas-routing.module';
import { CheckListEditavelComponent } from './check-list-editavel/check-list-editavel.component';
import { TagsComponent } from './tags/tags.component';
import { TarefasListComponent } from './tarefas-list/tarefas-list.component';
import { ComentariosComponent } from './comentarios/comentarios.component';
import { AnexosComponent } from './anexos/anexos.component';
import { ImportacaoTrelloComponent } from './importacao-trello/importacao-trello.component';


@NgModule({
  declarations: [
    TarefasFormComponent,
    CheckListEditavelComponent,
    TagsComponent,
    TarefasListComponent,
    ComentariosComponent,
    AnexosComponent,
    ImportacaoTrelloComponent,
  ],
  imports: [
    GlobalModule,
    TarefasRoutingModule,
  ]
})
export class TarefasModule { }
