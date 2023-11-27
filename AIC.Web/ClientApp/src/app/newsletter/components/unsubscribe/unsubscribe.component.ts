import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { NewsletterService } from '../../services/newsletter.service';
import { AppService } from '../../../app.service';

@Component({
  selector: 'app-unsubscribe',
  templateUrl: './unsubscribe.component.html',
  styleUrls: ['./unsubscribe.component.css']
})
export class UnsubscribeComponent implements OnInit {
  email;
  success;
  errorMsg;

  constructor(private route: ActivatedRoute, private newsletterService: NewsletterService, private translate: TranslateService,
              private appService: AppService  ) { }

  ngOnInit(): void {
    this.route.paramMap
      .subscribe(params => {
        this.email = params.get('email') || "";
        //console.log(this.email);
        if (this.email) {
          this.unsubscribe();
        }
      });
  }

  unsubscribe() {
    let body = { "Email": this.email };
    this.newsletterService.unsubscribe(body).subscribe(data => {
      if (data) {
        this.success = true;
      }
    },
      error => {
        if (error?.error?.ExceptionType == 'Custom') {
          let errorDetails = error?.error?.Message;
          this.errorMsg = "backEndValidation." + errorDetails;
        }
        else
          this.errorMsg = "Newsletter.UnsubscribeErrMsg";

        this.success = false;
      })
  }
}
