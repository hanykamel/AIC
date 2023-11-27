import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { PageLoaderService } from './page-loader.service';

@Component({
  selector: 'app-page-loader',
  templateUrl: './page-loader.component.html',
  styleUrls: ['./page-loader.component.css']
})
export class PageLoaderComponent implements OnInit {
  isLoading: boolean;

  constructor(private pageLoaderService: PageLoaderService) { }

  ngOnInit(): void {
    this.pageLoaderService.isLoading.subscribe((data) => {
      this.isLoading = data;
      
    });
  }

}
