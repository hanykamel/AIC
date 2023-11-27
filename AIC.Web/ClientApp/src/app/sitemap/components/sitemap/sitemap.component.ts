import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from '../../../app.service';
import { SitemapService } from '../../services/sitemap.service';

@Component({
  selector: 'app-sitemap',
  templateUrl: './sitemap.component.html',
  styleUrls: ['./sitemap.component.css']
})
export class SitemapComponent implements OnInit {
  title = "Site Map";
  siteMap:any;
  lang;

  constructor(private sitemapService: SitemapService, private translate: TranslateService, private appService: AppService) { }

  ngOnInit(): void {
    this.lang = this.translate.currentLang;
    this.appService.menuContent.subscribe(data => {
      this.siteMap = data['Menu'] ?? [];
    });
  }

}
