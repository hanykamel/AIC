import {
  Component,
  ElementRef,
  Input,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { Clipboard } from '@angular/cdk/clipboard';
import { AppService } from 'src/app/app.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-generic-social-icons',
  templateUrl: './generic-social-icons.component.html',
  styleUrls: ['./generic-social-icons.component.css'],
})
export class GenericSocialIconsComponent implements OnInit {
  @Input() links;
  @Input() sharingContentVisible = false;
  @Input() sharingSocialVisible = false;
  status: boolean = false;
  displayDialog: boolean = false;
  @ViewChild('someInput') someInput!: ElementRef;
  href: string;
  Title: string;
  description: any;
  desc: any;
  showMsg: boolean = false;
  currentUrl: string;
  socData: any[] = [];

  closePop() {
    this.someInput.nativeElement.classList.remove('PopIt');
  }
  constructor(
    private clipboard: Clipboard,
    public appService: AppService,
    public router: Router
  ) {}
  ngOnChanges(changes: SimpleChanges): void {
    if (this.description != null) {
      this.GetDescription();
    }
  }
  ngOnInit(): void {
    this.href = window.location.href;
    //console.log(this.href);
    this.GetSocialMedia();
  }
  showDialog() {
    this.displayDialog = true;
    this.someInput.nativeElement.classList.add('PopIt');
  }
  shareFacebook() {
    window.open(
      'https://www.facebook.com/sharer/sharer.php?u=' +
        this.href ,
      'popup',
      'width=600,height=600'
    );
    return false;
  }
  shareTwitter() {
    window.open(
      'https://twitter.com/share?url=' + this.href,
      'popup',
      'width=600,height=600'
    );
    return false;
  }
  shareLinkedIn() {
    window.open(
      'https://www.linkedin.com/shareArticle?url=' + this.href,
      'popup',
      'width=600,height=600'
    );
    return false;
  }
  GetDescription() {
    this.Title = '';
    this.desc = this.description.split(' ', 1000);
    this.desc.forEach((obj) => {
      this.Title += obj;
      this.Title += ' ';
    });
    this.Title = this.Title.substring(0, this.Title.length - 1);
  }
  copyUrl() {
    this.showMsg = true;
    this.currentUrl = window.location.href;
    this.clipboard.copy(this.currentUrl);
    setTimeout(
      function () {
        this.showMsg = false;
      }.bind(this),
      2000
    );
  }
  // GetSocialMedia() {
  //   this.appService.GetSocialMedia().subscribe(data => {
  //     this.socData = [];
  //     if (this.router.url == '/home')
  //       this.socData = this.appService.ParsingDataToHomeSocialMediaComponent(data.Items);
  //     else
  //       this.socData = this.appService.ParsingDataToSocialMediaComponent(data.Items);
  //   }, err => {
  //   })
  // }
  GetSocialMedia() {
    this.appService.GetSocialMedia().subscribe(
      (data) => {
        this.socData = [];
        this.socData = this.appService.ParsingDataToSocialMediaComponent(
          data.Items
        );
      },
      (err) => {}
    );
  }
}
