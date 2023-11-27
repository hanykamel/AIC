import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FaqsService {

  constructor(private http: HttpClient) { }

  ListFAQs(body: {}) {
    return this.http.post<any>("/api/FAQ/List", body);
  }
}
