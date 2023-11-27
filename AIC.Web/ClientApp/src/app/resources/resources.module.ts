import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { ResourcesRoutingModule } from './resources-routing.module';
import { WhitePapersListComponent } from './components/white-papers/white-papers-list/white-papers-list.component';
import { WhitePapersDetailsComponent } from './components/white-papers/white-papers-details/white-papers-details.component';
import { BestPracticeListComponent } from './components/best-practice/best-practice-list/best-practice-list.component';
import { BestPracticeDetailsComponent } from './components/best-practice/best-practice-details/best-practice-details.component';
import { AicMaterialsComponent } from './components/aic-materials/aic-materials.component';



@NgModule({
  declarations: [
    WhitePapersListComponent,
    WhitePapersDetailsComponent,
    BestPracticeListComponent,
    BestPracticeDetailsComponent,
    AicMaterialsComponent
  ],
  imports: [
    CommonModule,
    ResourcesRoutingModule, SharedModule
  ]
})
export class ResourcesModule { }
