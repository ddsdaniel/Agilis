import { NgModule } from '@angular/core';
import { GlobalModule } from 'src/app/modules/global.module';
import { SprintsFormComponent } from './sprints-form/sprints-form.component';
import { SprintsListComponent } from './sprints-list/sprints-list.component';
import { SprintsRoutingModule } from './sprints-routing.module';


@NgModule({
  declarations: [
    SprintsListComponent,
    SprintsFormComponent,
  ],
  imports: [
    GlobalModule,
    SprintsRoutingModule,
  ]
})
export class SprintsModule { }
