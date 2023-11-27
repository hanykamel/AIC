import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { MediaCenterRoutingModule } from './media-center-routing.module';

import { EventsListComponent } from './components/events/events-list/events-list.component';
import { EventsDetailsComponent } from './components/events/events-details/events-details.component';
import { NewsListComponent } from './components/news/news-list/news-list.component';
import { NewsDetailsComponent } from './components/news/news-details/news-details.component';
import { PhotosListComponent } from './components/photos/photos-list/photos-list.component';
import { PhotosDetailsComponent } from './components/photos/photos-details/photos-details.component';
import { VideosListComponent } from './components/videos/videos-list/videos-list.component';
import { VideosDetailsComponent } from './components/videos/videos-details/videos-details.component';
import { SocialMediaComponent } from './components/social-media/social-media.component';
import { ArchivedNewsListComponent } from './components/news/archived-news/archived-news-list/archived-news-list.component';
import { ArchivedEventsListComponent } from './components/events/archived-events/archived-events-list/archived-events-list.component';


@NgModule({
  declarations: [
    EventsListComponent,
    EventsDetailsComponent,
    NewsListComponent,
    NewsDetailsComponent,
    PhotosListComponent,
    PhotosDetailsComponent,
    VideosListComponent,
    VideosDetailsComponent,
    SocialMediaComponent,
    ArchivedNewsListComponent,
    ArchivedEventsListComponent
  ],
  imports: [
    CommonModule,
    MediaCenterRoutingModule,
    SharedModule
  ]
})
export class MediaCenterModule { }
