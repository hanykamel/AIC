import { Component, OnInit } from '@angular/core';
import { MediaCenterService } from '../../../services/media-center.service';
import { BreadcrumbService } from 'xng-breadcrumb';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-photos-list',
  templateUrl: './photos-list.component.html',
  styleUrls: ['./photos-list.component.css']
})
export class PhotosListComponent implements OnInit {
  imgvid: any[];
  url: string;
  pageSize: number = 6;
  currentLang = localStorage.getItem('oldLanguage') || 'en';


  constructor(private mediaCenterService: MediaCenterService,
    private breadcrumbService: BreadcrumbService,
    private translate: TranslateService,
    private appService : AppService) { }

  ngOnInit(): void {
    this.url = "api/media/PhotoAlbumsList";
    this.handleBreadCrumbesTitle();
    /*this.mediaCenterService.GetPhotosList({}).subscribe(data => {
      this.imgvid = data.Items.map(p => ({ id: p.Id, title: p.Title, date: p.AlbumDate, desc: p.Brief, img: p.AlbumCoverImage }));
    });*/
  }

  prepareData(data) {
    return data.map(p => ({ id: p.Id, title: p.Title, date: p.AlbumDate, desc: p.Brief, img: p.AlbumCoverImage }));
  }

  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.PhotoGallery","PhotosTitle");
  }
}
