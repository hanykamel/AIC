import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdvancedSearchComponent } from './advanced-search/advanced-search/advanced-search.component';
import { UnsubscribeComponent } from './newsletter/components/unsubscribe/unsubscribe.component';
import { RedirectSwitcherComponent } from './shared/redirect-switcher/redirect-switcher.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then((m) => m.HomeModule),
    data: {
      breadcrumb: {
        label: 'Home',
        info: 'home',
        routeInterceptor: (routeLink) => {
          return routeLink;
        },
      },
      title: 'PagesTitle.AIC',
    },
  },
  {
    path: 'about-us',
    loadChildren: () =>
      import('./about-us/about-us.module').then((m) => m.AboutUsModule),
    data: {
      breadcrumb: 'about-us',
      title: 'PagesTitle.AboutUs',
    },
  },
  {
    path: 'partners',
    loadChildren: () =>
      import('./partners/partners.module').then((m) => m.PartnersModule),
    data: {
      title: 'PagesTitle.Partners',
    },
  },
  {
    path: 'projects',
    loadChildren: () =>
      import('./projects/projects.module').then((m) => m.ProjectsModule),
    data: {
      title: 'PagesTitle.Projects',
    },
  },
  {
    path: 'resources',
    loadChildren: () =>
      import('./resources/resources.module').then((m) => m.ResourcesModule),
    data: {
      title: 'PagesTitle.Resources',
    },
  },
  {
    path: 'media-center',
    loadChildren: () =>
      import('./media-center/media-center.module').then(
        (m) => m.MediaCenterModule
      ),
    data: {
      title: 'PagesTitle.MediaCenter',
    },
  },
  {
    path: 'site-map',
    loadChildren: () =>
      import('./sitemap/sitemap-routing.module').then(
        (m) => m.SitemapRoutingModule
      ),
    data: {
      breadcrumb: 'PagesTitle.Sitemap',
      title: 'PagesTitle.Sitemap',
    },
  },
  {
    path: 'site-map',
    loadChildren: () =>
      import('./sitemap/sitemap.module').then((m) => m.SitemapModule),
    data: {
      breadcrumb: 'PagesTitle.Sitemap',
      title: 'PagesTitle.Sitemap',
    },
  },
  {
    path: 'performance-computing',
    loadChildren: () =>
      import('./performance-computing/performace-computing.module').then(
        (m) => m.PerformaceComputingModule
      ),
    data: {
      breadcrumb: 'PagesTitle.HighPreformaneComputing',
      title: 'PagesTitle.HighPreformaneComputing',
    },
  },
  {
    path: 'faqs',
    loadChildren: () => import('./faqs/faqs.module').then((m) => m.FaqsModule),
    data: {
      breadcrumb: 'PagesTitle.Faqs',
      title: 'PagesTitle.Faqs',
    },
  },
  {
    path: 'careers-Opportunities',
    loadChildren: () =>
      import('./careers/careers.module').then((m) => m.CareersModule),
    data: {
      breadcrumb: 'PagesTitle.Careers',
      title: 'PagesTitle.Careers',
    },
  },
  {
    path: 'contact-us',
    loadChildren: () =>
      import('./contact-us/contact-us.module').then((m) => m.ContactUsModule),
    data: {
      breadcrumb: '',
      title: 'PagesTitle.ContactUs',
    },
  },
  {
    path: 'advanced-search',
    component: AdvancedSearchComponent,
    data: {
      title: 'PagesTitle.AdvancedSearch',
    },
  },
  {
    path: 'unsubscribe/:email',
    component: UnsubscribeComponent,
    data: {
      title: 'PagesTitle.Unsubscribe',
    },
  },
  { path: 'home/redirect', component: RedirectSwitcherComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled',anchorScrolling: 'enabled' }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
