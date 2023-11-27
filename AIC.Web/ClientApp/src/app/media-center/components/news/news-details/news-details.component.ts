import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { AppService } from 'src/app/app.service';
import { MediaCenterService } from 'src/app/media-center/services/media-center.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-news-details',
  templateUrl: './news-details.component.html',
  styleUrls: ['./news-details.component.css']
})
export class NewsDetailsComponent implements OnInit {

  newsData: any[] = [];
  constructor(private _mediaCenterService: MediaCenterService,
    private _route: ActivatedRoute,
    public appService: AppService, private breadcrumbService: BreadcrumbService) { }

  ngOnInit(): void {
    this.handleBreadCrumbesTitle();
    this._route.params.subscribe(res => {
      const id = res?.['id']
      if (id) {
        this.GetNewsById(id)
      }
    })
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.News", "NewsTitle");
  }

  GetNewsById(id) {
    this._mediaCenterService.GetNewsById(id).subscribe(data => {
      this.ParsingDataToSharedComponent(data)
      localStorage.setItem("NewsTitle", data.Title ? data.Title : null);
      this.breadcrumbService.set('@title', data.Title);
    }, err => {
    })
  }
  ParsingDataToSharedComponent(data: any) {
    if (data) {
      var object: any = new Object();
      object.Id = data.Id;
      object.title = data.Title;
      object.subTitle = (data.NewsType ? data.NewsType + ',' : '') + (data.Sector ? data.Sector + ',' : '') + data.Technology;
      object.img = data.MainImage;
      object.desc = data.Description;
      object.date = data.PublishDate;
      object.url = data.SourceUrl;
      object.source = data.Source;
      object.RelatedLinksIds = data.RelatedMediaLinks ? data.RelatedMediaLinks.map(l => l.Id).join(',') : null;
      this.newsData.push(object);
    }
  }

}
