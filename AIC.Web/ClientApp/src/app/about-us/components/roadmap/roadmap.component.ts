import { Component, OnInit } from '@angular/core';
import { GenericContentTypes } from 'src/app/Core/Enums/genericContentTypes';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-roadmap',
  templateUrl: './roadmap.component.html',
  styleUrls: ['./roadmap.component.css']
})
export class RoadmapComponent implements OnInit {
  
  aicRoadmapData: any;
  constructor(private _sharedService : SharedService) { }

  ngOnInit(): void {
    this.GetAicStrategicRoadmap();
  }
  GetAicStrategicRoadmap(){
    this._sharedService.GetGenericContent(GenericContentTypes.StrategicRoadMap).subscribe(data => {
      this.ParsingDataToSharedComponent(data.Items);
    }, err =>{
    })
  }

  ParsingDataToSharedComponent(data:any){
    data.forEach((element) => {
      var object : any = new Object();
      object.desc = element.AICDesc;
      object.title = element.Title;
      object.img = element.AICImage;
      this.aicRoadmapData = object;   
    });
  }

}
