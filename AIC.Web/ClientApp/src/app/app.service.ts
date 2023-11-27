import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BreadcrumbService } from 'xng-breadcrumb';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  public pageLoader = new BehaviorSubject({ show: false });
  public headertrigger = new BehaviorSubject<boolean>(false);
  public matSpinner: any;
  public showSpinner: boolean = true;
  private messageSource = new BehaviorSubject({});
  menuContent = this.messageSource.asObservable();
  
  data: any[] = [];
  public observSocialMediaShareIcon: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  currentLang = localStorage.getItem('oldLanguage') || 'en';

  constructor(private http: HttpClient,
    private breadcrumbService: BreadcrumbService,
    private translate: TranslateService) {
    let mainMenuUrl = "/api/home/GetMainMenu";
    this.GetContentData(mainMenuUrl).subscribe(data => {

      this.ContentData(this.MapMainMenu(data))
    }
    )
  }

  GetContentData(url: string) {

    return this.http.get<any>(url);
  }


  GetList(url: string, body: {}, pageInfo: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'pagingInfo': pageInfo
      })
    };
    return this.http.post<any>(url, body, httpOptions);
  }
  GetPaginationList(url: string, body: {}, options: {} = {}) {
    return this.http.post<any>(url, body, options);
  }
  GetSocialMedia() {
    return this.http.get<any>("api/ContactUs/GetSocialMedia");
  }
  ParsingDataToSocialMediaComponent(data: any) {
    this.data = [];
    if (data) {
      data.forEach((element) => {
        var object: any = new Object();
        object.name = element.Title
        object.url = element.Url;
        object.img = element.Logo;
        this.data.push(object);
      })
    }
    return this.data;
  }
  ParsingDataToHomeSocialMediaComponent(data: any) {
    this.data = [];
    if (data) {
      data.forEach((element) => {
        var object: any = new Object();
        object.name = element.Title
        object.url = element.Url;
        object.img = element.HomeLogo;
        this.data.push(object);
      })
    }
    return this.data;
  }

  ContentData(message: any) {
    this.messageSource.next(message)
  }
  MapMainMenu(data) {
    let sections = data['Sections'] ?? [];
    let Urls = data['Urls'] ?? [];
    sections.forEach(s => {
      s.Url = s.Url ? Urls.find(u => u.Id == s.Url)?.Url : '';
    });
    let parents = sections.filter(s => s.Parent == "").sort((a, b) => { return a.Order - b.Order });
    //console.log(sections)
    //console.log(parents)
    parents.forEach(p => {
      p.Children = sections.filter(s => s.Parent == p.Title).sort((a, b) => { return a.Order - b.Order });
      p.Children.forEach(c => {
        c.Parent = this.currentLang == 'ar' ? p.TitleAr : p.Title
        c.ParentEN =  p.Title
        c.ParentAR =  p.TitleAr 
      });
    });
    return { Menu: parents };
  }

  private previousUrl: BehaviorSubject<string> = new BehaviorSubject<string>(null);
  public previousUrl$: Observable<string> = this.previousUrl.asObservable();


  setPreviousUrl(previousUrl: string) {
    this.previousUrl.next(previousUrl);
  }

  handleBreadCrumbesTitle(pageTitle , breadcrumbTitle) {
    var title = '';
    var localizedPageTitle = this.translate.instant(pageTitle);
    title = (this.currentLang == 'ar' ? localStorage.getItem('breadCrumbesAR') : localStorage.getItem('breadCrumbesEN')) + "  >  " + localizedPageTitle;
    this.breadcrumbService.set(`@${breadcrumbTitle}`, title);
  }
}
