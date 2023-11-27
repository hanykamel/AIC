import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/app.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-best-practice-list',
  templateUrl: './best-practice-list.component.html',
  styleUrls: ['./best-practice-list.component.css'],
})
export class BestPracticeListComponent implements OnInit {
  filters: { field: string; value: string }[] = [];
  url: string;
  pageSize: number;
  sortingField: string;
  isSortingAscending: boolean;
  pagination: boolean;
  currentLang = localStorage.getItem('oldLanguage') || 'en';
  constructor(private breadcrumbService: BreadcrumbService,
    private translate: TranslateService,
    private appService: AppService) {}

  ngOnInit(): void {
    this.url = 'api/BestPractice/List';
    this.pageSize = 9;
    this.sortingField = 'AICDate';
    this.isSortingAscending = false;
    this.filters = [];
    this.pagination = true;
    this.handleBreadCrumbesTitle();
  }

  prepareData(data) {
    data.forEach((element) => {
      element.id = element.Id;
      element.title = element.Title;
      element.date = element.PractiseDate;
      element.desc = element.Brief;
    });
    return data;
  }

  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.BestPractices","BestPracticesTitle");
  }
}
