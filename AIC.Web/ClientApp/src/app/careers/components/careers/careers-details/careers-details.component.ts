import { Component, OnInit, ViewChild } from '@angular/core';
import { CareersService } from '../../../services/careers.service';
import { ActivatedRoute, Router } from '@angular/router';
import * as moment from 'moment';
import { FormatDatePipe } from '../../../../shared/pipes/formate-date.pipe';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BreadcrumbService } from 'xng-breadcrumb';
import { Keys } from '../../../../../assets/Keys';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-careers-details',
  templateUrl: './careers-details.component.html',
  styleUrls: ['./careers-details.component.css','../../../../shared/page-loader/page-loader.component.css'],
  providers: [FormatDatePipe]
})
export class CareersDetailsComponent implements OnInit {
  emailDialog = false;
  id: number;
  detailsData: any = {};
  dateExpired: boolean = false;
  expDate;
  today: Date;
  loading: boolean =false;
  applyForm: any;
  body: {};
  success: boolean = false;
  siteKey;
  validRecaptcha = false;
  recaptchaErrorMsg;
  @ViewChild('cap', { static: false }) captcha;
  lang;

  constructor(private careersService: CareersService,
    public router: Router, private route: ActivatedRoute,
    private datePipe: FormatDatePipe,
    private translateService: TranslateService,
    private _careersService: CareersService, private breadcrumbService: BreadcrumbService,
    private appService: AppService) { }

  ngOnInit(): void {
    this.handleBreadCrumbesTitle();
    this.lang = this.translateService.currentLang;
    this.siteKey = Keys.siteKey;
    this.ReadQueryStringParameters();
    this.initForm();
  }
  initForm() {
    this.applyForm = new FormGroup({
      "Email": new FormControl('',Validators.required)
    });
  }
  ReadQueryStringParameters() {
    this.route.paramMap
      .subscribe(params => {
        this.id = +params.get('id') || 0;
        this.GetDetails();
      });
  }
  GetDetails() {
    this.careersService.GetCareerById(this.id).subscribe(data => {
      this.detailsData = data;
      this.breadcrumbService.set('@careerTitle', this.lang == 'en' ? data.Title : data.TitleAr);
      this.expDate = new Date(data.VacancyExpiryDate);
      this.today = new Date();
      if (this.today > this.expDate) {
        this.dateExpired = true;
      }
      setTimeout(() => {
        this.loading = false;
      }, 1000);
    })
  }
  onClick(){
    //console.log(this.applyForm.valid)
    this.recaptchaErrorMsg = false;
    if(this.applyForm.valid && this.validRecaptcha){
      this.body ={
        "Email":this.applyForm.value['Email'],
        "VacancyId":this.id || 0
      }
      this._careersService.AddUserProfileVacancy(this.body).subscribe(data => {
        if (data) {
          this.success = true;
          this.applyForm.reset();
          this.validRecaptcha = false;
          this.captcha.reset();
        }
        
      }, err => {
        //console.log(err);
        this.validRecaptcha = false;
        this.captcha.reset();
      })
    }
    else if (this.validRecaptcha == false) {
      this.recaptchaErrorMsg = true;
      if (this.applyForm.valid == false)
        this.applyForm.markAllAsTouched();
    }
    else {
      if (this.validRecaptcha)
        this.recaptchaErrorMsg = false;
      this.applyForm.markAllAsTouched();
    }
  }
  closeModal() {

    this.success = false;
    this.recaptchaErrorMsg = false;
    this.validRecaptcha = false;
    this.emailDialog = false;
    this.captcha.reset();
    this.applyForm.reset();
  }

  showResponse(response) {
    //call to a backend to verify against recaptcha with private key
    this._careersService.ValidateRecaptcha({ UserResponse: response?.response }).subscribe(data => {
      this.validRecaptcha = data;
      this.recaptchaErrorMsg = false;
    });
  }
  onExpire() {
    this.validRecaptcha = false;
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.Careers","CareersTitle");
  }
}
