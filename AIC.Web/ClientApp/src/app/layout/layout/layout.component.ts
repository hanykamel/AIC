import { Location } from '@angular/common';
import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { MessageService } from 'primeng/api';
import { AppService } from '../../app.service';
import { CustomMessageService } from '../../services/custom-message.service';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit ,AfterViewInit,OnDestroy{

  showSocialMediaShare: boolean = false;
  showSocialMediaContent: boolean = false;

  socData: any[] = [];

  backEndValidation = null;
  summary: any;
  validation: any;
  skipLinkPath: any = '';
  skipContent :boolean=false;
  lang;
  buttonToggle: boolean = false;
  constructor(public router: Router,
    private appService: AppService, public translate: TranslateService,
    private _messageService: MessageService, private _customMessageService: CustomMessageService ,
    
    public location:Location ) {
   
      router.events.pipe(filter(event => event instanceof NavigationEnd)).subscribe(() => {
        this.skipContent=false;
     });
    }
  ngAfterViewInit(): void {
      if (location.pathname == '/home' || this.router.url == '/home#banner'){
        document.getElementById('divHeight').setAttribute("role", "none");
        // document.getElementById('banner').setAttribute("role", "main");
        document.getElementById('banner').setAttribute("tabindex", "0");
      }
      else{
        document.getElementById('divHeight').setAttribute("role", "main"); 
        document.getElementById('divHeight').setAttribute("tabindex", "0"); 
      }
   //console.log('5');
   document.getElementById('banner')?.scrollIntoView({behavior: 'smooth'});
   document.getElementById('banner').focus();
   document.getElementById('banner').setAttribute("tabindex", "0");
  //  document.querySelector('#banner').scrollIntoView();

  }
  public navClick(elementId: string): void {
    if (location.pathname == '/home' || this.router.url == '/home#banner'){
      document.getElementById('divHeight').setAttribute("role", "none");
      // document.getElementById('banner').setAttribute("role", "main");
      document.getElementById('banner').setAttribute("tabindex", "0");
    }
    else{
      document.getElementById('divHeight').setAttribute("role", "main"); 
      document.getElementById('divHeight').setAttribute("tabindex", "0"); 
    }
    this.appService.headertrigger.next(true);
    this.skipContent=true;
    document.getElementById(elementId)?.scrollIntoView({behavior: 'smooth'});
    document.getElementById(elementId).focus();
    document.getElementById(elementId).setAttribute("tabindex", "0");
  }
 
  ngOnInit(): void {
    this.lang = this.translate.currentLang;
    this.socData = [];
    this.observeSocialMediaShareIcon();
    this._customMessageService.message.subscribe(m => {
      this.translate.get(['backEndValidation', 'summary', 'validation']).subscribe((res:any) => {
        this.backEndValidation = res.backEndValidation;
        this.summary = res.summary;
        this.validation = res.validation;
      });
      if (m) {
        //console.log(m);
        const msg = this.backEndValidation[m?.detail] ?? this.validation[m?.detail] ?? m?.detail
        const summary = this.summary[m?.summary] ?? m?.summary;
        this._messageService.add({ key: 'serverMsgs', severity: m?.severity, summary: summary, detail: msg + (m?.constantmsg ?? "") });
        if (m?.detail == 'ItemNotExist')
          this.router.navigate(['/']);
      }
    });
    // this.translate.get(['backEndValidation', 'summary', 'validation']).subscribe((res:any) => {
    //   this.backEndValidation = res.backEndValidation;
    //   this.summary = res.summary;
    //   this.validation = res.validation;
    //   this._customMessageService.message.subscribe(m => {
    //     if (m) {
    //       const msg = this.backEndValidation[m?.detail] ?? this.validation[m?.detail] ?? m?.detail
    //       const summary = this.summary[m?.summary] ?? m?.summary;
    //       this._messageService.add({ key: 'serverMsgs', severity: m?.severity, summary: summary, detail: msg + (m?.constantmsg ?? "") });
    //       if (m?.detail == 'ItemNotExist')
    //         this.router.navigate(['/']);
    //     }
    //   })
    // });
    this.appService.headertrigger.subscribe(skip=>{
      if(skip == false)
      {
        this.skipContent = false;
      }
    })
    //console.log('asma')

    document.getElementById('divHeight').focus();
    this.GetSocialMedia();
  }

  GetSocialMedia() {
    this.appService.GetSocialMedia().subscribe(data => {
      if (this.router.url == '/home')
        this.socData = this.appService.ParsingDataToHomeSocialMediaComponent(data.Items);
      else
        {
          this.socData = this.appService.ParsingDataToSocialMediaComponent(data.Items);
          this.showSocialMediaContent= false;
        }
    }, err => {
    })
  }
  observeSocialMediaShareIcon() {
    this.appService.observSocialMediaShareIcon.subscribe(data => {
      this.showSocialMediaShare = data;
    })
  }

  onActivate(event) {
    window.scroll(0, 0);
  }
  ngOnDestroy(): void {
    this.skipContent=false;

  }

}
