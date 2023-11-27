import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SitemapComponent } from './components/sitemap/sitemap.component';

const routes: Routes = [
  { path: '', component: SitemapComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SitemapRoutingModule { }
