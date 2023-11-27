import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SharedService } from '../shared.service';
import { AppService } from 'src/app/app.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-related-news',
  templateUrl: './related-news.component.html',
  styleUrls: ['./related-news.component.css']
})
export class RelatedNewsComponent implements OnInit {
  relatedLinks;
  constructor(private _route: ActivatedRoute, private sharedService: SharedService,
    private appService: AppService,
    private breadcrumbService: BreadcrumbService) { }

  ngOnInit(): void {
    this.handleBreadCrumbesTitle();
    this.GetRelatedLinksTitle();
    this._route.params.subscribe(res => {
      let ids = res?.['ids'];
      if (ids)
        this.GetRelatedLinks(ids.split(','));
    });
  }
  GetRelatedLinksTitle() {
    let title = localStorage.getItem("NewsTitle");
    this.breadcrumbService.set('@title', title);
  }

  GetRelatedLinks(ids) {
    this.sharedService.ListRelatedLinks(ids).subscribe(data => {
      this.relatedLinks = data;
    });
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.News", "NewsTitle");
    let title = localStorage.getItem("ProjectTitle");
    this.breadcrumbService.set('@projectTitle', title);
  }
}
