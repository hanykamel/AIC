import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { ProjectsRoutingModule } from './projects-routing.module';

import { ProjectsLayoutComponent } from './components/projects-layout/projects-layout.component';
import { ApplicationsListComponent } from './components/applications-domain/applications-list/applications-list.component';
import { ApplicationsDetailsComponent } from './components/applications-domain/applications-details/applications-details.component';
import { ApplicationsDemoComponent } from './components/applications-domain/applications-demo/applications-demo.component';
import { TechnologyListComponent } from './components/technology-domain/technology-list/technology-list.component';
import { TechnologyDetailsComponent } from './components/technology-domain/technology-details/technology-details.component';
import { TechnologyDemoComponent } from './components/technology-domain/technology-demo/technology-demo.component';


@NgModule({
  declarations: [
    ProjectsLayoutComponent,
    ApplicationsListComponent,
    ApplicationsDetailsComponent,
    ApplicationsDemoComponent,
    TechnologyListComponent,
    TechnologyDetailsComponent,
    TechnologyDemoComponent
  ],
  imports: [
    CommonModule,
    ProjectsRoutingModule, SharedModule
  ]
})
export class ProjectsModule { }
