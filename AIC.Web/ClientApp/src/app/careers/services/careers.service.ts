import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CareersService {

  constructor(private http: HttpClient) { }

  GetJobTypes(){
    return this.http.get<any>("/api/RequestLookup/ListJobTypes");
  }
  GetDegreeLevels(){
    return this.http.get<any>("/api/RequestLookup/ListDegreeLevels");
  }
  AddJoinUs(body:any){
    return this.http.post<any>("/api/JoinUs/Add",body);
  }
  GetJoinUs(email , date){
    const params = new HttpParams()
   .set('Email', email)
   .set('date', date);
    return this.http.get<any>("/api/JoinUs/Get?"+ params);
  }
  AddUserProfile(email:any){
    return this.http.post<any>("/api/JoinUs/AddProfile",email);
  }
  GetCareerById(id: number) {
    return this.http.get<any>("api/Careers/GetById?Id=" + id);
  }
  GetInternById(id: number) {
    return this.http.get<any>("api/Internships/GetById?Id=" + id);
  }
  AddUserProfileVacancy(body:any){
    return this.http.post<any>("/api/Careers/AddProfile",body);
  }
  GetVacancyProfile(email , date , vacancyId){
    const params = new HttpParams()
   .set('Email', email)
   .set('Date', date)
   .set('VacancyId', vacancyId);
    return this.http.get<any>("/api/Careers/Get?"+ params);
  }
  AddVacancy(body:any){
    return this.http.post<any>("/api/Careers/Add",body);
  }
  AddUserProfileInternship(body:any){
    return this.http.post<any>("/api/Internships/AddProfile",body);
  }
  GetInternshipProfile(email , date , internshipId){
    const params = new HttpParams()
   .set('Email', email)
   .set('Date', date)
   .set('InternshipId', internshipId);
    return this.http.get<any>("/api/Internships/Get?"+ params);
  }
  AddInternships(body:any){
    return this.http.post<any>("/api/Internships/Add",body);
  }
  ValidateRecaptcha(body: {}) {
    return this.http.post<any>("api/Recaptcha/ValidateRecaptcha", body);
  }
}
