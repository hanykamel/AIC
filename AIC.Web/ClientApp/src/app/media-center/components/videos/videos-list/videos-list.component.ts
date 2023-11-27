import { Component, OnInit } from '@angular/core';
import { MediaCenterService } from '../../../services/media-center.service';
import { BreadcrumbService } from 'xng-breadcrumb';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-videos-list',
  templateUrl: './videos-list.component.html',
  styleUrls: ['./videos-list.component.css']
})
export class VideosListComponent implements OnInit {
  imgvid: any[];
  url: string;
  pageSize: number = 6;
  currentLang = localStorage.getItem('oldLanguage') || 'en';

  constructor(private mediaCenterService: MediaCenterService,
    private breadcrumbService: BreadcrumbService,
    private translate: TranslateService,
    private appService : AppService) { }

  ngOnInit(): void {
    this.url =  "api/media/VideoAlbumsList";
    this.handleBreadCrumbesTitle();
    /*this.mediaCenterService.GetVideosList({}).subscribe(data => {
      this.imgvid = data.Items.map(p => ({ id: p.Id, title: p.Title, date: p.AlbumDate, desc: p.Brief, img: p.AlbumCoverImage }));
    });*/
  }

  prepareData(data) {
    return data.map(p => ({ id: p.Id, title: p.Title, date: p.AlbumDate, desc: p.Brief, img: p.AlbumCoverImage }));
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.VideoGallery","VideosTitle");
  }
}
