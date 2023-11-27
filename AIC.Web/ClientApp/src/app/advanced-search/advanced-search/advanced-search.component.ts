import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { GenericContentTypes } from 'src/app/core/Enums/genericContentTypes';
import { AdvancedSearchService } from '../advanced-search.service';
import { CoreService } from 'src/app/core/services/core.service';
import moment from 'moment';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-advanced-search',
  templateUrl: './advanced-search.component.html',
  styleUrls: ['./advanced-search.component.css']
})
export class AdvancedSearchComponent implements OnInit {
  startDate: Date;
  endDate: Date;
  selected;
  categories;
  key;
  cat;
  searchKey;
  errorMsg;
  results;
  noDataFound;
  searchMinLength;
  lang: string;
  counter: number = 0;
  constructor(private translateService: TranslateService, private route: ActivatedRoute, private advancedSearchService: AdvancedSearchService,
    private appService: AppService,
    private router: Router, private coreService: CoreService,) { }

  ngOnInit(): void {
    this.categories = [];
    this.results = [];
    this.lang = this.translateService.currentLang;
    this.translateService.get("Search").subscribe(data => {
      this.categories.push({ name: data.Projects, value: 'Projects' });
      this.categories.push({ name: data.HPCProjects, value: 'HPCProjects' });
      this.categories.push({ name: data.News, value: 'News' });
      this.categories.push({ name: data.Events, value: 'Events' });
      this.categories.push({ name: data.Careers, value: 'Careers' });
      this.categories.push({ name: data.Internships, value: 'Internships' });
      this.categories.push({ name: data.WhitePapers, value: 'WhitePapers' });
      this.categories.push({ name: data.BestPractices, value: 'BestPractices' });
      this.categories.push({ name: data.PhotoGallery, value: 'PhotoGallery' });
      this.categories.push({ name: data.VideoGallery, value: 'VideoGallery' });
      this.GetQueryParams();
    });

    // var myHTML= "<div><h1>Jimbo.</h1>\n<p>That's what she said</p></div>";
    // var strippedHtml = myHTML.replace(/<[^>]+>/g, '');    
  }

  GetQueryParams() {
    this.route.queryParams
      .subscribe(params => {
        this.key = params.key;
        this.cat = params.category;
        this.searchKey = null;
        this.selected = null;
        if (this.cat) {
          this.selected = this.categories.find(c => c.value == this.cat);
        }
        if (this.key) {
          this.searchKey = this.key;
        }
        if (this.key || this.cat)
          this.Search();
      });
  }
  Search() {
    if (this.selected || this.searchKey || this.startDate || this.endDate) {
      this.errorMsg = false;
      this.noDataFound = false;
      this.searchMinLength = false;
      if (this.searchKey && this.searchKey.length < 3) {
        this.searchMinLength = true;
      } else {
        let key = this.searchKey ? this.searchKey : '';
        let categ = this.selected ? this.selected.value : '';
        let from = this.startDate ? this.startDate : '';
        let to = this.endDate ? this.endDate : '';
        this.advancedSearchService.AdvancedSearch(key, categ, from.toLocaleString("en-US"), to.toLocaleString("en-US")).subscribe(data => {
          this.results = [];
          //console.log(data);
          if (data.Records.length == 0) {
            this.noDataFound = true;
          } else {
            this.noDataFound = false;
            this.results = data.Records;
            // //console.log(this.results);
            this.results.forEach(item => {
              if (item.Description != null)
                item.Description = item.Description.replace(/<[^>]+>/g, '');
              if (item.Date == '0001-01-01T00:00:00')
                item.Date = null;
            });
            //this.results = this.results.map(item => item.Description != null ? item.Description = item.Description.replace(/<[^>]+>/g, '') : item.Description);
            // this.results = replace;
            // //console.log(replace);
            // //console.log(this.results);
          }
        })
      }
    } else {
      this.errorMsg = true;
    }
  }
  RedirectToSearch() {
    this.counter++;
    if (this.selected || this.searchKey || this.startDate || this.endDate) {
      this.errorMsg = false;
      this.noDataFound = false;
      this.searchMinLength = false;
      if (this.searchKey && this.searchKey.length < 3) {
        this.searchMinLength = true;
      } else {
        let key = this.searchKey ? this.searchKey : '';
        let categ = this.selected ? this.selected.value : '';
        if (key && categ) {
          this.router.navigate(['/advanced-search'], { queryParams: { key: key, category: categ, flag: this.counter } });
        } else if (key) {
          this.router.navigate(['/advanced-search'], { queryParams: { key: key, flag: this.counter } });
        } else if (categ) {
          this.router.navigate(['/advanced-search'], { queryParams: { category: categ, flag: this.counter } })
        }
      }

    }
  }
  Reset() {
    this.searchKey = null;
    this.selected = null;
    this.startDate = null;
    this.endDate = null;
    this.results = [];
    this.noDataFound = false;
    this.errorMsg = false;
    this.searchMinLength = false;
    this.router.navigate(['/advanced-search']);
  }

  GetRoute(item: any) {
    debugger;
    this.CheckRouteLang(item);
    switch (item.ListName) {
      case "Events":
        this.router.navigateByUrl(`/media-center/events/events-details/${item.Id}`).then(() =>
          location.reload());
        break;
      case "Projects":
        this.router.navigateByUrl(`/projects/list/application/details/${item.Id}`).then(() =>
          location.reload());
        break;
      case "HPCProjects":
        this.router.navigateByUrl(`/performance-computing/hpc/hpc-project-details/${item.Id}`).then(() =>
          location.reload());
        break;
      case "News":
        this.router.navigateByUrl(`/media-center/news/news-details/${item.Id}`).then(() =>
          location.reload());
        break;
      case "Careers":
        this.router.navigateByUrl(`/careers-Opportunities/careers-list/careers-details/${item.Id}`).then(() =>
          location.reload());
        break;
      case "Internships":
        this.router.navigateByUrl(`/careers-Opportunities/internships-list/internships-details/${item.Id}`).then(() =>
          location.reload());
        break;
      case "WhitePapers":
        this.router.navigateByUrl(`/resources/white-papers/white-papers-details/${item.Id}`).then(() =>
          location.reload());
        break;
      case "BestPractices":
        this.router.navigateByUrl(`/resources/best-practice/best-practice-details/${item.Id}`).then(() =>
          location.reload());
        break;
      case "BODAndScientificCommittee":
        this.router.navigateByUrl(`/about-us/committee-list/committee-details/${item.Id}`).then(() =>
          location.reload());
        break;
      case "OurPeople":
        this.router.navigateByUrl(`/about-us/our-people-list/our-people-details/${item.Id}`).then(() =>
          location.reload());
        break;
      case "AICMaterials":
        this.router.navigateByUrl(`/resources/aic-materials`).then(() =>
          location.reload());
        break;
      case "PhotoGallery":
        this.router.navigateByUrl(`/about-us/our-people-list/our-people-details/${item.Title}`).then(() =>
          location.reload());
        break;
      case "VideoGallery":
        this.router.navigateByUrl(`/media-center/videos/video-details/${item.Title}`).then(() =>
          location.reload());
        break;
      case "ApplicationsDomains":
        this.router.navigateByUrl(`/projects/list/application`).then(() =>
          location.reload());
        break;
      case "TechnologyDomains":
        this.router.navigateByUrl(`/projects/list/technology`).then(() =>
          location.reload());
        break;
      case "GenericContent": {
        switch (item.Id) {
          case GenericContentTypes.StrategicRoadMap: {
            this.router.navigateByUrl(`/about-us/roadmap`).then(() =>
              location.reload());
            break;
          }
          case GenericContentTypes.AICMission: {
            this.router.navigateByUrl(`/about-us/mission`).then(() =>
              location.reload());
            break;
          }
          case GenericContentTypes.AICObjectives: {
            this.router.navigateByUrl(`/about-us/objectives`).then(() =>
              location.reload());
            break;
          }
          case GenericContentTypes.AICValues: {
            this.router.navigateByUrl(`/about-us/values`).then(() =>
              location.reload());
            break;
          }
          case GenericContentTypes.AICPartnershipModel: {
            this.router.navigateByUrl(`/about-us/partnership`).then(() =>
              location.reload());
            break;
          }
          case GenericContentTypes.HighPerformanceComputing: {
            this.router.navigateByUrl(`/performance-computing/hpc`).then(() =>
              location.reload());
            break;
          }
          case GenericContentTypes.WorkingWithAIC: {
            this.router.navigateByUrl(`/careers-Opportunities/working-with-aic`).then(() =>
              location.reload());
            break;
          }
          case GenericContentTypes.Internships: {
            this.router.navigateByUrl(`/careers-Opportunities/internships-list`).then(() =>
              location.reload());
            break;
          }
        }
        break;
      }
      default:
        this.router.navigate(['/']);
    }
  }
  private CheckRouteLang(item: any) {
    debugger;
    if (item.Lang != this.lang) {
      this.appService.menuContent = null;
      localStorage.setItem('lang', item.Lang);
      localStorage.setItem('oldLanguage', item.Lang);
      this.translateService.use(item.Lang);
      moment.locale(localStorage.getItem('oldLanguage'));
    }
  }
}
