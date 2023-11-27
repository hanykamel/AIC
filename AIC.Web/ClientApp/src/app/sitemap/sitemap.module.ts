import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SitemapComponent } from './components/sitemap/sitemap.component';
import { SitemapRoutingModule } from './sitemap-routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    SitemapComponent
  ],
  imports: [
    CommonModule,
    SitemapRoutingModule, SharedModule
  ]
})
export class SitemapModule { }
