import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/app.service';
import { SharedService } from 'src/app/shared/shared.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-our-people-list',
  templateUrl: './our-people-list.component.html',
  styleUrls: ['./our-people-list.component.css']
})
export class OurPeopleListComponent implements OnInit {
  title = "Our People";
  filters: { field: string; value: string; }[] = [];
  body: {};
  ourPeopleData: any[] = [];
  tempData: any;
  lang: string;
  currentLang = localStorage.getItem('oldLanguage') || 'en';
  constructor(private _sharedService: SharedService,
    private breadcrumbService: BreadcrumbService,
    private translate: TranslateService,
    private appService: AppService) { }

  ngOnInit(): void {
    this.GetOurPeopleList();
    this.handleBreadCrumbesTitle();
  }

  GetOurPeopleList() {
    this.body = {
      filters: this.filters
    }
    this._sharedService.GetOurPeopleList(this.body).subscribe(data => {
        this.ParsingDataToSharedComponent(data.Items);
    }, err => {
    })
  }
  ParsingDataToSharedComponent(data: any) {
    console.log(data);
    data.forEach((element) => {
      var object : any = new Object();
      object.id = element.Id;
      object.name = element.Name;
      object.title = element.Title;
      object.img = element.AICImage;
      object.brief = element.AICBrief;
      this.ourPeopleData.push(object);   
    });
    console.log(this.ourPeopleData);
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.OurPeople","OurPeopleTitle");
  }
}
