import { Component, OnInit } from '@angular/core';
import { MediaCenterService } from 'src/app/media-center/services/media-center.service';
import { Router } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.css']
})
export class EventsListComponent implements OnInit {
  filters: { field: string; value: string; }[] = [];
  url: string;
  sortingField: string;
  isSortingAscending: boolean;
  pagination: boolean;
  Filters: {}[];
  pageSize: number;
  currentLang = localStorage.getItem('oldLanguage') || 'en';

  constructor(private _router: Router,private _mediaCenterService: MediaCenterService,
    private breadcrumbService: BreadcrumbService,
    private translate: TranslateService,
    private appService : AppService) { }

  ngOnInit(): void {
    this.sortingField = "AICDate";
    this.isSortingAscending = false;
    this.Filters = [];
    this.pagination = true;
    this.url = "api/Events/List";
    this.pageSize = 6;
    this.handleBreadCrumbesTitle();
  }

  onClickArchivedEvents = () => this._router.navigate(['/media-center/archived/events']);

  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.Events","EventsTitle");
  }
}
