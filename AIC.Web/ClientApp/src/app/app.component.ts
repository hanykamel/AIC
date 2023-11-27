import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { AppService } from './app.service';
import { TranslateService } from '@ngx-translate/core';
import * as moment from 'moment';
import { Title } from '@angular/platform-browser';
import { filter } from 'rxjs/operators';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = '';
  lang;
  @ViewChild('loginInput') loginInput: ElementRef;
  constructor(private router: Router,
    public appService: AppService, private translateService: TranslateService,
    private titleService: Title, private activatedRoute: ActivatedRoute) {
    this.translateService.get("PagesTitle.AIC").subscribe(data => {
      this.titleService.setTitle(data);
      this.title = data;
    });

    this.translateService.use(localStorage.getItem('oldLanguage'));
    moment.locale(localStorage.getItem('oldLanguage'));

    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
      
        var route = val.url.split('/', 6);
        var res = route[route.length - 1];
        if(route[route.length - 2] == "video-details" ||
         route[route.length - 2] == "careers-details" || route[route.length - 2] == "internships-details")
         this.appService.observSocialMediaShareIcon.next(true);
        else if (route[1] == "careers-Opportunities")
          this.appService.observSocialMediaShareIcon.next(false);
        else if ((res[0] == res.charAt(0)?.toUpperCase()) || (res.includes('%'))) { // is encoded ID for details page
          this.appService.observSocialMediaShareIcon.next(true);
        } 
        // else if (route[route.length - 2] == "video-details") {
        //   this.appService.observSocialMediaShareIcon.next(true);
        // }
        else { // Not Details page
          this.appService.observSocialMediaShareIcon.next(false);
        }
      }
    });

    // open this to fix accessability bug when navigate , comment it if you want to scroll with keyboard insisde the app
  //   window.addEventListener("keydown", function(e) {
  //     if(["Space","ArrowUp","ArrowDown","ArrowLeft","ArrowRight"].indexOf(e.code) > -1) {
  //         e.preventDefault();
  //     }
  // }, false);
  
  }

  previousUrl: string = null;
  currentUrl: string = null;

  ngOnInit(): void {
    this.lang = this.translateService.currentLang;
  this.router.events.pipe(
    filter(event => event instanceof NavigationEnd),
  ).subscribe((event: NavigationEnd) => {
    this.previousUrl = this.currentUrl;
    this.currentUrl = event.url;
    this.appService.setPreviousUrl(this.previousUrl);
    const rt = this.getChild(this.activatedRoute);
    rt.data.subscribe(data => {
      if (data.title != null) {
        this.translateService.get(data.title).subscribe(data => {
          this.titleService.setTitle(data);
          this.loginInput.nativeElement.focus()

        });
      }
    });
  });
  }
  getChild(activatedRoute: ActivatedRoute) {
    if (activatedRoute.firstChild) {
      return this.getChild(activatedRoute.firstChild);
    } else {
      return activatedRoute;
    }
  }

}
