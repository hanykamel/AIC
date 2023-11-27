import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppService } from 'src/app/app.service';
import { SharedService } from 'src/app/shared/shared.service';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-committee-details',
  templateUrl: './committee-details.component.html',
  styleUrls: ['./committee-details.component.css', '../../../../shared/page-loader/page-loader.component.css']
})
export class CommitteeDetailsComponent implements OnInit {
  bodDetailsData: any;
  loading: boolean;
  constructor(private _route: ActivatedRoute,
    private _sharedService: SharedService, private breadcrumbService: BreadcrumbService,
    private appService: AppService) { }

  ngOnInit(): void {
    this.handleBreadCrumbesTitle();
    this._route.params.subscribe(res => {
      const id = res?.['id']
      if (id)
        this.getBodAndScientificData(id)
    })
  }

  getBodAndScientificData(id) {
    this._sharedService.GetBodAndScientificById(id).subscribe(data => {
      this.ParsingDataToSharedComponent(data);
      this.breadcrumbService.set('@committeeDetails', data.Name);
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
    this.bodDetailsData = object;
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.CommitteeList","CommitteeListTitle");
  }
}
