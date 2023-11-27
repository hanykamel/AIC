import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { AppService } from 'src/app/app.service';
import { HomeService } from '../home.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css'],
})
export class ProjectsComponent implements OnInit {
  data: any;
  @Output() dataEmitter = new EventEmitter();
  constructor(
    private translateService: TranslateService,
    private homeService: HomeService,
    private appService: AppService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    //this.appService.pageLoader.next({show:true});
    this.GetProjects();
  }
  GetProjects() {
    this.homeService.GetProjects().subscribe(
      (data) => {
        this.data = data.Items;
        //console.log(this.data);
        this.dataEmitter.emit(this.data);
        //this.appService.pageLoader.next({show:false});
      },
      (err) => {
        //this.appService.pageLoader.next({show:false});
      }
    );
  }

  onClick(project) {
    //console.log(project.Id);
    if (project.ApplicationDomain != null && project.ApplicationDomain !='')
      this._router.navigate(['/projects/list/application/details/' + project.Id]);
    else
      this._router.navigate(['/projects/list/technology/details/' + project.Id]);
  }
}
