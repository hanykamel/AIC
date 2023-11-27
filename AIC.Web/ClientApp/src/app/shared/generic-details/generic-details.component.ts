import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-generic-details',
  templateUrl: './generic-details.component.html',
  styleUrls: ['./generic-details.component.css']
})
export class GenericDetailsComponent implements OnInit {
  @Input() details;
  @Input() newsView = false;
  @Input() whitePapersView = false;
  @Input() eventView = false;
  
  constructor(private _router: Router) { }

  ngOnInit(): void {
  }
  onClickRealtedLinks(ids) {
    if (this.newsView)
      this._router.navigate(["media-center/news/news-details/" + this.details[0].Id + "/related-news", ids]);
    else
      this._router.navigate(['/projects/related-news', ids]);
  }
}
