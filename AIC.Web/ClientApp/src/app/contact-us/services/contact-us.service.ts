import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ContactUsService {

  constructor(private http: HttpClient) { }

  GetContactUs() {
    return this.http.get<any>("api/ContactUs/GetContactUs");
  }
  GetSocialMediaLinks() {
    return this.http.get<any>("api/ContactUs/GetSocialMedia");
  }
  CreateContactUsForm(body: {}) {
    return this.http.post<any>("api/ContactUs/CreateContactUsForm",body);
  }
  ValidateRecaptcha(body: {}) {
    return this.http.post<any>("api/Recaptcha/ValidateRecaptcha", body);
  }
}
