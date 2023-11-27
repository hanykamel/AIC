import { Component, OnInit } from '@angular/core';
import { GenericContentTypes } from 'src/app/Core/Enums/genericContentTypes';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-values',
  templateUrl: './values.component.html',
  styleUrls: ['./values.component.css']
})
export class ValuesComponent implements OnInit {
  
  aicValuesData: any;
  constructor(private _sharedService : SharedService) { }

  ngOnInit(): void {
    this.GetAicValues();
  }
  GetAicValues(){
    this._sharedService.GetGenericContent(GenericContentTypes.AICValues).subscribe(data => {
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
      this.aicValuesData = object;   
    });
  }

}
