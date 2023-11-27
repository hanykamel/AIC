import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommitteeDetailsComponent } from './components/committee/committee-details/committee-details.component';
import { CommitteeListComponent } from './components/committee/committee-list/committee-list.component';
import { MissionComponent } from './components/mission/mission.component';
import { ObjectivesComponent } from './components/objectives/objectives.component';
import { OurPeopleDetailsComponent } from './components/our-people/our-people-details/our-people-details.component';
import { OurPeopleListComponent } from './components/our-people/our-people-list/our-people-list.component';
import { PartnershipModelComponent } from './components/partnership-model/partnership-model.component';
import { RoadmapComponent } from './components/roadmap/roadmap.component';
import { ValuesComponent } from './components/values/values.component';
import { skip } from 'rxjs/operators';

const routes: Routes = [
  {
    path: 'mission', component: MissionComponent, data: {
      breadcrumb: { skip: true },
      title: 'PagesTitle.AICMission'
    }
  },
  {
    path: 'objectives', component: ObjectivesComponent, data: {
      breadcrumb: { skip: true },
      title: 'PagesTitle.AICObjectives'
    }
  },
  {
    path: 'values', component: ValuesComponent, data: {
      breadcrumb: { skip: true },
      title: 'PagesTitle.AICValues'
    }
  },
  {
    path: 'partnership', component: PartnershipModelComponent, data: {
      breadcrumb: { skip: true },
      title: 'PagesTitle.AICPartnership'
    }
  },
  {
    path: 'roadmap', component: RoadmapComponent, data: {
      breadcrumb: { skip: true },
      title: 'PagesTitle.StrategicRoadmap'
    }
  },
  {
    path: 'our-people-list',
    children: [{
      path: '', component: OurPeopleListComponent, data: {
        breadcrumb: {
          alias:'OurPeopleTitle'
        },
        title: 'PagesTitle.OurPeople'
      }
    },
    {
      path: 'our-people-details/:id', component: OurPeopleDetailsComponent, data: {
        breadcrumb: {
          alias: 'personName'
        },
        title: 'PagesTitle.OurPeopleDetails'
      }
    }
    ]
  },
  {
    path: 'committee-list',
    children: [{
      path: '', component: CommitteeListComponent, data: {
        breadcrumb: {
          alias:'CommitteeListTitle'
        },
        title: 'PagesTitle.CommitteeList'
      }
    },
    {
      path: 'committee-details/:id', component: CommitteeDetailsComponent, data: {
        breadcrumb: {
          alias: 'committeeDetails'
        },
        title: 'PagesTitle.CommitteeDetais'
      }
    }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AboutUsRoutingModule { }
