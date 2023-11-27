import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ResourcesService {
  constructor(private http: HttpClient) {}

  GetMaterialsList() {
    return this.http.get<any>('/api/AICMaterials/ListMaterials');
  }
  GetWhitePapersList(body: any) {
    return this.http.post<any>('/api/WhitePapers/list', body);
  }
  GetWhitePapersById(id) {
    return this.http.get<any>('/api/WhitePapers/GetById?id=' + id);
  }
  ParsingWhitePapersDataToSharedComponent(data: any) {
    data.forEach((element) => {
      element.id = element.Id;
      element.title = element.Title;
      element.type = element.Type;
      element.date = element.Date;
      element.desc = element.Brief;
    });
    return data;
    // data.forEach((element) => {
    //   var object: any = new Object();
    //   object.id = element.Id;
    //   object.title = element.Title;
    //   object.type = element.Type;
    //   object.date = element.Date;
    //   object.desc = element.Brief;
    //   Data.push(object);
    // });
    // return Data;
  }
  GetBestPracticeById(id) {
    return this.http.get<any>('/api/BestPractice/GetById?id=' + id);
  }
}
