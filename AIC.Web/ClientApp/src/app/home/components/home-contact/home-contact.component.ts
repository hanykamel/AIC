import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AppService } from 'src/app/app.service';
import { HomeService } from '../home.service';

@Component({
  selector: 'app-home-contact',
  templateUrl: './home-contact.component.html',
  styleUrls: ['./home-contact.component.css'],
})
export class HomeContactComponent implements OnInit {
  aboutUs: any;
  @Output() dataEmitter = new EventEmitter();

  constructor(
    private homeService: HomeService,
    public appService: AppService
  ) {}

  ngOnInit(): void {
    //this.appService.pageLoader.next({show:true});
    this.GetHomeAboutUsSections();
  }
  GetHomeAboutUsSections() {
    this.homeService.GetHomePageSections().subscribe(
      (data) => {
        this.aboutUs = data.ContactUsBrief;
        this.dataEmitter.emit(this.aboutUs);
        //this.appService.pageLoader.next({ show: false });
      },
      (err) => {}
    );
  }
}
