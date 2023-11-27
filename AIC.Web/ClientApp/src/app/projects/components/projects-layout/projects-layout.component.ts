import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ProjectsService } from '../../services/projects.service';

@Component({
  selector: 'app-projects-layout',
  templateUrl: './projects-layout.component.html',
  styleUrls: ['./projects-layout.component.css']
})
export class ProjectsLayoutComponent implements OnInit {
  projects;
  selectedIndex = 0;
  lang;
  constructor(private projectsService: ProjectsService, private _route: ActivatedRoute, private translate: TranslateService, private _router: Router) { }

  ngOnInit(): void {
    this.projectsService.ListProjects({}).subscribe(data => {
      this.projects = data.Items
    });
    this._route.params.subscribe(res => {
      let type = res?.['type']
      if (type) {
        switch (type) {
          case 'technology':
            this.selectedIndex = 1;
            break;
          case 'application':
            this.selectedIndex = 0;
            break;
          default:
        }
      }

    });
  }
  changeRoute(event) {
    if (event?.index == 0 || event?.index == 1) {
      switch (event?.index) {
        case 0:
          this._router.navigate(['/projects/list/application']);
          break;
        case 1:
          this._router.navigate(['/projects/list/technology']);
          break;
        default:
      }
    }
  }
}
