import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PerformanceComputingService {

  constructor(private http: HttpClient) { }


  GetHPCProjectById(id) {
    return this.http.get<any>("api/HPCProjects/GetById?id=" + id);
  }
}
