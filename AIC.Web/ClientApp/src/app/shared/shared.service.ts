import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private http: HttpClient) { }

  GetBodAndScientificList(body: {}) {
    return this.http.post<any>("api/BodAndScientific/List",body);
  }
  GetBodAndScientificById(id) {
    return this.http.get<any>("api/BodAndScientific/Get?Id=" + id);
  }
  GetOurPeopleList(body: {}) {
    return this.http.post<any>("api/OurPeople/List",body);
  }
  GetOurPeopleById(id) {
    return this.http.get<any>("api/OurPeople/Get?Id=" + id);
  }
  GetGenericContent(contentType){
    return this.http.get<any>("api/GenericContent/Get?Type=" + contentType);
  }
  GetHpcProjectsData(body){
    return this.http.post<any>("api/HPCProjects/List",body);
  }
  ListRelatedLinks(ids) {
    return this.http.post<any>("api/RelatedLinks/ListByIds", ids);
  }
}
