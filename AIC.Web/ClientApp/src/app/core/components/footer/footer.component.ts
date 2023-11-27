import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AppService } from '../../../app.service';
import { CoreService } from '../../services/core.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  year: number;
  socData: any[] = [];
  showSocialMediaShare: boolean = false;
  showSocialMediaContent: boolean = false;
  footerElements;
  userForm: FormGroup;
  displaySubscribe;
  isScrolled:boolean=false;

  constructor(public router: Router, private coreService: CoreService, private appService: AppService,
    ) { }
    
  ngOnInit(): void {
    this.year = new Date().getFullYear();
    this.socData = [];
    this.observeSocialMediaShareIcon();
    this.appService.menuContent.subscribe(data => {
      let sections = JSON.parse(JSON.stringify(data['Menu'] ?? []));
      this.footerElements = sections.filter(e => e.ShowInFooter == true && e.HasChildren == true).slice(0, 3);
      this.footerElements.forEach(s => {
          s.Children = s.Children.filter(e => e.ShowInFooter == true);
        });
    });

    this.userForm = new FormGroup({
      'Email': new FormControl('', Validators.required),
    });
    this.GetSocialMedia();
  }

  add() {
    if (this.userForm.valid) {
      var subscriber = {
        Email: this.userForm.value['Email'],
      };
      this.coreService.AddNewSubscriber(subscriber).subscribe(data => {
        if (data)
          this.displaySubscribe = true;
      },
        error => {
          if (error.status== 409)
          this.userForm.controls['Email'].setErrors({ 'check': true });
        })
    } else {
      this.userForm.markAllAsTouched();
    }
  }


  reset() {
    //this.userForm.controls['Email'].markAsUntouched();
    this.userForm.setValue({ 'Email': '' });
    this.userForm.controls['Email'].setErrors({ 'required': null });

  }
  leave(){
    this.userForm.reset();
  }
  
  GetSocialMedia() {
    this.appService.GetSocialMedia().subscribe(data => {
        this.socData = this.appService.ParsingDataToSocialMediaComponent(data.Items);
        this.showSocialMediaContent = true;
    }, err => {
    })
  }
  observeSocialMediaShareIcon() {
    this.appService.observSocialMediaShareIcon.subscribe(data => {
      this.showSocialMediaShare = false;
    })
  }

}
