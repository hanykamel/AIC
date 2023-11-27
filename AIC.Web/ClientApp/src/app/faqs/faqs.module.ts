import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FaqsRoutingModule } from './faqs-routing.module';
import { FaqsComponent } from './components/faqs/faqs.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    FaqsComponent
  ],
  imports: [
    CommonModule,
    FaqsRoutingModule,
    SharedModule
  ]
})
export class FaqsModule { }
