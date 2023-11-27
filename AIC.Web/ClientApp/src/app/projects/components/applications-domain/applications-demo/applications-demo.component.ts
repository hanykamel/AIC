import { Component, OnInit } from '@angular/core';
import { CarouselComponent, OwlOptions, SlidesOutputData } from 'ngx-owl-carousel-o';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-applications-demo',
  templateUrl: './applications-demo.component.html',
  styleUrls: ['./applications-demo.component.css']
})
export class ApplicationsDemoComponent implements OnInit {
  projectDetails;
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
  constructor(public translate: TranslateService, private _router: Router,
    private appService: AppService,
    private breadcrumbService: BreadcrumbService) { }

  ngOnInit(): void {
    this.handleBreadCrumbesTitle();
    this.lang = this.translate.currentLang;
    this.customOptions.rtl = this.lang === 'ar' ? true : false;
    if (sessionStorage.getItem('projectDetails')) {
      this.projectDetails = JSON.parse(sessionStorage.getItem('projectDetails'));
    } else
      this._router.navigate(['/']);
  }

  updateVideoData(video) {
    this.videoEmbeded = video.EmbedCode;
    this.videoURL = video.URL;
    this.videoTitle = video.Title;
    this.displayVideo = true;
  }

  checkConditionalArrow(){

    const query = window.matchMedia("(max-width: 780px)")
    
    if((query.matches && this.projectDetails.DemoVideosList.length >= 2) || (query.matches == false && this.projectDetails.DemoVideosList.length > 3)){
      return true
    } else {
      return false
    }
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.News", "NewsTitle");
    let title = localStorage.getItem("ProjectTitle");
    this.breadcrumbService.set('@projectTitle', title);
  }

}
