import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PerformaceComputingRoutingModule } from './performace-computing-routing.module';
import { PerformanceComputingComponent } from './components/performance-computing/performance-computing.component';
import { SharedModule } from '../shared/shared.module';
import { HpcProjectDetailsComponent } from './components/hpc-project-details/hpc-project-details.component';


@NgModule({
  declarations: [
    PerformanceComputingComponent,
    HpcProjectDetailsComponent
  ],
  imports: [
    CommonModule,
    PerformaceComputingRoutingModule,
    SharedModule
  ]
})
export class PerformaceComputingModule { }
