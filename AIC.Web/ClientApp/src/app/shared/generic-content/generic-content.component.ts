import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-generic-content',
  templateUrl: './generic-content.component.html',
  styleUrls: ['./generic-content.component.css']
})

export class GenericContentComponent implements OnInit, OnChanges {
  @Input() data;
  noDataAvailable: boolean = false;
  showHomeBtn: boolean = false;
  constructor() { }
  ngOnChanges() {
    if (this.data == undefined || this.data.length <= 0)
      this.noDataAvailable = true;
    else
      this.noDataAvailable = false;
    //console.log(this.data);
  }

  ngOnInit(): void {

  }

}
