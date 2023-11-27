import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicationsDemoComponent } from './components/applications-domain/applications-demo/applications-demo.component';
import { ApplicationsDetailsComponent } from './components/applications-domain/applications-details/applications-details.component';
import { ProjectsLayoutComponent } from './components/projects-layout/projects-layout.component';
import { RelatedNewsComponent } from '../shared/related-news/related-news.component';

const routes: Routes = [
  {
    path: 'list/:type',
    children: [{
      path: '', component: ProjectsLayoutComponent, data: {
        breadcrumb: 'PagesTitle.Projects',
        title: 'PagesTitle.Projects'
      }
    },
    {
      path: 'details/:id',
      children: [{
        path: '', component: ApplicationsDetailsComponent, data: {
          breadcrumb: {
            alias:'projectTitle'
          },
          title: 'PagesTitle.ProjectDetails'
        }
      },
      {
        path: 'demo', component: ApplicationsDemoComponent, data: {
          breadcrumb: 'PagesTitle.ProjectDemo',
          title: 'PagesTitle.ProjectDemo'
        }
        },
        {
          path: 'related-news/:ids', component: RelatedNewsComponent, data: {
            breadcrumb: 'PagesTitle.RelatedNews',
            title: 'PagesTitle.RelatedNews'
          }
        }
      ]
    }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectsRoutingModule { }
