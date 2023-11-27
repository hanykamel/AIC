import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreRoutingModule } from './core-routing.module';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { LayoutComponent } from '../layout/layout/layout.component';

import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [

    LayoutComponent
  ],
  imports: [
    CommonModule,
    CoreRoutingModule,
    SharedModule, BreadcrumbModule
  
  ],
  exports: [LayoutComponent, BreadcrumbModule
  ],
})
export class CoreModule { }
