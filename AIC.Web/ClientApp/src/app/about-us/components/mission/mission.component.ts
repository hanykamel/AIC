import { Component, OnInit } from '@angular/core';
import { GenericContentTypes } from 'src/app/core/Enums/genericContentTypes';
import { SharedService } from 'src/app/shared/shared.service';
import { AppService } from '../../../app.service';

@Component({
  selector: 'app-mission',
  templateUrl: './mission.component.html',
  styleUrls: ['./mission.component.css']
})
export class MissionComponent implements OnInit {
 
  aicMissionData: any;
  constructor(private _sharedService: SharedService, private appService: AppService) { }

  ngOnInit(): void {
    //this.appService.pageLoader.next({show : true});
    this.GetAicMission();

  }

  GetAicMission(){
    this._sharedService.GetGenericContent(GenericContentTypes.AICMission).subscribe(data => {
      this.ParsingDataToSharedComponent(data.Items);
      //this.appService.pageLoader.next({ show: false });
    }, err =>{
    })
  }

  ParsingDataToSharedComponent(data:any){
    data.forEach((element) => {
      var object : any = new Object();
      object.desc = element.AICDesc;
      object.title = element.Title;
      object.img = element.AICImage;
      this.aicMissionData = object;   
    });
  }
}
