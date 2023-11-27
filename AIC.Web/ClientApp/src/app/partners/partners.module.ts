import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { PartnersRoutingModule } from './partners-routing.module';

import { PartnersComponent } from './components/partners.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    PartnersComponent
  ],
  imports: [
    CommonModule,
    PartnersRoutingModule,
    TranslateModule,
    SharedModule
  ]
})
export class PartnersModule { }
