import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { OwlOptions, SlidesOutputData, CarouselComponent, CarouselModule } from 'ngx-owl-carousel-o';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { MediaCenterService } from '../../../services/media-center.service';
import { TranslateService } from '@ngx-translate/core';
import { BreadcrumbService } from 'xng-breadcrumb';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-videos-details',
  templateUrl: './videos-details.component.html',
  styleUrls: ['./videos-details.component.css']
})
export class VideosDetailsComponent implements OnInit {
  slides;
  lang: string = 'ar';
  videoAlbumDetails;
  currentUrl = '';
  activeSlide: any;
  current: number = 0;
  prev: number = -1;
  next: number = 1;

  url: string;
  sortingField: string;
  isSortingAscending: boolean;
  pagination: boolean;
  Filters: {}[];
  pageSize: number;


  constructor(private _route: ActivatedRoute,
    private _router: Router, private location: Location,
    private activatedRoute: ActivatedRoute, private mediaCenterService: MediaCenterService
    , public translate: TranslateService, private breadcrumbService: BreadcrumbService,
    private appService : AppService) {
  }

  ngOnInit(): void {
    this.handleBreadCrumbesTitle();
    this.slides = [];
    this._route.params.subscribe(res => {
      const albumName = res?.['albumName'];
      if (albumName) {
        this.GetVideoAlbum(albumName)
      }
    })

    this.lang = localStorage.getItem('oldLanguage');
    this.currentUrl = this._router.url;
  }
  activateSlider(slide, index) {
    this.activeSlide = slide;
    this.current = index;
    this.prev = index - 1;
    this.next = index + 1;
  }

  previousSlide() {
    this.current = this.prev;
    this.prev = this.prev - 1;
    this.next = this.next - 1;
    this.activeSlide = this.slides[this.current];
  }

  nextSlide() {
    this.current = this.next;
    this.prev = this.prev + 1;
    this.next = this.next + 1;
    this.activeSlide = this.slides[this.current];
  }
 
  GetVideoAlbum(albumName) {
    this.sortingField = "AICDate";
    this.isSortingAscending = false;
    this.Filters = [];
    this.pagination = true;
    this.url = "/api/Media/VideoAlbumVideosList?albumName=" + albumName;
    this.pageSize = 9;
    this.mediaCenterService.GetVideoAlbumDetails(albumName).subscribe(data => {
      this.videoAlbumDetails = data;
      this.breadcrumbService.set('@albumName', data.Title);

    });

    /*this.mediaCenterService.GetVideosAlbum({}, albumName).subscribe(data => {
      this.slides = data.Items;
      this.activeSlide = this.slides[0];
    })*/
  }

  setData(data) {
    this.slides = data;
    this.activeSlide = this.activeSlide ? this.activeSlide : this.slides[0];
    return data;
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.VideoGallery","VideosTitle");
  }
}
