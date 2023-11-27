import { Component, OnInit } from '@angular/core';
import { GenericContentTypes } from 'src/app/Core/Enums/genericContentTypes';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-objectives',
  templateUrl: './objectives.component.html',
  styleUrls: ['./objectives.component.css']
})
export class ObjectivesComponent implements OnInit {
 
  aicObjectiveData: any;
  constructor(private _sharedService : SharedService) { }

  ngOnInit(): void {
    this.GetAicObjective();
  }
  GetAicObjective(){
    this._sharedService.GetGenericContent(GenericContentTypes.AICObjectives).subscribe(data => {
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
      this.aicObjectiveData = object;   
    });
  }
}
