import { EnterTheViewportNotifierDirective } from './services/enter-the-view-port.directive';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { HomeRoutingModule } from './home-routing.module';

import { HomeComponent } from './components/home.component';
import { SliderComponent } from './components/slider/slider.component';
import { ProjectsComponent } from './components/projects/projects.component';
import { HighlightsComponent } from './components/highlights/highlights.component';
import { CareersComponent } from './components/careers/careers.component';
import { HomeContactComponent } from './components/home-contact/home-contact.component';

@NgModule({
  declarations: [
    HomeComponent,
    SliderComponent,
    ProjectsComponent,
    HighlightsComponent,
    CareersComponent,
    HomeContactComponent,
    EnterTheViewportNotifierDirective,
  ],
  imports: [CommonModule, HomeRoutingModule, SharedModule],
})
export class HomeModule {}
