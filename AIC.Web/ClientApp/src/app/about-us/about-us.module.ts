import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';


import { AboutUsRoutingModule } from './about-us-routing.module';
import { CoreModule } from '../core/core.module';
import { MissionComponent } from './components/mission/mission.component';
import { RoadmapComponent } from './components/roadmap/roadmap.component';
import { ObjectivesComponent } from './components/objectives/objectives.component';
import { ValuesComponent } from './components/values/values.component';
import { PartnershipModelComponent } from './components/partnership-model/partnership-model.component';
import { OurPeopleListComponent } from './components/our-people/our-people-list/our-people-list.component';
import { OurPeopleDetailsComponent } from './components/our-people/our-people-details/our-people-details.component';
import { CommitteeListComponent } from './components/committee/committee-list/committee-list.component';
import { CommitteeDetailsComponent } from './components/committee/committee-details/committee-details.component';

@NgModule({
  declarations: [
    MissionComponent,
    OurPeopleListComponent,
    OurPeopleDetailsComponent,
    CommitteeListComponent,
    CommitteeDetailsComponent,
    RoadmapComponent,
    ObjectivesComponent,
    ValuesComponent,
    PartnershipModelComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    AboutUsRoutingModule, CoreModule
  ]
})
export class AboutUsModule { }
