import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NewsletterService {

  constructor(private http: HttpClient) { }

  unsubscribe(body: {}) {
    return this.http.post<any>("api/Newsletters/Unsubscribe", body);
  }
}
