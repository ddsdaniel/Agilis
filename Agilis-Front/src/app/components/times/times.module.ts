import { NgModule } from '@angular/core';
import { GlobalModule } from 'src/app/modules/global.module';
import { TimesFormComponent } from './times-form/times-form.component';
import { TimesListComponent } from './times-list/times-list.component';
import { TimesRoutingModule } from './times-routing.module';


@NgModule({
  declarations: [
    TimesListComponent,
    TimesFormComponent,
  ],
  imports: [
    GlobalModule,
    TimesRoutingModule,
  ]
})
export class TimesModule { }
