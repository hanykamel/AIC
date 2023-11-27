import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http: HttpClient) { }

  Get() {
    return this.http.post<any>("/api/home/get","");
  }
  GetMainBanner(){
    return this.http.get<any>("api/Home/GetMainBanner");
  }
  GetProjects(){
    return this.http.get<any>("api/Home/GetProjects");
  }
  GetHighlights(){
    return this.http.get<any>("api/Home/GetHighlights");
  }
  GetHomePageSections(){
    return this.http.get<any>("api/Home/GetHomeSections");
  }
  GetCarrers(){
    return this.http.get<any>("api/Home/GetCareers");
  }
}
