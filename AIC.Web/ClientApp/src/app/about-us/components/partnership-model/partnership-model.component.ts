import { Component, OnInit } from '@angular/core';
import { GenericContentTypes } from 'src/app/Core/Enums/genericContentTypes';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-partnership-model',
  templateUrl: './partnership-model.component.html',
  styleUrls: ['./partnership-model.component.css']
})
export class PartnershipModelComponent implements OnInit {
  aicPartnershipData: any;

  constructor(private _sharedService : SharedService) { }

  ngOnInit(): void {
    this.GetAicPartnership();
  }
  GetAicPartnership(){
    this._sharedService.GetGenericContent(GenericContentTypes.AICPartnershipModel).subscribe(data => {
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
      this.aicPartnershipData = object;   
    });
  }

}
