import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { SharedService } from 'src/app/shared/shared.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-our-people-details',
  templateUrl: './our-people-details.component.html',
  styleUrls: ['./our-people-details.component.css']
})
export class OurPeopleDetailsComponent implements OnInit {
  ourPeopleDetailsData: any;
  loading:boolean;
  constructor(private _route: ActivatedRoute,
    private _sharedService: SharedService, private breadcrumbService: BreadcrumbService,
    private appService: AppService) { }

  ngOnInit(): void {
    this.handleBreadCrumbesTitle();
    this.loading = true;
    this._route.params.subscribe(res => {
      const id = res?.['id']
      if (id)
        this.getOurPeopleData(id)
    })
  }
  getOurPeopleData(id) {
    this._sharedService.GetOurPeopleById(id).subscribe(data => {
      this.ParsingDataToSharedComponent(data);
      this.breadcrumbService.set('@personName', data.Name);
      setTimeout(() => { 
        this.loading = false;
      }, 1000);
   
 
    }, err => {
    })
  }
  ParsingDataToSharedComponent(data: any) {
    var object: any = new Object();
    object.name = data.Name;
    object.title = data.Title;
    object.img = data.AICImage;
    object.desc = data.AICBrief;
    this.ourPeopleDetailsData = object;
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.OurPeople","OurPeopleTitle");
  }
}
