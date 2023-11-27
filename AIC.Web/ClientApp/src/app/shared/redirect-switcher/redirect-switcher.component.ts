import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from '../../app.service';
import * as moment from 'moment';

@Component({
  selector: 'app-redirect-switcher',
  templateUrl: './redirect-switcher.component.html',
  styleUrls: ['./redirect-switcher.component.css']
})
export class RedirectSwitcherComponent implements OnInit {
  url: any;
  lang: any;

  constructor(private route: ActivatedRoute, private router: Router, public translate: TranslateService,
    private appService: AppService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      debugger;
      this.appService.menuContent = null;
      this.lang = params.lang || "ar";
      localStorage.setItem('oldLanguage', this.lang);
      this.translate.use(this.lang);
      this.translate.currentLang = this.lang;
      moment.locale(localStorage.getItem('oldLanguage'));
      this.url = params.url;
      this.router.navigate([this.url]);
      document.body.classList.remove(this.lang);
      document.body.classList.add(this.lang);
      //console.log(params);
    });
  }

}
