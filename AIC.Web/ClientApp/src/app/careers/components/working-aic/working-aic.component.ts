import { Component, OnInit } from '@angular/core';
import { GenericContentTypes } from 'src/app/core/Enums/genericContentTypes';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-working-aic',
  templateUrl: './working-aic.component.html',
  styleUrls: ['./working-aic.component.css']
})
export class WorkingAicComponent implements OnInit {
  workingAicData: any;
  constructor(private _sharedService: SharedService) { }

  ngOnInit(): void {
    this.getWorkingAicData();

  }

  getWorkingAicData() {
    this._sharedService.GetGenericContent(GenericContentTypes.WorkingWithAIC).subscribe(data => {
      this.ParsingDataToSharedComponent(data.Items);
    }, err => {
    })
  }

  ParsingDataToSharedComponent(data: any) {
    data.forEach((element) => {
      var object: any = new Object();
      object.desc = element.AICDesc;
      object.title = element.Title;
      object.img = element.AICImage;
      this.workingAicData = object;
    });
  }

}
