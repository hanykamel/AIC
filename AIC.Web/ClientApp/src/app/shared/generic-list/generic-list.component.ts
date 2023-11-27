import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UrlsRoutes } from 'src/app/core/Enums/urlsRoutes';
import { MediaCenterService } from 'src/app/media-center/services/media-center.service';

@Component({
  selector: 'app-generic-list',
  templateUrl: './generic-list.component.html',
  styleUrls: ['./generic-list.component.css']
})
export class GenericListComponent implements OnInit {
  @Input() hpcHead;
  @Input() cards;
  @Input() title;
  @Input() buttonTitle;
  @Input() newsView = false;
  @Input() archivedNewsView = false;
  @Input() eventsView = false;
  @Input() archivedEventsView = false;
  @Input() WhitePapersView = false;
  @Input() BestPracticeView = false;
  filters: { field: string; value: string; }[] = [];
  body: {};
  archivedNewsData: any[] = [];
  url;
  constructor(private _router: Router,
    private _mediaCenterService: MediaCenterService) { }

  ngOnInit(): void {
  }

  onClickForDetails(id) {
    let urlSections = this._router.url.split('/', 6);
    this.url = urlSections[urlSections.length - 1]
    switch (this.url) {
      case UrlsRoutes.NewsList: {
        this._router.navigate(['/media-center/news/news-details/' + id]);
        break;
      }
      case UrlsRoutes.EventsList: {
        this._router.navigate(['/media-center/events/events-details/' + id]);
        break;
      }
      case UrlsRoutes.WhitePapers: {
        this._router.navigate(['/resources/white-papers/white-papers-details/' + id]);
        break;
      }
      case UrlsRoutes.BestPracticeList: {
        this._router.navigate(['/resources/best-practice/best-practice-details/' + id]);
        break;
      }
      case UrlsRoutes.Hpc: {
        this._router.navigate(['/performance-computing/hpc/hpc-project-details/' + id]);
        break;
      }
    }
  } 


}
