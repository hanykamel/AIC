import { Component, OnInit } from '@angular/core';
import { MediaCenterService } from 'src/app/media-center/services/media-center.service';
import { Router } from '@angular/router';
import { CareersService } from '../../../services/careers.service';
import { Observable } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { BreadcrumbService } from 'xng-breadcrumb';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-careers-list',
  templateUrl: './careers-list.component.html',
  styleUrls: ['./careers-list.component.css']
})
export class CareersListComponent implements OnInit {
filters: { field: string; value: string; } [] = [];
url: string;
sortingField: string;
isSortingAscending: boolean;
pagination: boolean;
Filters: { } [];
pageSize: number;
lang;
currentLang = localStorage.getItem('oldLanguage') || 'en';

  constructor(private _router: Router, private translateService: TranslateService,
    private breadcrumbService: BreadcrumbService,
    private appService: AppService) { }

  ngOnInit(): void {
  this.lang = this.translateService.currentLang;
  this.Filters = [];
  this.pagination = true;
  this.url = "api/Careers/List";
  this.pageSize = 9;
  this.sortingField = 'AICDate'
  this.isSortingAscending = false;
  this.handleBreadCrumbesTitle();
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.Careers","CareersTitle");
  }
}
