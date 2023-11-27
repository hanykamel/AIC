import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PartnersService {

  constructor(private http: HttpClient) { }

  GetPartnershipsList(body: {}) {
    return this.http.post<any>("api/Partenerships/List", body);
  }
}
