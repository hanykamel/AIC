import { Component, OnInit } from '@angular/core';
import { PhotoService } from '../../../../photoservice';
import { MediaCenterService } from '../../../services/media-center.service';
import { Router, ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-photos-details',
  templateUrl: './photos-details.component.html',
  styleUrls: ['./photos-details.component.css'],
  providers: [PhotoService]

})
export class PhotosDetailsComponent implements OnInit {

  url: string;
  sortingField: string;
  isSortingAscending: boolean;
  pagination: boolean;
  Filters: {}[];
  pageSize: number;
  albumDetails;
  constructor(private _route: ActivatedRoute, private mediaCenterService: MediaCenterService, private breadcrumbService: BreadcrumbService,
    private appService : AppService) { }
  ngOnInit() {
    this.handleBreadCrumbesTitle()
    this.albumDetails = null;
    this._route.params.subscribe(res => {
      const albumName = res?.['albumName']
      if (albumName) {
        this.GetPhotoAlbum(albumName)
      }
    })
  }

  GetPhotoAlbum(albumName) {
    this.sortingField = "AICDate";
    this.isSortingAscending = false;
    this.Filters = [];
    this.pagination = true;
    this.url = "/api/Media/PhotoAlbumPhotosList?albumName=" + albumName;
    this.pageSize = 6;
    this.mediaCenterService.GetAlbumDetails("api/media/GetPhotoAlbumDetails?albumName=" + albumName).subscribe(data => {
      if (data != null) {
        this.albumDetails = data;
        this.breadcrumbService.set('@albumName', data.Title);
      }
    });
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.PhotoGallery","PhotosTitle");
  }

}
