import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ProjectsService } from '../../../services/projects.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-applications-details',
  templateUrl: './applications-details.component.html',
  styleUrls: ['./applications-details.component.css']
})
export class ApplicationsDetailsComponent implements OnInit {
  projectDetails: any = null;
  id;
  type;
  constructor(private _route: ActivatedRoute, private projectsService: ProjectsService
    , private _router: Router, private breadcrumbService: BreadcrumbService,
    private translate: TranslateService) { }

  ngOnInit(): void {
    this._route.params.subscribe(res => {
      this.id = res?.['id'];
      this.type = res?.['type'] ? res?.['type'] : null;

      if (this.id)
        this.GetProjectDetails();
      else
        this._router.navigate(['/']);
    });
  }

  GetProjectDetails() {
    this.projectsService.GetProjectById(this.id).subscribe(data => {
      this.projectDetails = data;
      //console.log(data);
      var title = '';
      const pageTitleApplication = this.translate.instant('Projects.ApplicationsDomain');
      const pageTitleTechnology = this.translate.instant('Projects.TechnologyDomain');
      if (this.type == 'application')
        title = pageTitleApplication + "  >  " + data.ApplicationDomain + "  >  " + data.Title;
      else
        title = pageTitleTechnology + "  >  " + data.TechnologyDomain + "  >  " + data.Title;
      localStorage.setItem("ProjectTitle", title);
      this.breadcrumbService.set('@projectTitle', title);
    });
  }

  onClickDemo() {
    sessionStorage.setItem("projectDetails", JSON.stringify(this.projectDetails));
    this._router.navigate(["/projects/list/" + this.type + "/details/" + this.id + "/demo"]);
  }

  ShowProjectDemo() {
    return this.projectDetails.URL || this.projectDetails.DemoPhotosList?.length > 0 || this.projectDetails.DemoVideosList?.length > 0 || this.projectDetails.DemoBrief;
  }

  ShowRelatedLinks() {
    return this.projectDetails.RelatedMediaLinks && this.projectDetails.RelatedMediaLinks.length > 0;
  }
  onClickRelatedLinks() {
    let ids = this.projectDetails.RelatedMediaLinks.map(l => l.Id).join(',');
    this._router.navigate(["/projects/list/" + this.type + "/details/" + this.id + "/related-news", ids]);
  }
}
