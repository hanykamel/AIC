import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-no-data-found',
  templateUrl: './no-data-found.component.html',
  styleUrls: ['./no-data-found.component.css']
})
export class NoDataFoundComponent implements OnInit {

  constructor(private router: Router) { }
  @Input() showHomeBtn: boolean = false;
  @Input() NoDataFoundMsg: string ='NoDataFound';

  ngOnInit(): void {
  }
  navigatToHomePage() {
    this.router.navigate(['./']);
  }
}
