import { Component, OnInit, ViewChild } from '@angular/core';
import { CareersService } from '../../../services/careers.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BreadcrumbService } from 'xng-breadcrumb';
import { Keys } from '../../../../../assets/Keys';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-internships-details',
  templateUrl: './internships-details.component.html',
  styleUrls: ['./internships-details.component.css', '../../careers/careers-details/careers-details.component.css']
})
export class InternshipsDetailsComponent implements OnInit {
  emailDialog = false;
  id: number;
  detailsData: any = {};
  dateExpired: boolean = false;
  expDate;
  today: Date;
  loading: boolean = false;
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
      "Email": new FormControl('', Validators.required)
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
    this.careersService.GetInternById(this.id).subscribe(data => {
      this.detailsData = data;
      this.breadcrumbService.set('@internTitle', this.lang == 'en' ? data.Title : data.TitleAr);
      this.expDate = new Date(data.ExpiryDate);
      this.today = new Date();
      if (this.today > this.expDate) {
        this.dateExpired = true;
      }
      setTimeout(() => {
        this.loading = false;
      }, 1000);
    })
  }
  onClick() {
    //console.log(this.applyForm.valid)
    this.recaptchaErrorMsg = false;
    if (this.applyForm.valid && this.validRecaptcha) {
      this.body = {
        "Email": this.applyForm.value['Email'],
        "InternShipId": this.id || 0
      }
      this._careersService.AddUserProfileInternship(this.body).subscribe(data => {
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
    this.recaptchaErrorMsg = false;
    this.success = false;
    this.validRecaptcha = false;
    this.captcha.reset();
    this.emailDialog = false;
    this.applyForm.reset();
  }
  onExpire() {
    this.validRecaptcha = false;
  }

  showResponse(response) {
    //call to a backend to verify against recaptcha with private key
    this._careersService.ValidateRecaptcha({ UserResponse: response?.response }).subscribe(data => {
      this.validRecaptcha = data;
      this.recaptchaErrorMsg = false;
    });
  }
  handleBreadCrumbesTitle() {
    this.appService.handleBreadCrumbesTitle("PagesTitle.Internships","InternshipsTitle");
  }
}
