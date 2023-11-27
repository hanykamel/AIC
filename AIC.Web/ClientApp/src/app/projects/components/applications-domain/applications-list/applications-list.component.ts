import { Component, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ProjectsService } from '../../../services/projects.service';

@Component({
  selector: 'app-applications-list',
  templateUrl: './applications-list.component.html',
  styleUrls: ['./applications-list.component.css']
})
export class ApplicationsListComponent implements OnInit {
  @Input() Projects;
  applicationsDomain;
  lang;
  type;
  domainType;

  constructor(private projectsService: ProjectsService, private translate: TranslateService
    , private _router: Router, private _route: ActivatedRoute) { }

  ngOnInit(): void {
    this._route.params.subscribe(res => {
      this.type = res?.['type'] ? res?.['type'] : null;

    });

    this.lang = this.translate.currentLang;
    this.projectsService.ListApplicationsDomain({}).subscribe(data => {
      this.applicationsDomain = data.Items;
      this.applicationsDomain.forEach(d => {
        let domainProjects;
        if (this.lang == 'ar') {
          domainProjects = this.Projects.filter(p => p.ApplicationDomain == d.TitleAr);
        } else {
          domainProjects = this.Projects.filter(p => p.ApplicationDomain == d.Title);
        }
        d.Children = domainProjects;
      });
      //console.log(this.applicationsDomain);
    });
  }

  getItemsOfDomain(domain) {
    this.domainType = domain;
    return this.Projects.filter(p => p.ApplicationDomain == domain);
  }

  onClickDetails = (id) => this._router.navigate(["/projects/list/" + this.type + "/details", id]);
}
