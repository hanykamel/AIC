import { Component, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute} from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ProjectsService } from '../../../services/projects.service';

@Component({
  selector: 'app-technology-list',
  templateUrl: './technology-list.component.html',
  styleUrls: ['./technology-list.component.css']
})
export class TechnologyListComponent implements OnInit {
  @Input() Projects;
  technologyDomain;
  lang;
  type;

  constructor(private projectsService: ProjectsService, private translate: TranslateService,
    private _router: Router, private _route: ActivatedRoute) { }

  ngOnInit(): void {
    this._route.params.subscribe(res => {
      this.type = res?.['type'] ? res?.['type'] : null;
    });

    this.lang = this.translate.currentLang;
    this.projectsService.ListTechnologyDomain({}).subscribe(data => {
      this.technologyDomain = data.Items;
      this.technologyDomain.forEach(d => {
        let domainProjects;
        if (this.lang == 'ar') {
          domainProjects = this.Projects.filter(p => p.TechnologyDomain == d.TitleAr);
        } else {
          domainProjects = this.Projects.filter(p => p.TechnologyDomain == d.Title);
        }
        d.Children = domainProjects;
      });
    });
  }
  getItemsOfDomain(domain) {
    return this.Projects.filter(p => p.TechnologyDomain == domain);
  }
  onClickDetails = (id) => this._router.navigate(["/projects/list/" + this.type + "/details", id]);
}
