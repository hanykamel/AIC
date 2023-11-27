import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FaqsComponent } from './components/faqs/faqs.component';

const routes: Routes = [
  {
    path: '', component: FaqsComponent, data: {
      title: 'PagesTitle.Faqs'
    } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FaqsRoutingModule { }
