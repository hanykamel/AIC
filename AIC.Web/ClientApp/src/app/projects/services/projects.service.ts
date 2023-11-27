import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {

  constructor(private http: HttpClient) { }

  ListProjects(body: {}) {
    return this.http.post<any>("api/projects/List", body);
  }
  ListApplicationsDomain(body: {}) {
    return this.http.post<any>("api/projects/ListApplicationDomains", body);
  }
  ListTechnologyDomain(body: {}) {
    return this.http.post<any>("api/projects/ListTechnologyDomains", body);
  }
  GetProjectById(id) {
    return this.http.get<any>("api/projects/GetById?id=" + id);
  }

}
