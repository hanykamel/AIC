import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/app.service';
import { GenericContentTypes } from 'src/app/core/Enums/genericContentTypes';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-performance-computing',
  templateUrl: './performance-computing.component.html',
  styleUrls: ['./performance-computing.component.css']
})
export class PerformanceComputingComponent implements OnInit {

  highPCData: any;
  hpcData: any;
  body: {};
  url: string;
  sortingField: string;
  isSortingAscending: boolean;
  pagination: boolean;
  Filters: {}[];
  pageSize: number;
  data: any[];
  projectsFound: boolean = true;

  constructor(private _sharedService: SharedService) { }

  ngOnInit(): void {
    this.GetHighPerformanceComputing();
    this.sortingField = "Modified";
    this.isSortingAscending = false;
    this.Filters = [];
    this.pagination = true;
    this.url = "api/HPCProjects/List";
    this.pageSize = 9;

  }

  GetHighPerformanceComputing() {
    this._sharedService.GetGenericContent(GenericContentTypes.HighPerformanceComputing).subscribe(data => {
      this.ParsingDataToSharedComponent(data.Items);
      //this.appService.pageLoader.next({ show: false });
    }, err => {
    })
  }

  ParsingDataToSharedComponent(data: any) {
    
    data.forEach((element) => {
      var object: any = new Object();
      object.desc = element.AICDesc;
      object.title = element.Title;
      object.img = element.AICImage;
      this.highPCData = object;
    });
  }
 
  ParsingDataToGenericList(data:any){
    this.data = [];
    if (data.length == 0)
      this.projectsFound = false;
    else
      this.projectsFound = true;
    data.forEach((element) => {
      var object: any = new Object();
      object.id = element.Id;
      object.desc = element.AICBrief;
      object.title = element.Title;
      object.img = element.AICImage;
      this.data.push(object);   
    });
    return this.data;

  }

}
