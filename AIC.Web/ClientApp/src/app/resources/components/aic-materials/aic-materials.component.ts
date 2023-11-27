import { Component, OnInit, AfterViewChecked , ViewChildren, QueryList, HostListener, ViewChild, ElementRef } from '@angular/core';
import { Subject } from 'rxjs';
import { ResourcesService } from '../../services/resources.service';
import { AppService } from '../../../app.service';
import { CarouselComponent, OwlOptions, SlidesOutputData } from 'ngx-owl-carousel-o';
import { TranslateService } from '@ngx-translate/core';
import { BreadcrumbService } from 'xng-breadcrumb';


@Component({
  selector: 'app-aic-materials',
  templateUrl: './aic-materials.component.html',
  styleUrls: ['./aic-materials.component.css','../../../shared/generic-slider/generic-slider.component.css']
})
export class AicMaterialsComponent implements OnInit {
  slides;
  title = "Material Title";
  materialsList;
  //eventsSubject: Subject<void> = new Subject<void>();
  materialListUrl;
  Filters: {}[];
  pageSize = 9;
  lang: string = 'ar';
  displayVideo;
  videoEmbeded;
  videoURL;
  videoTitle;
  hideNext = false;
  hidePrev = false;
  currentLang = localStorage.getItem('oldLanguage') || 'en';
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
  constructor(private resourcesService: ResourcesService, private appService: AppService
    , private translate: TranslateService,
    private breadcrumbService: BreadcrumbService ) {
    
  }

  ngOnInit(): void {
    this.lang = this.translate.currentLang;
    this.customOptions.rtl = this.lang === 'ar' ? true : false;
    //this.appService.pageLoader.next({ show: true });
    /* this.getMateriallList()*/
    this.hidePrev = true;
    this.getMateriallList()
    this.handleBreadCrumbesTitle();
  }
  prev() {
    document.querySelector('.p-tabview-nav-content').scrollLeft -= 100;
    this.hideNext = false
    if (document.querySelector('.p-tabview-nav-content').scrollLeft <= 100) {
      this.hidePrev = true;
    }
  }
  next() {
    document.querySelector('.p-tabview-nav-content').scrollLeft += 100;
    this.hidePrev = false;
    if (
      (document.querySelector('.p-tabview-nav-content').scrollWidth -
        document.querySelector('.p-tabview-nav-content').clientWidth - 100)
      <= document.querySelector('.p-tabview-nav-content').scrollLeft) {
      this.hideNext = true;
    } else {
      this.hideNext = false
    }
  }
  prevAr() {
    document.querySelector('.p-tabview-nav-content').scrollLeft += 100;
    this.hideNext = false
    if (document.querySelector('.p-tabview-nav-content').scrollLeft >= -100) {
      this.hidePrev = true;
    }

  }
  nextAr() {
    document.querySelector('.p-tabview-nav-content').scrollLeft -= 100;
    this.hidePrev = false;
    if (
      (document.querySelector('.p-tabview-nav-content').scrollWidth -
        document.querySelector('.p-tabview-nav-content').clientWidth - 100)
      <= (-document.querySelector('.p-tabview-nav-content').scrollLeft)) {
      this.hideNext = true;
    } else {
      this.hideNext = false
    }

  }


  getMateriallList() {
    this.resourcesService.GetMaterialsList().subscribe(data => {
      this.materialsList = data;
      if (data[0]?.Id) {
        this.loadMaterialList(data[0].Id)
      }
      this.slides = this.materialsList.VideosList;
      //this.appService.pageLoader.next({ show: false });
    })
  }
  emitEventToChild(event) {
    let tabTitle = event?.originalEvent?.target?.innerText;
    let selectedId = this.materialsList.find(e => e.Title == tabTitle)?.Id;
    if (selectedId) {
      this.loadMaterialList(selectedId);
    }
    //this.eventsSubject.next();
  }
  loadMaterialList(subjectId) {
    this.materialListUrl = "api/AICMaterials/ListAICMaterials";
    this.Filters = [{
      Field: "Subject",
      Operator: "Eq",
      Value: subjectId.toString(),
      FieldValueType: "Lookup"
    }];
  }

  updateVideoData(video) {
    this.videoEmbeded = video.EmbedCode;
    this.videoURL = video.URL;
    this.videoTitle = video.Title;
    this.displayVideo = true;
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.AICMaterials","AICMaterialsTitle");
  }
}
