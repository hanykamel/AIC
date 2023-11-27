import { Component, OnInit } from '@angular/core';
import { GenericContentTypes } from '../../../../core/Enums/genericContentTypes';
import { SharedService } from 'src/app/shared/shared.service';
import { TranslateService } from '@ngx-translate/core';
import { BreadcrumbService } from 'xng-breadcrumb';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-internships-list',
  templateUrl: './internships-list.component.html',
  styleUrls: ['./internships-list.component.css']
})
export class InternshipsListComponent implements OnInit {
  filters: { field: string; value: string; }[] = [];
  url: string;
  sortingField: string;
  isSortingAscending: boolean;
  pagination: boolean;
  Filters: {}[];
  pageSize: number;
  desc: string;
  lang;
  currentLang = localStorage.getItem('oldLanguage') || 'en';
  constructor(private _sharedService: SharedService, private translateService: TranslateService,
    private breadcrumbService: BreadcrumbService,
    private appService: AppService) { }

  ngOnInit(): void {
    this.lang = this.translateService.currentLang;
    this.Filters = [];
    this.pagination = true;
    this.url = "api/Internships/List";
    this.pageSize = 9;
    this.sortingField = 'AICDate'
    this.isSortingAscending = false;
    this.handleBreadCrumbesTitle();

    this._sharedService.GetGenericContent(GenericContentTypes.Internships).subscribe(data => {
      this.desc = data.Items[0].AICDesc;
    }, err => {
    })

    
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.Internships","InternshipsTitle");
  }
}
