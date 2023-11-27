import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { ResourcesService } from 'src/app/resources/services/resources.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-white-papers-details',
  templateUrl: './white-papers-details.component.html',
  styleUrls: ['./white-papers-details.component.css']
})
export class WhitePapersDetailsComponent implements OnInit {
  
  detailsData: any[] = [];

  constructor(private _route: ActivatedRoute,
    private _resourcesService: ResourcesService,
    public appService: AppService, private breadcrumbService: BreadcrumbService) { }

  ngOnInit(): void {
    this.handleBreadCrumbesTitle();
    //this.appService.pageLoader.next({show:true});
    this._route.params.subscribe(res => {
      const id = res?.['id']
      if (id)
        this.GetWhitePaperById(id)
    })
  }
  GetWhitePaperById(id: string) {
    this._resourcesService.GetWhitePapersById(id).subscribe(data => {
      this.ParsingDataToSharedComponent(data);
      this.breadcrumbService.set('@title', data.Title);
    }, err => {
    })
    //this.appService.pageLoader.next({show:false});

  }
  ParsingDataToSharedComponent(data: any) {
    if (data) {
      var object: any = new Object();
      object.title = data.Title;
      object.subTitle = ',' + data.Type;
      object.date = data.Date;
      object.url = data.Url;
      object.desc = data.Description;
      this.detailsData.push(object);
    }
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.WhitePapers","WhitePapersTitle");
  }
}
