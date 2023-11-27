import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { UrlsRoutes } from '../../../core/Enums/urlsRoutes';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  @Input() items;
  @Input() title;
  url;
  constructor(private _router: Router) { }

  ngOnInit(): void {
  }
  onClick(id) {
    let urlSections = this._router.url.split('/', 6);
    this.url = urlSections[urlSections.length - 1]
    console.log(this.items.filter(x => x.id == id)[0]);
    switch(this.url){
      case UrlsRoutes.CommitteeList: {
        if (this.items.filter(x => x.id == id)[0].brief != "")
          this._router.navigate(['/about-us/committee-list/committee-details/'+id]);
        break;
      } 
      case UrlsRoutes.OurPeopleList: {
        if (this.items.filter(x => x.id == id)[0].brief != "")
          this._router.navigate(['/about-us/our-people-list/our-people-details/'+id]);
        break;
      } 
    }
  }
}
