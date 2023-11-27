import { Component, OnInit } from '@angular/core';
import { MediaCenterService } from 'src/app/media-center/services/media-center.service';
import { Router } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.css']
})
export class NewsListComponent implements OnInit {
  newsData: any[] = [];
  url: string;
  sortingField: string;
  isSortingAscending: boolean;
  pagination: boolean;
  Filters: {}[];
  pageSize: number;
  currentLang = localStorage.getItem('oldLanguage') || 'en';

  constructor(private _router: Router, private _mediaCenterService: MediaCenterService,
    private breadcrumbService: BreadcrumbService,
    private translate: TranslateService,
    private appService : AppService) { }

  ngOnInit(): void {
    this.sortingField = "AICDate";
    this.isSortingAscending = false;
    this.Filters = [];
    this.pagination = true;
    this.url = "api/News/List";
    this.pageSize = 6;
    this.handleBreadCrumbesTitle();

  }

  onClickArchived = () => this._router.navigate(['/media-center/archived/news']);

  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.News","NewsTitle");
  }
}
