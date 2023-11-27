import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResourcesService } from '../../../services/resources.service';
import { CarouselComponent, OwlOptions, SlidesOutputData } from 'ngx-owl-carousel-o';
import { TranslateService } from '@ngx-translate/core';
import { BreadcrumbService } from 'xng-breadcrumb';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-best-practice-details',
  templateUrl: './best-practice-details.component.html',
  styleUrls: ['./best-practice-details.component.css']
})
export class BestPracticeDetailsComponent implements OnInit {

  details;
  displayVideo;
  videoEmbeded;
  videoURL;
  videoTitle;
  lang: string = 'ar';
  customOptions: OwlOptions = {
    loop: false,
    dots: false,
    autoplay: false,
    nav: true,
    margin: 30,
    rtl: this.lang === 'ar',
    stagePadding: 5,
    navText: ['<i class="material-icons-outlined">west</i>', '<i class="material-icons-outlined">east</i>'],
    responsive: {
      0: {
        items: 1,
        stagePadding: 0,
      },
      400: {
        items: 1,
        stagePadding: 0,
      },
      740: {
        items: 1,
        stagePadding: 10,
      },
      940: {
        items: 3
      }
    },
  }
  constructor(private _route: ActivatedRoute, private resourcesService: ResourcesService
    , private translate: TranslateService, private breadcrumbService: BreadcrumbService,
    private appService: AppService  ) { }

  ngOnInit(): void {
    this.handleBreadCrumbesTitle();
    this.lang = this.translate.currentLang;
    this.customOptions.rtl = this.lang === 'ar' ? true : false;
    this._route.params.subscribe(res => {
      const id = res?.['id']
      if (id)
        this.GetDetails(id);
    });
  }

  GetDetails(id) {
    this.resourcesService.GetBestPracticeById(id).subscribe(data => {
      this.details = data;
      this.breadcrumbService.set('@title', data.Title);
    });
  }
  MapPhotos(photos) {
    photos.forEach(p => {
      p.Title = p.name
    });
    return photos;
  }

  updateVideoData(video) {
    this.videoEmbeded = video.EmbedCode;
    this.videoURL = video.URL;
    this.videoTitle = video.Title;
    this.displayVideo = true;
  }

  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.BestPractices","BestPracticesTitle");
  }
}
