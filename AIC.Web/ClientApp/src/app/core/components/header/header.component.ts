import {
  Component,
  OnInit,
  HostListener,
  ElementRef,
  ViewChild,
  Renderer2,
  Input,
  ChangeDetectorRef
} from '@angular/core';
import {
  transition,
  animate,
  trigger,
  style,
  state,
} from '@angular/animations';
import { MenuItem } from 'primeng/api';
import { Router, RouterLink } from '@angular/router';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import * as moment from 'moment';
import { AppService } from '../../../app.service';
import { CoreService } from '../../services/core.service';
import { PageLoaderService } from '../../../shared/page-loader/page-loader.service';
import { Location } from '@angular/common';


function accessibility(d, lang) {
  var s = d.createElement('script');
  s.setAttribute('data-language', lang);
  s.setAttribute('data-statement_text:', '');
  s.setAttribute('data-statement_url', '');
  s.setAttribute('data-trigger', 'AccessibilityFun');
  s.setAttribute('data-account', '9NZyQ5JCKB');
  s.setAttribute('src', '../../../../assets/js/widget.js');
  (d.body || d.head).appendChild(s);
}

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  animations: [
    trigger('openClose', [
      state(
        'open',
        style({
          transform: 'translateY(0%)',
          opacity: 1,
          visibility: 'visible',
        })
      ),
      state(
        'closed',
        style({
          transform: 'translateY(-100%)',
          opacity: 0,
          visibility: 'hidden',
        })
      ),
      transition('open => closed', [animate('0.3s')]),
      transition('closed => open', [animate('0.3s')]),
    ]),
    trigger('openCloseSearch', [
      state(
        'open',
        style({
          transform: 'translateY(0%)',
          opacity: 1,
          visibility: 'visible',
        })
      ),
      state(
        'closed',
        style({
          transform: 'translateY(-100%)',
          opacity: 0,
          visibility: 'hidden',
        })
      ),
      transition('open => closed', [animate('0.3s')]),
      transition('closed => open', [animate('0.3s')]),
    ]),
  ],
})
export class HeaderComponent implements OnInit {
  items: MenuItem[];
  isOpen: boolean = true;
  isOpenSearch: boolean = false;
  isAccess: boolean = true;
  accessDialog: boolean = false;
  showMenu: boolean = false;
  currentLang = localStorage.getItem('oldLanguage') || 'en';
  navbarfixed: boolean = false;
  categories;
  selected;
  searchKey;
  currentLangText = localStorage.getItem('oldLanguage') || 'en';
  showSearchError;
  @ViewChild('menu', { read: ElementRef }) menu: ElementRef;
  trigger: boolean = false;
  @HostListener('window:scroll', ['$event']) onscroll() {
    this.navbarfixed = false;
    this.isOpenSearch = false;
    var containerHeight: number = document.getElementById('divHeight').clientHeight;
    var headerHeight: number = 60;
    var footerHeight: number = document.getElementById('footer').clientHeight;
    var fullBody = containerHeight + headerHeight + footerHeight;
    this.trigger = this.appService.headertrigger.getValue();
    if (containerHeight >= 665 && window.scrollY > 20 && fullBody > window.innerHeight) {
      this.navbarfixed = true;
      if (this.appService.headertrigger.getValue() == true && window.scrollY < 300) {
        this.navbarfixed = false;

      }
    } else {
      this.navbarfixed = false;
      // this.appService.headertrigger.next(false);

    }
    const subs = document.querySelectorAll('.p-menuitem');

    subs.forEach((sub) => {
      sub.classList.remove('p-menuitem-active');
    });
  }
  @HostListener('document:click', ['$event']) onDocumentClick(event) {
    if (!this._eref.nativeElement.contains(event.target)) {
      this.isOpenSearch = false;
    }
  }
  ClickedOut(event) {
    if (event.target.className.includes('headerCont')) {
      this.isOpenSearch = false;
    }
  }

  constructor(
    public router: Router,
    private translateService: TranslateService,
    private appService: AppService,
    private coreService: CoreService,
    private _eref: ElementRef,
    private pageLoaderService: PageLoaderService,
    private renderer: Renderer2,
    private ref: ChangeDetectorRef,
    private location: Location
  ) {
    //this.translateService.use(this.translateService.defaultLang);
    //moment.locale(localStorage.getItem('oldLanguage'));

  }
  public onKeydownMain(event): void {
    //console.log('event', event);
    if (event.shiftKey == true && event.key == "Tab") {
      this.appService.headertrigger.next(false);
      //console.log('header 1');
      if (location.pathname == '/home' || this.router.url == '/home#banner') {
        document.getElementById('divHeight').setAttribute("role", "none");
        // document.getElementById('banner').setAttribute("role", "main");
        document.getElementById('banner').setAttribute("tabindex", "0");
      }
      else {
        document.getElementById('divHeight').setAttribute("role", "main");
        document.getElementById('divHeight').setAttribute("tabindex", "0");
      }
    }
  }

  ngOnInit() {
    this.changeLayout();
    this.items = [];
    this.appService.menuContent.subscribe((data) => {
      this.items = [];
      var menu = data['Menu'] ?? [];
      menu
        .sort((a, b) => {
          return a.Order - b.Order;
        })
        .slice(0, 8)
        .forEach((e) => {
          this.items.push({
            label: this.currentLang == 'ar' ? e.TitleAr : e.Title,
            routerLink: e.Url ? e.Url : null,
            command: () => {
              if (e.Children.length == 0) {
                this.showMenu = false;
                this.isOpenSearch = false;
              }
            },
            items:
              e.Children && e.Children.length > 0
                ? e.Children.sort((a, b) => {
                  return a.Order - b.Order;
                }).map((c) => ({
                  label: this.currentLang == 'ar' ? c.TitleAr : c.Title,
                  routerLink: c.Url && !c.OtherUrl ? c.Url : null,
                  url: c.Url && c.OtherUrl ? c.OtherUrl : null,
                  command: () => {
                    //console.log(c)
                    localStorage.setItem('breadCrumbesAR',c.ParentAR);
                    localStorage.setItem('breadCrumbesEN',c.ParentEN);
                    this.showMenu = false;
                    this.isOpenSearch = false;
                  },
                }))
                : null,
          });
        });
    });

    this.categories = [];
    this.translateService.get('Search').subscribe((data) => {
      this.categories.push({ name: data.Projects, value: 'Projects' });
      this.categories.push({ name: data.HPCProjects, value: 'HPCProjects' });
      this.categories.push({ name: data.News, value: 'News' });
      this.categories.push({ name: data.Events, value: 'Events' });
      this.categories.push({ name: data.Careers, value: 'Careers' });
      this.categories.push({ name: data.Internships, value: 'Internships' });
      this.categories.push({ name: data.WhitePapers, value: 'WhitePapers' });
      this.categories.push({
        name: data.BestPractices,
        value: 'BestPractices',
      });
      this.categories.push({ name: data.PhotoGallery, value: 'PhotoGallery' });
      this.categories.push({ name: data.VideoGallery, value: 'VideoGallery' });
    });
    if (this.appService.headertrigger.getValue() == true) {
      this.navbarfixed = false;
      this.ref.markForCheck();
      this.ref.detectChanges();

    }
  }

  ngAfterViewInit(): void {
    accessibility(document, this.translateService.currentLang);
  }
  accessbility(type) {
    let action = '';
    switch (type) {
      case 'bigTextToggle': //done
        action = 'window.UserWay.bigTextToggle()';
        break;
      case 'contrastToggle': //done
        action = 'window.UserWay.contrastToggle()';
        break;
      case 'bigCursorToggle': //done
        action = 'window.UserWay.bigCursorToggle()';
        break;
      case 'keyboardNavToggle': //done
        action = 'window.UserWay.keyboardNavToggle()';
        break;
      case 'desaturateToggle':
        action = 'window.UserWay.desaturateToggle()';
        break;
      case 'highlightToggle': //done
        action = 'window.UserWay.highlightToggle()';
        break;
      case 'legibleFontsToggle':
        action = 'window.UserWay.legibleFontsToggle()';
        break;
      case 'textSpacingToggle':
        action = 'window.UserWay.textSpacingToggle()';
        break;
      case 'resetAll':
        action = 'window.UserWay.resetAll()';
        break;
      case 'readPageToggle': //done
        action = 'window.UserWay.readPageToggle()';
        break;
      case 'readingGuideToggle':
        action = 'window.UserWay.readingGuideToggle()';
        break;
      case 'dyslexiaFontToggle':
        action = 'window.UserWay.dyslexiaFontToggle()';
        break;
      case 'iconVisibilityToggle': //no
        action = 'window.UserWay.iconVisibilityToggle()';
        break;
      case 'pageStructureLinks':
        action = 'window.UserWay.pageStructureLinks()';
        break;
      case 'tooltipsToggle':
        action = 'window.UserWay.tooltipsToggle()';
        break;
      case 'stopAnimationToggle':
        action = 'window.UserWay.stopAnimationToggle()';
        break;
      case 'openQuickAccessibilityMenu':
        action = 'window.UserWay.openQuickAccessibilityMenu()';
        break;
      case 'lineHeightToggle':
        action = 'window.UserWay.lineHeightToggle()';
        break;
      default:
        action = 'window.UserWay.resetAll()';
    }
    eval(action);
  }
  openMenu() {
    this.isOpenSearch = false;
    this.showMenu = true;
  }
  toggleSearch() {
    this.isOpenSearch = !this.isOpenSearch;
    this.isOpen = true;
    this.isAccess = true;
  }
  toggleAccess() {
    this.accessDialog = true;
    this.isOpenSearch = false;
  }
  goToSearch() {
    if (this.selected || this.searchKey) {
      this.showSearchError = false;
      if (this.searchKey && this.searchKey.length < 3) {
        this.showSearchError = true;
      } else {
        if (this.searchKey && this.selected) {
          let sel = this.selected.value;
          let key = this.searchKey;
          this.searchKey = null;
          this.selected = null;
          this.isOpenSearch = false;
          this.router.navigate(['/advanced-search'], {
            queryParams: { key: key, category: sel },
          });
        } else if (this.searchKey) {
          let key = this.searchKey;
          this.searchKey = null;
          this.isOpenSearch = false;
          this.router.navigate(['/advanced-search'], {
            queryParams: { key: key },
          });
        } else if (this.selected) {
          let sel = this.selected.value;
          this.selected = null;
          this.isOpenSearch = false;
          this.router.navigate(['/advanced-search'], {
            queryParams: { category: sel },
          });
        }
      }
    }
  }
  changeLayout(): void {
    const oldLang = localStorage.getItem('oldLanguage') || 'en';
    this.translateService.use(oldLang);


    if (oldLang == 'ar') {
      document.getElementsByTagName('html')[0].removeAttribute('dir');
      document.getElementsByTagName('html')[0].setAttribute('dir', 'rtl');
      document.getElementsByTagName('body')[0].classList.remove('en');
      document.getElementsByTagName('body')[0].classList.add('ar');
      this.currentLangText = 'ar';
    } else {
      document.getElementsByTagName('html')[0].removeAttribute('dir');
      document.getElementsByTagName('html')[0].setAttribute('dir', 'ltr');
      document.getElementsByTagName('body')[0].classList.remove('ar');
      document.getElementsByTagName('body')[0].classList.add('en');
      this.currentLangText = 'en';
    }
  }

  changeLang(lang: string): void {
    this.pageLoaderService.isLoading.next(true);
    this.appService.menuContent = null;
    localStorage.setItem('oldLanguage', lang);
    this.translateService.use(lang);
    moment.locale(localStorage.getItem('oldLanguage'));
    location.reload();
  }

  // GroupHeader(data) {
  //   let parents = data.filter((s) => s.Parent == '');
  //   parents.forEach((p) => {
  //     p.Children = data.filter((s) => s.Parent == p.Title);
  //   });
  //   return parents;
  // }
}
