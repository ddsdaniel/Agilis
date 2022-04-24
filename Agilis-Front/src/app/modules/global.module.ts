import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule } from '@angular/forms';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { ActionBarComponent } from '../components/widgets/action-bar/action-bar.component';
import { ColorPickerComponent } from '../components/widgets/color-picker/color-picker.component';
import { DialogoSimNaoComponent } from '../components/widgets/dialogo-sim-nao/dialogo-sim-nao.component';
import { MonthNavigationComponent } from '../components/widgets/month-navigation/month-navigation.component';
import { ProgressBarComponent } from '../components/widgets/progress-bar/progress-bar.component';
import { SpanEditavelComponent } from '../components/widgets/span-editavel/span-editavel.component';
import { YearNavigationComponent } from '../components/widgets/year-navigation/year-navigation.component';
import { AngularMaterialModule } from './angular-material.module';


export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;

@NgModule({
  declarations: [
    ActionBarComponent,
    ColorPickerComponent,
    MonthNavigationComponent,
    YearNavigationComponent,
    ProgressBarComponent,
    DialogoSimNaoComponent,
    SpanEditavelComponent,
  ],
  imports: [
    FormsModule,
    AngularMaterialModule,
    FlexLayoutModule,
    InfiniteScrollModule,
    NgxChartsModule,
    NgxMaskModule.forRoot(options),
  ],
  exports: [
    FormsModule,
    AngularMaterialModule,
    FlexLayoutModule,
    InfiniteScrollModule,
    NgxChartsModule,
    NgxMaskModule,
    ActionBarComponent,
    ColorPickerComponent,
    MonthNavigationComponent,
    YearNavigationComponent,
    ProgressBarComponent,
    DialogoSimNaoComponent,
    SpanEditavelComponent,
  ]
})
export class GlobalModule { }
