import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CoreService {

  constructor(private http: HttpClient) { }

  GetMainMenu() {
    return this.http.get<any>("api/home/GetMainMenu");
  }
  AddNewSubscriber(body: {}) {
    return this.http.post<any>("/api/Newsletters/Add", body);
  }
}
