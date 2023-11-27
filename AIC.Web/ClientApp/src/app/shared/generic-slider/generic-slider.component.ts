import { Component, Input, OnInit, ViewChild, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { CarouselComponent, OwlOptions, SlidesOutputData } from 'ngx-owl-carousel-o';
import { CarouselService } from 'ngx-owl-carousel-o/lib/services/carousel.service';
import { Observable, Subscription } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-generic-slider',
  templateUrl: './generic-slider.component.html',
  styleUrls: ['./generic-slider.component.css']
})
export class GenericSliderComponent implements OnInit, OnChanges {
  @Input() slides;
  @Input() title;
  //@ViewChild('carousel', { static: true }) carousel: CarouselComponent;

  lang: string = 'ar';
  private eventsSubscription: Subscription;
  @Input() events: Observable<void>

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
  noDataAvailable: boolean = false;
  constructor(public translate: TranslateService,) { }
  ngOnChanges() {
    if (this.slides == null || this.slides == undefined || this.slides.length <= 0)
      this.noDataAvailable = true;
    else
      this.noDataAvailable = false
  }

  ngOnInit(): void {
    this.lang = this.translate.currentLang;
    this.customOptions.rtl = this.lang === 'ar' ? true : false;
  }

  getData(slides: SlidesOutputData) {
  }
 
  checkConditionalArrow(){

    const query = window.matchMedia("(max-width: 780px)")
    
    if((query.matches && this.slides?.length >= 2) || (query.matches == false && this.slides?.length > 3)){
      return true
    } else {
      return false
    }
  }

}
