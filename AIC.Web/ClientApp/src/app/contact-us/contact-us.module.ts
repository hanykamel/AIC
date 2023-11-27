import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { ContactUsRoutingModule } from './contact-us-routing.module';
import { ContactUsComponent } from './components/contact-us/contact-us.component';
import { CaptchaModule } from 'primeng/captcha';
import { ClipboardModule } from 'ngx-clipboard';


@NgModule({
  declarations: [
    ContactUsComponent
  ],
  imports: [
    CommonModule,
    ContactUsRoutingModule, SharedModule, CaptchaModule, ClipboardModule
  ]
})
export class ContactUsModule { }
