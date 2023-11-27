import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JoinFormComponent } from './components/join-us/join-form/join-form.component';
import { CareersListComponent } from './components/careers/careers-list/careers-list.component';
import { InternshipsListComponent } from './components/internships/internships-list/internships-list.component';
import { JoinHomeComponent } from './components/join-us/join-home/join-home.component';
import { CareersDetailsComponent } from './components/careers/careers-details/careers-details.component';
import { InternshipsDetailsComponent } from './components/internships/internships-details/internships-details.component';
import { WorkingAicComponent } from './components/working-aic/working-aic.component';
import { ApplyCareersComponent } from './components/careers/apply-careers/apply-careers.component';
import { ApplyInternshipsComponent } from './components/internships/apply-internships/apply-internships.component';

const routes: Routes = [
  {
    path: 'join-us', component: JoinFormComponent, data: {
      breadcrumb: { skip: true },
      title: 'JoinUs.ApplyNow'
    }
  },
  {
    path: 'join-us/:email/:date', component: JoinFormComponent, data: {
      breadcrumb: { skip: true },
      title: 'JoinUs.ApplyNow'
    }
  },
  {
    path: 'careers-list',
    children: [
      {
        path: '', component: CareersListComponent, data: {
          breadcrumb: {
            alias: 'CareersTitle'
          },
          title: 'PagesTitle.Careers'
        }
      },
      {
        path: 'careers-details/:id',
        component: CareersDetailsComponent,
        data: {
          breadcrumb: {
            alias: 'careerTitle'
          },
          title: 'PagesTitle.CareersDetails'
        }
      }
    ]
  },
  {
    path: 'internships-list',
    children: [
      {
        path: '', component: InternshipsListComponent, data: {
          breadcrumb: {
            alias:'InternshipsTitle'
          },
          title: 'PagesTitle.Internships'
        }
      },
      {
        path: 'internships-details/:id',
        component: InternshipsDetailsComponent,
        data: {
          breadcrumb: {
            alias: 'internTitle'
          },
          title: 'PagesTitle.InternshipsDetails'
        }
      }
    ]
  },
  {
    path: 'apply', component: JoinHomeComponent
    , data: {
      breadcrumb: { skip: true },
      title: 'JoinUs.ApplyNow'
    }
  },
  {
    path: 'apply-careers', component: ApplyCareersComponent,
    data: {
      title: 'Careers.Apply'
    }
  },
  {
    path: 'apply-careers/:email/:date/:vacancyId', component: ApplyCareersComponent, data: {
      breadcrumb: { skip: true },
      title: 'Careers.Apply'
    }
  },
  {
    path: 'apply-internship', component: ApplyInternshipsComponent,
    data: {
      title: 'Intern.Apply'
    }
  },
  {
    path: 'working-with-aic', component: WorkingAicComponent
    , data: {
      breadcrumb: { skip: true },
      title: "AboutUs.Working"
    }
  },
  {
    path: 'apply-internships/:email/:date/:internshipId', component: ApplyInternshipsComponent, data: {
      breadcrumb: { skip: true },
      title: 'Intern.Apply'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CareersRoutingModule { }
