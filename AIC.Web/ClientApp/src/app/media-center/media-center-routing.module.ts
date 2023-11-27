import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArchivedEventsListComponent } from './components/events/archived-events/archived-events-list/archived-events-list.component';
import { EventsDetailsComponent } from './components/events/events-details/events-details.component';
import { EventsListComponent } from './components/events/events-list/events-list.component';
import { ArchivedNewsListComponent } from './components/news/archived-news/archived-news-list/archived-news-list.component';
import { NewsDetailsComponent } from './components/news/news-details/news-details.component';
import { NewsListComponent } from './components/news/news-list/news-list.component';
import { PhotosDetailsComponent } from './components/photos/photos-details/photos-details.component';
import { PhotosListComponent } from './components/photos/photos-list/photos-list.component';
import { SocialMediaComponent } from './components/social-media/social-media.component';
import { VideosDetailsComponent } from './components/videos/videos-details/videos-details.component';
import { VideosListComponent } from './components/videos/videos-list/videos-list.component';
import { RelatedNewsComponent } from '../shared/related-news/related-news.component';

const routes: Routes = [
  {
    path: 'news',
    children: [{
      path: '', component: NewsListComponent, data: {
        breadcrumb: {
          alias:'NewsTitle'
        },
        title: 'PagesTitle.News'
      }
    },
    {
      path: 'news-details/:id',
      children: [{
        path: '', component: NewsDetailsComponent, data: {
          breadcrumb: {
            alias: 'title'
          },
          title: 'PagesTitle.NewsDetails'
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
  },

  {
    path: 'archived/news', component: ArchivedNewsListComponent, data: {
      breadcrumb: {
        alias:'NewsTitle'
      },
      title: 'PagesTitle.News'
    }
  },
  {
    path: 'events',
    children: [{
      path: '', component: EventsListComponent, data: {
        breadcrumb: {
          alias:'EventsTitle'
        },
        title: 'PagesTitle.Events'
      }
    },
    {
      path: 'events-details/:id', component: EventsDetailsComponent, data: {
        breadcrumb: {
          alias: 'title'
        },
        title: 'PagesTitle.EventsDetails'
      }
    }
    ]
  },
  {
    path: 'archived/events', component: ArchivedEventsListComponent, data: {
      breadcrumb: {
          alias:'EventsTitle'
        },
      title: 'PagesTitle.Events'
    }
  },
  {
    path: 'social-media', component: SocialMediaComponent, data: {
      breadcrumb: { skip: true },
      title: 'PagesTitle.SocialMedia'
    }
  },
  {
    path: 'photos',
    children: [{
      path: '', component: PhotosListComponent, data: {
        breadcrumb: {
          alias:'PhotosTitle'
        },
        title: 'PagesTitle.PhotoGallery'
      }
    },
    {
      path: 'photo-details/:albumName', component: PhotosDetailsComponent, data: {
        breadcrumb: {
          alias: 'albumName'
        },
        title: 'PagesTitle.PhotoAlbum'
      }
    }
    ]
  },
  {
    path: 'videos',
    children: [{
      path: '', component: VideosListComponent, data: {
        breadcrumb: {
          alias:'VideosTitle'
        },
        title: 'PagesTitle.VideoGallery'
      }
    },
    {
      path: 'video-details/:albumName', component: VideosDetailsComponent, data: {
        breadcrumb: {
          alias: 'albumName'
        },
        title: 'PagesTitle.VideoAlbum'
      }
    }
    ]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MediaCenterRoutingModule { }
