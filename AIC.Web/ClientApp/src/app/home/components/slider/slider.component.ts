import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { HomeService } from '../home.service';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.css'],
})
export class SliderComponent implements OnInit {
  lang: string = 'ar';
  display: boolean = false;

  slides;
  customOptions: OwlOptions = {
    loop: false,
    rewind: true,
    dots: true,
    autoplay: true,
    nav: false,
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
        items: 1,
      },
      740: {
        items: 1,
      },
      940: {
        items: 1,
      },
      1920: {
        items: 1,
      },
    },
  };
  @Output() dataEmitter = new EventEmitter();
  data: any[] = [];
  mainBannerCount: any;
  noDataAvailable: boolean = false;
  constructor(
    private homeService: HomeService,
    public translate: TranslateService,
    private appService: AppService
  ) {}

  ngOnInit(): void {
    //this.appService.pageLoader.next({show : true});
    this.lang = this.translate.currentLang;
    this.customOptions.rtl = this.lang === 'ar' ? true : false;
    this.GetMainBanner();
  }
  GetMainBanner() {
    this.homeService.GetMainBanner().subscribe(
      (data) => {
        //console.log(data);
        if (
          data.Items == null ||
          data.Items == undefined ||
          data.Items.length <= 0
        )
          this.noDataAvailable = true;
        else {
          this.noDataAvailable = false;
          this.ParsingData(data.Items);
        }
        //this.appService.pageLoader.next({show : false});
      },
      (err) => {
        //this.appService.pageLoader.next({show : false});
      }
    );
  }

  ParsingData(data: any) {
    if (data) {
      data.forEach((element) => {
        var object: any = new Object();
        object.id = element.Id;
        object.title = element.Title;
        object.desc = element.Brief;
        object.videoUrl = element.VideoUrl;
        object.readMoreUrl = element.ReadMoreUrl;
        object.img = '../../../assets/images/home/slider.png';
        this.data.push(object);
        this.dataEmitter.emit(this.data);
      });
      this.mainBannerCount = '0' + this.data.length;
      return this.data;
    }
  }
  onClick(slide) {
    window.open(slide.readMoreUrl);
  }

  showDialog() {
    this.display = true;
  }
}
