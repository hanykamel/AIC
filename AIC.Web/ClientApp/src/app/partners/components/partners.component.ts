import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { PartnersService } from '../services/partners.service';

@Component({
  selector: 'app-partners',
  templateUrl: './partners.component.html',
  styleUrls: ['./partners.component.css']
})
export class PartnersComponent implements OnInit {
  filters: { field: string; value: string; }[] = [];
  body: {};
  partnershipsData: any;
  noDataAvailable: any = false;

  url: string;
  pagination: boolean;
  Filters: {}[];
  pageSize: number;
  nationalPartners;
  internationalPartners;
  lang;
  types = [];
  constructor(private _PartnersService: PartnersService, private translate : TranslateService) { }

  ngOnInit(): void {
    this.lang = this.translate.currentLang;
    this._PartnersService.GetPartnershipsList({}).subscribe(data => {
      data.Items.forEach(item => {
        if (this.types.includes(item.Type) == false)
          this.types.push(item.Type);
      });
      //if (this.lang == 'en') {
        this.nationalPartners = data.Items.filter(p => p.Type == this.types[0]);
        this.internationalPartners = data.Items.filter(p => p.Type == this.types[1]);
      //} else {
      //  this.nationalPartners = data.Items.filter(p => p.Type.toLowerCase() == 'محلي');
      //  this.internationalPartners = data.Items.filter(p => p.Type.toLowerCase() == 'دولي');
      //}
      
      //console.log(this.nationalPartners);
      //console.log(this.internationalPartners);
    });
  }

  onClick(url) {
    window.open(
      url,
      '_blank'
    );
  }
}
