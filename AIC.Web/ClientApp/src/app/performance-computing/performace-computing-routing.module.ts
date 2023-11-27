import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PerformanceComputingComponent } from './components/performance-computing/performance-computing.component';
import { HpcProjectDetailsComponent } from './components/hpc-project-details/hpc-project-details.component';

const routes: Routes = [
    {
    path: 'hpc',
    children: [{
      path: '', component: PerformanceComputingComponent, data: {
        breadcrumb: 'PagesTitle.HighPreformanceComputing',
        title: 'PagesTitle.HighPreformanceComputing'
      }
    },
    {
      path: 'hpc-project-details/:id', component: HpcProjectDetailsComponent, data: {
        breadcrumb: 'PagesTitle.HPCProjectDetails',
        title: 'PagesTitle.HPCProjectDetails'
      }
    }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PerformaceComputingRoutingModule { }
