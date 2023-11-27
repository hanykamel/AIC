import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { CareersRoutingModule } from './careers-routing.module';
import { WorkingAicComponent } from './components/working-aic/working-aic.component';
import { CareersListComponent } from './components/careers/careers-list/careers-list.component';
import { CareersDetailsComponent } from './components/careers/careers-details/careers-details.component';
import { InternshipsListComponent } from './components/internships/internships-list/internships-list.component';
import { InternshipsDetailsComponent } from './components/internships/internships-details/internships-details.component';
import { ApplyCareersComponent } from './components/careers/apply-careers/apply-careers.component';
import { ApplyInternshipsComponent } from './components/internships/apply-internships/apply-internships.component';
import { JoinHomeComponent } from './components/join-us/join-home/join-home.component';
import { JoinFormComponent } from './components/join-us/join-form/join-form.component';
import { SharedModule } from '../shared/shared.module';
import { CaptchaModule } from 'primeng/captcha';


@NgModule({
  declarations: [
    WorkingAicComponent,
    CareersListComponent,
    CareersDetailsComponent,
    InternshipsListComponent,
    InternshipsDetailsComponent,
    ApplyCareersComponent,
    ApplyInternshipsComponent,
    JoinHomeComponent,
    JoinFormComponent
  ],
  imports: [
    CommonModule,
    CareersRoutingModule,
    CaptchaModule,
    SharedModule
  ],
  providers:[DatePipe]
})
export class CareersModule { }
