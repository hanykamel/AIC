import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SitemapService {

  constructor(private http: HttpClient) { }

  GetMainMenu() {
    return this.http.get<any>("api/home/GetMainMenu");
  }
}
