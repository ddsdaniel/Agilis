import { NgModule } from '@angular/core';
import { GlobalModule } from 'src/app/modules/global.module';
import { ReleasesFormComponent } from './releases-form/releases-form.component';
import { ReleasesListComponent } from './releases-list/releases-list.component';
import { ReleasesRoutingModule } from './releases-routing.module';


@NgModule({
  declarations: [
    ReleasesListComponent,
    ReleasesFormComponent,
  ],
  imports: [
    GlobalModule,
    ReleasesRoutingModule,
  ]
})
export class ReleasesModule { }
