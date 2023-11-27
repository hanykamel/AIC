import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { AppService } from 'src/app/app.service';
import { UrlsRoutes } from 'src/app/core/Enums/urlsRoutes';
import { HomeService } from '../home.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-highlights',
  templateUrl: './highlights.component.html',
  styleUrls: ['./highlights.component.css'],
})
export class HighlightsComponent implements OnInit {
  lang: string = 'ar';
  slides;
  slidePosition: number;
  @Output() dataEmitter = new EventEmitter();

  customOptions: OwlOptions = {
    loop: false,
    dots: false,
    autoplay: false,
    nav: true,
    margin: 30,
    rtl: this.lang === 'ar',
    navText: [
      '<i class="material-icons-outlined">west</i>',
      '<i class="material-icons-outlined">east</i>',
    ],
    responsive: {
      0: {
        items: 1,
      },
      400: {
        items: 2,
      },
      740: {
        items: 2,
      },
      768: {
        items: 2,
      },
      820: {
        items: 2,
        },
      940: {
        items: 3,
      },
      1200: {
        items: 3,
      },
      1366: {
        items: 3,
      },
      1920: {
        items: 3,
      },
    },
  };
  data: any;
  constructor(
    private homeService: HomeService,
    private _router: Router,
    public translate: TranslateService,
    public appService: AppService
  ) {}

  ngOnInit(): void {
    //this.appService.pageLoader.next({ show: true });
    this.lang = this.translate.currentLang;
    this.customOptions.rtl = this.lang === 'ar' ? true : false;
    this.GetAicHighlights();
  }
  GetAicHighlights() {
    this.homeService.GetHighlights().subscribe(
      (data) => {
        this.data = data;
        this.dataEmitter.emit(this.data);
        //this.appService.pageLoader.next({show:false});
      },
      (err) => {}
    );
    //this.appService.pageLoader.next({show:false});
  }
  onClick(slide) {
    switch (slide.ListName) {
      case UrlsRoutes.News: {
        this._router.navigate(['/media-center/news/news-details/' + slide.Id]);
        break;
      }
      case UrlsRoutes.Events: {
        this._router.navigate([
          '/media-center/events/events-details/' + slide.Id,
        ]);
        break;
      }
    }
  }

  getData(e) {
    this.slidePosition = e.startPosition;
    //console.log('slide' + this.slidePosition);
  }
}
