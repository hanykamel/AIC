import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from '../../../../app.service';
import { BreadcrumbService } from 'xng-breadcrumb';


@Component({
  selector: 'app-committee-list',
  templateUrl: './committee-list.component.html',
  styleUrls: ['./committee-list.component.css']
})
export class CommitteeListComponent implements OnInit {

  filters: { field: string; value: string; }[] = [];
  body: {};
  bodAndScientificData: any[] = [];
  tempData: any;
  title = "BOD and Scientific Committee";
  currentLang = localStorage.getItem('oldLanguage') || 'en';
  constructor(private _sharedService: SharedService, public translate: TranslateService
    , private appService: AppService,
    private breadcrumbService: BreadcrumbService  ) { }

  ngOnInit(): void {
    //this.appService.pageLoader.next({ show: true });
    this.GetBodAndScientificList();
    this.handleBreadCrumbesTitle();
  }

  GetBodAndScientificList() {
    this.body = {
      filters: this.filters
    }
    this._sharedService.GetBodAndScientificList(this.body).subscribe(data => {
      this.ParsingDataToSharedComponent(data.Items);
      //this.appService.pageLoader.next({ show: false });
    }, err => {
    })
  }

  ParsingDataToSharedComponent(data:any){
    data.forEach((element) => {
      var object : any = new Object();
      object.id = element.Id;
      object.name = element.Name;
      object.title = element.Title;
      object.img = element.AICImage;
      this.bodAndScientificData.push(object);   
    });
  }

  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.CommitteeList","CommitteeListTitle");
  }

}
