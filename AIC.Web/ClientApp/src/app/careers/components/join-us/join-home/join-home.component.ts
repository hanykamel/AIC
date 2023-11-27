import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { CareersService } from 'src/app/careers/services/careers.service';
import { Keys } from '../../../../../assets/Keys';

@Component({
  selector: 'app-join-home',
  templateUrl: './join-home.component.html',
  styleUrls: ['./join-home.component.css']
})
export class JoinHomeComponent implements OnInit {
  emailDialog = false;
  applyForm: any;
  body: {};
  success: boolean = false;
  siteKey;
  validRecaptcha = false;
  recaptchaErrorMsg;
  @ViewChild('cap', { static: false }) captcha;
  lang: string;

  constructor(private _careersService: CareersService, private translate: TranslateService) { }

  ngOnInit(): void {
    this.siteKey = Keys.siteKey;
    this.lang = this.translate.currentLang;
    this.initForm();
  }
  initForm() {
    this.applyForm = new FormGroup({
      "Email": new FormControl('',Validators.required)
    });
  }
  onClick(){
    //console.log(this.applyForm.valid)
    if (this.applyForm.valid && this.validRecaptcha) {
      this.recaptchaErrorMsg = false;
      this.body = {
        "Email": this.applyForm.value['Email']
      }
      this._careersService.AddUserProfile(this.body).subscribe(data => {
        if (data) {
          this.applyForm.reset();
          this.validRecaptcha = false;
          this.captcha.reset();
          this.success = true;
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
    this.applyForm.reset();
    this.validRecaptcha = false;
    this.captcha.reset();
    this.emailDialog = false;

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
}
