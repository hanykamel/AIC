import { Router } from '@angular/router';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import {
  Model,
  QuestionFile,
  Serializer,
  StylesManager,
  SurveyNG,
} from 'survey-angular';
import * as Survey from 'survey-angular';
import * as ContactUSJSONAR from '../../contact-us-form-ar.json';
import * as ContactUSJSONEN from '../../contact-us-form-en.json';
import { ContactUsService } from '../../services/contact-us.service';
import { TranslateService } from '@ngx-translate/core';
import {} from 'googlemaps';
import { AfterViewInit } from '@angular/core';
import { ClipboardService } from 'ngx-clipboard';
import { Keys } from '../../../../assets/Keys';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrls: ['./contact-us.component.css'],
})
export class ContactUsComponent implements OnInit , AfterViewInit {
  @ViewChild('map') mapElement: any;
  map: google.maps.Map;

  locationsDialog = false;
  offices: [];
  socialMediaLinks: [];
  headOffice;
  lang;
  mapProperties;
  survey: any;
  validRecaptcha;
  recaptchaErrorMsg;
  showComplete;
  showMsg: boolean = false;
  siteKey;

  constructor(
    private contactUsService: ContactUsService,
    private translateService: TranslateService,
    private clipboardService: ClipboardService,
  ) {
    
  }
  ngAfterViewInit(): void {
      
 
    }
  @ViewChild('cap', { static: false }) captcha;
  @ViewChild('searchterms') searchterms: ElementRef;
  ngOnInit(): void {

    this.siteKey = Keys.siteKey;
    this.lang = this.translateService.currentLang;
    this.socialMediaLinks = [];
    this.GetOffices();
    this.GetSocialMedialLinks();
    
    Survey.StylesManager.applyTheme('modern');
    
    Serializer.addProperty('question', 'captcha:text');
    
    if (this.lang == 'ar') this.survey = new Survey.Model(ContactUSJSONAR);
    else if (this.lang == 'en') this.survey = new Survey.Model(ContactUSJSONEN);

    this.survey.onCompleting.add((survey, options) => {
      this.recaptchaErrorMsg = false;
      if (this.validRecaptcha) {
        options.allowComplete = true;
      this.recaptchaErrorMsg = false;

      } else {
        options.allowComplete = false;
      this.recaptchaErrorMsg = true;

      }
    });
    this.survey.onComplete.add((survey) => {
      this.CreateNewRequest(survey.data);
    });
    Survey.SurveyNG.render('surveyElement', { model: this.survey });

    //survey.onAfterRenderPanel.add(this.afterRenderPanel);
    this.survey.onAfterRenderQuestion.add(this.afterRenderQuestion);
    
    this.survey.onLoadChoicesFromServer.add((survey, options) => {
      let question = options.question.name;
      if (question == 'CountryId') {
        let selectedCountryId = options.choices.find(c => c.locTextValue.renderedText?.toLowerCase() == 'egypt' || c.locTextValue.renderedText?.toLowerCase() == 'مصر').value;
        if (selectedCountryId)
        options.question.value = selectedCountryId;

      } else if (question == 'CityId') {
        let selectedCityId = options.choices.find(c => c.locTextValue.renderedText?.toLowerCase() == 'cairo' || c.locTextValue.renderedText?.toLowerCase() == 'القاهرة').value;
        if (selectedCityId)
          options.question.value = selectedCityId;
      }
    });
    function ShowCities(params) {
      const question = this.survey.getQuestionByName(params[0]);
      if (question?.choicesFromUrl) {
        let defaultCountry = question?.choicesFromUrl.find(c => c.locTextValue.renderedText?.toLowerCase() == 'egypt' || c.locTextValue.renderedText?.toLowerCase() == 'مصر');
        return defaultCountry.value == question.value;
      }
      return false;
    }
    Survey.FunctionFactory.Instance.register('ShowCities', ShowCities);

    this.survey.showCompletedPage = false;
    this.survey.focusFirstQuestionAutomatic = false
    this.survey.focusOnFirstError = false;
  }
  Complete(){
    this.survey.completeLastPage()
  }
  
  CreateNewRequest(data) {
    let SelectedTypeId = this.GetSelectedTypeId(data.Type);
    let body = {
      ...data,
      TypeId: SelectedTypeId,
    };
    body.CityId = body.CityId ? body.CityId : 0;
    this.contactUsService.CreateContactUsForm(body).subscribe((data) => {
      this.captcha.reset();
      this.survey.clear();
      this.showComplete = true;
    });
  }

  GetSelectedTypeId(type) {
    if (type == 'Careers' || type == 'الوظائف') return 1;
    else if (type == 'Complaints' || type == 'الشكاوي') return 2;
    else if (type == 'Feedbacks' || type == 'التقييمات') return 3;
    else if (type == 'HPC' || type == 'الحوسبة فائقة القدرة') return 4;
    else if (type == 'Human Resources' || type == 'الموارد البشرية') return 5;
    else if (type == 'Inquiries' || type == 'الاستعلامات') return 6;
    else if (type == 'R&D' || type == 'البحث والتطوير') return 7;
    else if (type == 'Suggestions' || type == 'الاقتراحات') return 8;
  }

  GetOffices() {
    this.contactUsService.GetContactUs().subscribe((data) => {
      this.offices = data.Items;
      this.headOffice = data.Items.find((o) => o.IsHeadOffice == true);
      this.searchterms.nativeElement.focus({
        preventScroll: true
      })
      /*//console.log(this.offices);
      this.mapProperties = {
        center: new google.maps.LatLng(35.2271, -80.8431),
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
      };
      this.map = new google.maps.Map(this.mapElement.nativeElement, this.mapProperties);*/
    });
  }

  GetSocialMedialLinks() {
    this.contactUsService.GetSocialMediaLinks().subscribe((data) => {
      this.socialMediaLinks = data.Items;
    });
  }

  afterRenderPanel = (survey, options) => {};

  afterRenderQuestion = (survey, options) => {
    //window.scrollTo(0, 0);

    /*   window.scrollTo(0, 0);*/
    //if (options.question.captcha) {
    //  //console.log(options.question.captcha)
    //  var tet = options.htmlElement.querySelector(".sv-question");
    //  var te = document.createElement("p");
    //  tet.removeChild(tet.firstChild);
    //  te.innerHTML += options.question.captcha;;
    //  tet.appendChild(te);
    //}
  };

  showResponse(response) {
    this.recaptchaErrorMsg = false;

    //call to a backend to verify against recaptcha with private key
    //if (response?.response) {
      //this.validRecaptcha = true;
    this.contactUsService.ValidateRecaptcha({ UserResponse: response?.response }).subscribe(data => {
        this.validRecaptcha = data;
      });
    //}
  }
  onExpire() {
    this.validRecaptcha = false;
  }
  index: number;
  CopyText(text, i) {
    this.index = i;
    this.showMsg = true;
    /*  navigator.clipboard.writeText(text);*/
    setTimeout(
      function () {
        this.showMsg = false;
      }.bind(this),
      500
    );
  }
}
