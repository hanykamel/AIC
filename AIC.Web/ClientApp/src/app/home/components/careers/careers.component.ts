import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AppService } from 'src/app/app.service';
import { HomeService } from '../home.service';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-careers',
  templateUrl: './careers.component.html',
  styleUrls: ['./careers.component.css'],
})
export class CareersComponent implements OnInit {
  careersSections: any;
  careers: any;
  @Output() dataEmitter = new EventEmitter();
  lang;

  constructor(
    private homeService: HomeService,
    public appService: AppService,
    private _router: Router,
    private translateService: TranslateService
  ) {}

  ngOnInit(): void {
    this.lang = this.translateService.currentLang;
    this.GetHomeCareersSections();
    this.GetCareers();
  }
  GetCareers() {
    this.homeService.GetHomePageSections().subscribe(
      (data) => {
        this.careersSections = data?.FeaturedCareersBrief;
        this.dataEmitter.emit(this.careersSections);
      },
      (err) => {}
    );
  }
  GetHomeCareersSections() {
    this.homeService.GetCarrers().subscribe(
      (data) => {
        this.careers = data.Items;
      },
      (err) => {}
    );
  }

  getCareerById(id) {
    //console.log(id);
    this._router.navigate([
      '/careers-Opportunities/careers-list/careers-details/' + id,
    ]);
  }
}
