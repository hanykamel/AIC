import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AicMaterialsComponent } from './components/aic-materials/aic-materials.component';
import { WhitePapersDetailsComponent } from './components/white-papers/white-papers-details/white-papers-details.component';
import { WhitePapersListComponent } from './components/white-papers/white-papers-list/white-papers-list.component';
import { BestPracticeDetailsComponent } from './components/best-practice/best-practice-details/best-practice-details.component';
import { BestPracticeListComponent } from './components/best-practice/best-practice-list/best-practice-list.component';

const routes: Routes = [
  {
    path: 'aic-materials',
    component: AicMaterialsComponent,
    data: {
      breadcrumb: { alias:'AICMaterialsTitle' },
      title: 'PagesTitle.AICMaterials'
    }
  },
  {
    path: 'white-papers',
    children: [{
      path: '', component: WhitePapersListComponent, data: {
        breadcrumb: {
          alias:'WhitePapersTitle'
        },
        title: 'PagesTitle.WhitePapers'
      }
    },
    {
      path: 'white-papers-details/:id', component: WhitePapersDetailsComponent, data: {
        breadcrumb: {
          alias: 'title'
        },
        title: 'PagesTitle.WhitePapersDetails'
      }
    }
    ]
  },
  {
    path: 'best-practice',
    children: [{
      path: '', component: BestPracticeListComponent, data: {
        breadcrumb: {alias:'BestPracticesTitle'},
        title: 'PagesTitle.BestPractices'
      }
    },
    {
      path: 'best-practice-details/:id', component: BestPracticeDetailsComponent, data: {
        breadcrumb: {
          alias: 'title'
        },
        title: 'PagesTitle.BestPracticesDetails'
      }
    }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ResourcesRoutingModule { }
