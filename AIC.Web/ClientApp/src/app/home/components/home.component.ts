import {
  Component,
  ElementRef,
  HostListener,
  OnInit,
  ViewChild,
} from '@angular/core';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  socData: any[] = [];
  slidesdata: any;
  projectsdata: any;
  highlightsdata: any;
  careersdata: any;
  contactsdata: any;
  clickedElement;

  isbannercrolledIntoView: boolean;
  isprojectsScrolledIntoView: boolean;
  ishighlightsScrolledIntoView: boolean;
  iscareersScrolledIntoView: boolean;
  iscontactScrolledIntoView: boolean;

  @ViewChild('banner', { static: false })
  private banner: ElementRef<HTMLDivElement>;

  @ViewChild('projects', { static: false })
  private projects: ElementRef<HTMLDivElement>;

  @ViewChild('highlights', { static: false })
  private highlights: ElementRef<HTMLDivElement>;

  @ViewChild('careers', { static: false })
  private careers: ElementRef<HTMLDivElement>;

  @ViewChild('contact', { static: false })
  private contact: ElementRef<HTMLDivElement>;

  @HostListener('window:scroll', ['$event'])
  isScrolledIntoView() {
    //getting bounding rec
    const rectbanner = this.banner.nativeElement.getBoundingClientRect();
    const rectProjects = this.projects.nativeElement.getBoundingClientRect();
    const recthighlights =
      this.highlights.nativeElement.getBoundingClientRect();
    const rectcareers = this.careers.nativeElement.getBoundingClientRect();
    const rectcontact = this.contact.nativeElement.getBoundingClientRect();

    //check if section met top position
    const topShownBanner = rectbanner.top >= 0;
    const topShownProjects = rectProjects.top >= 0;
    const topShownHighlights = recthighlights.top >= 0;
    const topShownCareers = rectcareers.top >= 0;
    const topShownContact = rectcontact.top >= 0;

    //check if bottom in view as well

    const bottomShownBanner = rectbanner.bottom <= window.innerHeight;
    const bottomShownProjects = rectProjects.bottom <= window.innerHeight;
    const bottomShownHighlights = recthighlights.bottom <= window.innerHeight;
    const bottomShownCareers = rectcareers.bottom <= window.innerHeight;
    const bottomShownContact = rectcontact.bottom <= window.innerHeight;

    //getting toggle result of everysection

    this.isbannercrolledIntoView =
      (topShownBanner && bottomShownBanner); //|| topShownBanner
    this.isprojectsScrolledIntoView =
      (topShownProjects && bottomShownProjects); //|| topShownProjects
    this.ishighlightsScrolledIntoView =
      (topShownHighlights && bottomShownHighlights); //|| topShownHighlights
    this.iscareersScrolledIntoView =
      (topShownCareers && bottomShownCareers); //|| topShownCareers
    this.iscontactScrolledIntoView =
      (topShownContact && bottomShownContact); //|| topShownContact

    const circleToggled = document.querySelectorAll('.circle');
    if (this.isbannercrolledIntoView) {
      Array.from(document.querySelectorAll('.circle')).forEach((el) =>
        el.classList.remove('active')
      );

      circleToggled[0].classList.add('active');
    } else if (this.isprojectsScrolledIntoView) {
      Array.from(document.querySelectorAll('.circle')).forEach((el) =>
        el.classList.remove('active')
      );

      circleToggled[1].classList.add('active');
    } else if (this.ishighlightsScrolledIntoView) {
      Array.from(document.querySelectorAll('.circle')).forEach((el) =>
        el.classList.remove('active')
      );

      circleToggled[2].classList.add('active');
    } else if (this.iscareersScrolledIntoView) {
      Array.from(document.querySelectorAll('.circle')).forEach((el) =>
        el.classList.remove('active')
      );

      circleToggled[3].classList.add('active');
    } else if (this.iscontactScrolledIntoView) {
      Array.from(document.querySelectorAll('.circle')).forEach((el) =>
        el.classList.remove('active')
      );

      circleToggled[4].classList.add('active');
    }
  }

  constructor(private appService: AppService) {}

  ngOnInit(): void {
    this.GetSocialMedia();
    setTimeout(() => {
      this.onButtonGroupClick();
    }, 1000);
  }

  slidesExist(items) {
    this.slidesdata = items;
  }
  projectsExist(items) {
    this.projectsdata = items;
  }
  highlightsExist(items) {
    this.highlightsdata = items;
  }
  careersExist(items) {
    this.careersdata = items;
  }
  contactsExist(items) {
    this.contactsdata = items;
  }
  scroll(el: HTMLElement, sectionName?: string) {
    if (!document.getElementsByClassName('stickyNav').length) {
      const id = sectionName;
      const yOffset = -200;
      const element = document.getElementById(id);
      const y =
        element.getBoundingClientRect().top + window.pageYOffset + yOffset;

      window.scrollTo({ top: y, behavior: 'smooth' });
    } else {
      el.scrollIntoView({ behavior: 'smooth' });
    }
  }
  GetSocialMedia() {
    this.appService.GetSocialMedia().subscribe(
      (data) => {
        this.socData = this.appService.ParsingDataToHomeSocialMediaComponent(
          data.Items
        );
      },
      (err) => {}
    );
  }

  onButtonGroupClick() {
    let clickedElement = document.querySelectorAll('.circle');
    clickedElement.forEach((div) => {
      div.addEventListener('click', (e) => {
        clickedElement.forEach((el) => el.classList.remove('active'));
        div.classList.add('active');
      });
    });
  }
}
