import { Component, OnInit, ViewChild } from '@angular/core';
import { Model, QuestionFile, Serializer, StylesManager, SurveyNG } from 'survey-angular';
import * as Survey from "survey-angular";
import * as surveyJSONAr from '../../../join-us-form-ar.json';
import * as surveyJSONEn from '../../../join-us-form-en.json';
import { CareersService } from 'src/app/careers/services/careers.service';
import { TranslateService } from '@ngx-translate/core';
import * as widgets from "surveyjs-widgets";
import Inputmask from 'inputmask';
import * as SurveyCore from "survey-angular";
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe, formatDate } from '@angular/common';
import { HelperService } from 'src/app/shared/helper/helper.service';
import { Keys } from '../../../../../assets/Keys';

widgets.inputmask(SurveyCore);
Survey.StylesManager.applyTheme("modern");


@Component({
  selector: 'app-join-form',
  templateUrl: './join-form.component.html',
  styleUrls: ['./join-form.component.css']
})
export class JoinFormComponent implements OnInit {

  lang: string;
  body: any;
  data: any = {};
  survey: any;
  success: boolean = false;
  extensionErrorMsg: boolean = false;
  lengthErrorMsg: boolean = false;
  uploadCvErrorMsg:boolean = false;
  errorMsg: string;
  UploadedCV: Array<File> = new Array<File>();
  OtherDocuments: Array<File> = new Array<File>();
  workExperiences: any[] = [];
  technicalSkills: any[] = [];
  certificates: any[] = [];
  formData: FormData = new FormData();
  siteKey;
  validRecaptcha;
  recaptchaErrorMsg;
  @ViewChild('cap', { static: false }) captcha;

  constructor(private _careersService: CareersService,
    private translate: TranslateService,
    private _route: ActivatedRoute,
    private datePipe: DatePipe,
    private _router: Router,
    private _helper: HelperService) { }


  ngOnInit(): void {
    this.siteKey = Keys.siteKey;
    this.lang = this.translate.currentLang;
    Serializer.addProperty("panel", "icon:text");
    Serializer.addProperty("panel", "iconText:text");
    Serializer.addProperty("question", "icon:text");
    Serializer.addProperty("question", "iconText:text");
    Serializer.addProperty("file", "isfile:text");
    Serializer.addProperty("file", "iconBtn:text");
    Serializer.addProperty("file", "iconTextBtn:text");
    Serializer.addProperty("file", "label:text");
    Serializer.addProperty("file", "desc:text");
    Serializer.addProperty("file", "desc2:text");
    Serializer.addProperty("question", "captcha:text");
    if (this.lang == "ar")
      this.survey = new Survey.Model(surveyJSONAr);
    else if (this.lang == "en")
      this.survey = new Survey.Model(surveyJSONEn);

    var mylocalization = Survey.surveyLocalization.locales["ar"];
    mylocalization.cleanCaption = "حذف الكل";
    var mylocalization = Survey.surveyLocalization.locales["en"];
    mylocalization.cleanCaption = "Delete All";

    this.survey.onLoadChoicesFromServer.add(function (sender, options) {
      //console.log(
      //  'question name: ' +
      //  options.question.name +
      //  ', choices count: ' +
      //  options.choices.length
      //);
    });
    this._route.params.subscribe(res => {
      const email = res?.['email']
      const date = res?.['date']
      if (email && date)
        this.getJoinUsData(email, date)
    })

    this.survey.onCompleting.add((survey, options) => {
      this.extensionErrorMsg = false;
      this.lengthErrorMsg = false;
      this.recaptchaErrorMsg = false;
      //check optional for upload c.v
      if(this.checkNullableObject(survey.data.UploadedCV) && (survey.data.Documents == null || survey.data.Documents.length <= 0)){
        options.allowComplete = false;
        this.uploadCvErrorMsg = true;
      }
      //check if upload other document without upload c.v
      if(this.checkNullableObject(survey.data.UploadedCV) && !this.checkNullableObject(survey.data.OtherDocuments)){
        options.allowComplete = false;
        this.uploadCvErrorMsg = true;
      }
      //chech files extensions
      if (!this.checkNullableObject(survey.data.OtherDocuments) && !this.CheckValidityFilesExtensions(survey.data.OtherDocuments)) {
        options.allowComplete = false;
        this.extensionErrorMsg = true;
      }
      if (!this.checkNullableObject(survey.data.UploadedCV) && !this.CheckValidityFilesExtensions(survey.data.UploadedCV)) {
        options.allowComplete = false;
        this.extensionErrorMsg = true;
      }
      //check files length 
      if ((!this.checkNullableObject(survey.data.OtherDocuments)) && survey.data.OtherDocuments.length > 2) {
        options.allowComplete = false;
        this.lengthErrorMsg = true;
      }
      if (this.validRecaptcha) {
        options.allowComplete = true;
      this.recaptchaErrorMsg = false;

      } else {
        options.allowComplete = false;
      this.recaptchaErrorMsg = true;

      }

    });

    this.survey.onComplete.add((survey, options) => {
      //console.log(survey.data)
      options.allowComplete = false;
      this.MappingDataToServer(survey.data);
      this.body = this.GenerateFormData(survey.data);
      this._careersService.AddJoinUs(this.body).subscribe(data => {
        this.success = true;        
        this._router.navigate['/careers-Opportunities/apply']
      },
        error => {
        if (error?.error?.ExceptionType == 'Custom') {
          let errorDetails = error?.error?.Message;
          this.errorMsg = "backEndValidation." + errorDetails;
        }
        else {
          this.errorMsg = "shared.GeneralErrMsg";
        }
        this._router.navigate(['/careers-Opportunities/apply'])
      })
    });
    this.survey.showCompletedPage = false;
    Survey.SurveyNG.render("surveyElement", { model: this.survey });
    this.survey.onAfterRenderPanel.add(this.afterRenderPanel);
    this.survey.onAfterRenderQuestion.add(this.afterRenderQuestion);
    

  }

  getJoinUsData(email, date) {
    this._careersService.GetJoinUs(email, date).subscribe(data => {
      if (data) {
        this.data = data;
        if (!this.checkNullableObject(data.WorkExperiences)){
          this.data.WorkExperiences.forEach(element => {
            Object.keys(element).forEach(function(key) {
              //console.log(key, element[key]);
              if(element[key] == " " && key !="CurrentJob" && key !="CurrentJobBool")
              {
                element[key]=null
              }
            });
          });
        }
        if (!this.checkNullableObject(data.JoinedUsAsLst)) {
          if(data.JoinedUsAsLst.length > 0)
          {
            data.JoinedUsAsLst=data.JoinedUsAsLst[0];
          }
          if (this.lang == 'ar') {
            if (data.JoinedUsAsLst == "Job")
              data.JoinedUsAsLst = "وظيفة";
            else if (data.JoinedUsAsLst == "Internship")
              data.JoinedUsAsLst = "دورة تدريبية";
            else if (data.JoinedUsAsLst == "Other")
              data.JoinedUsAsLst = "اخري";
          }
          else if (this.lang == 'en') {
            if (data.JoinedUsAsLst == "وظيفة")
              data.JoinedUsAsLst = "Job";
            else if (data.JoinedUsAsLst == "دورة تدريبية")
              data.JoinedUsAsLst = "Internship";
            else if (data.JoinedUsAsLst == "اخري")
              data.JoinedUsAsLst = "Other";
          }
        }
        this.survey.data = this.data;

        if (this.survey.getValue('LinkToPortfolio') == ("undefined") || this.survey.getValue('LinkToPortfolio') == ("null"))
          this.survey.setValue('LinkToPortfolio', " ");
      }
      //console.log(this.survey.data)
    }, error => {
      if (error?.error?.ExceptionType == 'Custom') {
        let errorDetails = error?.error?.Message;
        this.errorMsg = "backEndValidation." + errorDetails;
      }
      else {
        this.errorMsg = "shared.GeneralErrMsg";
      }
      this._router.navigate(['/careers-Opportunities/apply'])
    })
  }

  afterRenderPanel = (survey, options) => {
    if (options.panel.icon) {
      var iElement = document.createElement("i");
      iElement.className = options.panel.icon;
      iElement.innerHTML += options.panel.iconText;
      var header = options.htmlElement.querySelector("h4");
      if (!header)
        header = options.htmlElement;
      header.insertBefore(iElement, header.children[0])
    }
  }

  afterRenderQuestion = (survey, options) => {

    if (options.question.icon) {
      var iElement = document.createElement("i");
      iElement.className = options.question.icon;
      iElement.innerHTML += options.question.iconText;

      var header = options.htmlElement.querySelector("h5");
      if (!header)
        header = options.htmlElement;
      header.insertBefore(iElement, header.children[0])
    }
    else if (options.question.isfile) {
      var iElement = document.createElement("i");
      iElement.className = options.question.iconBtn;
      iElement.innerHTML += options.question.iconTextBtn;

      var header = options.htmlElement.querySelector(".sv-file__choose-btn");
      var btnLabel = options.htmlElement.querySelector(".sv-file__choose-btn span");
      btnLabel.textContent = options.question.label;

      var title = options.htmlElement.querySelector(".sv-title");

      title.className = "hideTitle";


      var descCont = options.htmlElement.querySelector(".sv-file__no-file-chosen");
      var desc1 = document.createElement("p");
      var desc2 = document.createElement("p");

      descCont.removeChild(descCont.firstChild);
      desc1.innerHTML += options.question.desc;
      desc2.innerHTML += options.question.desc2;
      descCont.appendChild(desc1);
      descCont.appendChild(desc2);

      if (!header)
        header = options.htmlElement;
      header.insertBefore(iElement, header.children[0])
    }

    if (options.question.hideNumber) {
      var checkTitle = options.htmlElement.querySelector(".sv-title");
      if (checkTitle != null)
        checkTitle.className = "hideTitle";
    }
    if (options.question.inputType == "date") {
      var checkDate = options.htmlElement.querySelector(".sv-question__content");
      checkDate.classList.add("dateIcon");
    }
  }

  MappingDataToServer(data) {
    if (data != null) {
      this.body = data;
      if (this.checkNullableObject(this.body.AcademicDegrees))
        this.body.AcademicDegrees = null;
      if (this.checkNullableObject(this.body.Certificates))
        this.body.Certificates = null;
      if (this.checkNullableObject(this.body.WorkExperiences))
        this.body.WorkExperiences = null;
      if (this.checkNullableObject(this.body.TechnicalSkills))
        this.body.TechnicalSkills = null;
    }
  }
  checkNullableObject(arr: any[]) {
    if (arr != undefined) {
      if (arr) {
        if (arr.length > 0) {
          if (Object.keys(arr[0]).length === 0)
            return true;
          else
            return false
        }
      }
    }
    return true;
  }
  closeModal() {
    this.success = false;
    this._router.navigate(['/careers-Opportunities/apply'])
  }

  GenerateFormData(data) {
    if (!this.checkNullableObject(data.WorkExperiences)){
      data.WorkExperiences.forEach(element => {
        if(element.CurrentJob){
          if(element.CurrentJobBool){
            element.CurrentJobBool = true;        
          }
        }
        else{
          if(element.CurrentJobBool){
          element.CurrentJobBool = false;
        }
      }
       
      });
    }
    this.formData.append('Address', data.Address);
    this.formData.append('BirthDate', data.BirthDate);
    this.formData.append('Email', data.Email);
    this.formData.append('FullName', data.FullName);
    this.formData.append('LinkToPortfolio', data.LinkToPortfolio);
    this.formData.append('MobileNumber', data.MobileNumber);
    this.formData.append('ProfileId', data.ProfileId);
    this.formData.append('StartDate', data.StartDate);
    this.formData.append('UploadedCVStr', data.UploadedCVStr);
    this.formData.append('OtherDocumentsStr', data.OtherDocumentsStr);

    if (!this.checkNullableObject(data.JoinedUsAsLst)) {
      this.formData.append('JoinedUsAs', data.JoinedUsAsLst);
      this.formData.append('JoinedIn', data.JoinedIn);
    }

    if (!this.checkNullableObject(data.AcademicDegrees))
      this.formData.append('AcademicDegreesStr', JSON.stringify(data.AcademicDegrees));

    if (!this.checkNullableObject(data.Certificates))
      this.formData.append('CertificatesStr', JSON.stringify(data.Certificates));

    if (!this.checkNullableObject(data.TechnicalSkills))
      this.formData.append('TechnicalSkillsStr', JSON.stringify(data.TechnicalSkills));

    if (!this.checkNullableObject(data.WorkExperiences))
      this.formData.append('WorkExperiencesStr', JSON.stringify(data.WorkExperiences));

    if (!this.checkNullableObject(data.Documents))
      this.formData.append('DocumentsStr', JSON.stringify(data.Documents));

    if (!this.checkNullableObject(data.UploadedCV))
      this._helper.appendArrayToFormData(this.formData, "UploadedCV", data.UploadedCV);

    if (!this.checkNullableObject(data.OtherDocuments))
      this._helper.appendArrayToFormData(this.formData, "OtherDocuments", data.OtherDocuments);

    return this.formData;
  }


  CheckValidityFilesExtensions(arr: any) {
    for (var item of arr) {
      var extensions = item.name.split('.', 6);
      if (extensions[extensions.length - 1] != 'pdf' && extensions[extensions.length - 1] != 'doc'
        && extensions[extensions.length - 1] != 'docx')
        return false;
    }
    return true;
  }
  showResponse(response) {
    this.recaptchaErrorMsg = false;
    //call to a backend to verify against recaptcha with private key
    if (response?.response) {
      this.validRecaptcha = true;
    }
  }
  Complete() {
    this.survey.completeLastPage();
    var errors = [];
    var qErrors = [];
    var errorsDynamicPanel = [];
    for (var i = 0; i < this.survey.visiblePages.length; i++) {
      var page = this.survey.visiblePages[i];
      var questions = page.questions;
      for (var j = 0; j < questions.length; j++) {
        if (questions[j].items) {
          if (questions[j].items.length > 0) {
            questions[j].items[0].textPreProcessor.panel.elements.forEach(
              (element) => {
                if (element.errors.length > 0) {
                  errorsDynamicPanel.push(element.errors[0]);
                }
              }
            );
          }
        }
        questions[j].hasErrors(true);

        qErrors = questions[j].errors.concat(errorsDynamicPanel);
        for (var k = 0; k < qErrors.length; k++) {
          errors.push({
            page: page,
            question: questions[j],
            error: qErrors[k],
          });
        }
      }
    }
    if (errors.length > 0) {
      var error = errors[0];
      var name = error?.error?.errorOwner?.name;
      var id = error?.error?.errorOwner?.id;
      if (name == 'BirthDate') {
        let element: HTMLElement = document.getElementsByClassName(
          'ui-datepicker-trigger'
        )[0] as HTMLElement;
        element.click();
      } else if (name == 'StartDate' && id != 'sq_141') {
        let element: HTMLElement = document.getElementsByClassName(
          'ui-datepicker-trigger'
        )[1] as HTMLElement;
        element.click();
      } else if (name == 'DegreeDate') {
        let element: HTMLElement = document.getElementsByClassName(
          'ui-datepicker-trigger'
        )[2] as HTMLElement;
        element.click();
      } else if (name == 'CertifiedDate') {
        let element: HTMLElement = document.getElementsByClassName(
          'ui-datepicker-trigger'
        )[3] as HTMLElement;
        element.click();
      } else if (name == 'StartDate' && id == 'sq_141') {
        //console.log(
        //  'error?.error?.errorOwner?.id',
        //  error?.error?.errorOwner?.id
        //);
        let element: HTMLElement = document.getElementsByClassName(
          'ui-datepicker-trigger'
        )[4] as HTMLElement;
        element.click();
      } else if (name == 'EndDate') {
        let element: HTMLElement = document.getElementsByClassName(
          'ui-datepicker-trigger'
        )[5] as HTMLElement;
        element.click();
      }
    }
  }
}
