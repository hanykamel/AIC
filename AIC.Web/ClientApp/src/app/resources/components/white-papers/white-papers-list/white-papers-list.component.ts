import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/app.service';
import { ResourcesService } from 'src/app/resources/services/resources.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-white-papers-list',
  templateUrl: './white-papers-list.component.html',
  styleUrls: ['./white-papers-list.component.css'],
})
export class WhitePapersListComponent implements OnInit {
  url: string;
  sortingField: string;
  isSortingAscending: boolean;
  pagination: boolean;
  Filters: {}[];
  pageSize: number;
  currentLang = localStorage.getItem('oldLanguage') || 'en';
  constructor(private _resourcesService: ResourcesService,
    private breadcrumbService: BreadcrumbService,
    private translate: TranslateService,
    private appService: AppService) {}

  ngOnInit(): void {
    this.isSortingAscending = false;
    this.Filters = [];
    this.pagination = true;
    this.url = 'api/WhitePapers/list';
    this.pageSize = 6;
    this.handleBreadCrumbesTitle();
  }

  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.WhitePapers","WhitePapersTitle");
  }
}
