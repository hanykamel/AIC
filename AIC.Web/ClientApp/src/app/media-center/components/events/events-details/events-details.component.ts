import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { MediaCenterService } from 'src/app/media-center/services/media-center.service';
import { CarouselComponent, OwlOptions, SlidesOutputData } from 'ngx-owl-carousel-o';
import { TranslateService } from '@ngx-translate/core';
import { BreadcrumbService } from 'xng-breadcrumb';


@Component({
  selector: 'app-events-details',
  templateUrl: './events-details.component.html',
  styleUrls: ['./events-details.component.css']
})
export class EventsDetailsComponent implements OnInit {
  eventsData: any[] = [];
  photosList;
  videosList;
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
  constructor(private _mediaCenterService: MediaCenterService,
    private _route: ActivatedRoute,
    public appService: AppService
    , private translate: TranslateService, private breadcrumbService: BreadcrumbService   ) { }
  ngOnInit(): void {
    this.handleBreadCrumbesTitle();
    this.lang = this.translate.currentLang;
    this.customOptions.rtl = this.lang === 'ar' ? true : false;
    //this.appService.pageLoader.next({show:true});
    this._route.params.subscribe(res => {
      const id = res?.['id']
      if (id){
        this.GetEventsById(id)
      }
    })
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.Events","EventsTitle");
  }

  GetEventsById(id){
    this._mediaCenterService.GetEventById(id).subscribe(data => {
      this.photosList = data.EventImagesList;
      this.videosList = data.EventVideosList;
      this.ParsingDataToSharedComponent(data)
      this.breadcrumbService.set('@title', data.Title);
      //console.log(this.eventsData)
    }, err => {
    })
    //this.appService.pageLoader.next({show:false});

  }
  ParsingDataToSharedComponent(data: any) {
    if (data) {
      var object: any = new Object();
      object.title = data.Title;
      object.subTitle = data.EventType;
      object.img = data.MainImage;
      object.desc = data.Description;
      object.eventDate = data.EventDate;
      object.endDate = data.EndDate == "0001-01-01T00:00:00" ? null : data.EndDate;
      object.Address = data.Address;
      object.Latitude = data.Latitude;
      object.Longitude = data.Longitude;
      this.eventsData.push(object);
    }
  }


  updateVideoData(video) {
    this.videoEmbeded = video.EmbedCode;
    this.videoURL = video.URL;
    this.videoTitle = video.Title;
    this.displayVideo = true;
  }
}
