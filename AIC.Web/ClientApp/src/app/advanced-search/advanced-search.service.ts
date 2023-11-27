import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AdvancedSearchService {

  constructor(private http: HttpClient) { }

  AdvancedSearch(key: string, category: string, from: string, to: string, pageIndex: number = 0, pageSize: number = 9) {
    return this.http.get<any>("/api/Search/Search?key=" + key + "&category=" + category +
      "&from=" + from + "&to=" + to + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);
  }
}
