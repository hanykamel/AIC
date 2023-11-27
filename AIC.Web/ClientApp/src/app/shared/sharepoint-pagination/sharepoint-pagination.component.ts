import { Component, ContentChild, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, TemplateRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from '../../app.service';
import { DataLoaderComponent } from '../data-loader/data-loader.component';

@Component({
  selector: 'app-sharepoint-pagination',
  templateUrl: './sharepoint-pagination.component.html',
  styleUrls: ['./sharepoint-pagination.component.css']
})
export class SharepointPaginationComponent implements OnInit, OnDestroy, OnChanges {

  constructor(private appService: AppService, public translate: TranslateService) {
  }
    
  @Input() url: string;
  @Input() IsShowNoData: boolean = true;
  @Input() NoDataFoundMsg: string ='NoDataFound';
  @Input() pageSize: number=6;
  @Input() sortingField: string = "Title";
  @Input() isSortingAscending: boolean=false;
  @Input() pagination: boolean=true;
  @Input() Filters: [];
  @Input() SortType: [];
  @ContentChild(TemplateRef)
  templateRef: TemplateRef<any>;
  first: boolean = true;
  Data: any[] = [];
  NoDataAvailable: boolean = false;
  throttle = 300;
  scrollDistance = 1;
  loader = false;
  isEnd = false;
  pagingInfo: string = "";
  body = {};
  service: any;
  ngOnInit(): void {
  }
  ngOnChanges(): void {
    this.init()
    this.GetListData(true);
  }
  ngOnDestroy(): void {
    this.init();
    this.Data = [];
    this.appService.showSpinner = true;
    this.service.unsubscribe();
  }
  private init() {
    this.pagingInfo = "";
    this.first = true;
    this.isEnd = false;
  }
  onScrollDown() {
    if (this.Data.length > 0)
      if (!this.isEnd && !this.loader && this.pagination) {
        this.loader = true;
        this.GetListData(false)
      }
  }
  LoadMore() {
    if (this.Data.length > 0)
      if (!this.isEnd && !this.loader && this.pagination) {
        this.loader = true;
        this.GetListData(false)
      }
  }
  private GetListData(isReset) {
    if (isReset) {
      this.Data = [];
      this.loader = true;
      this.NoDataAvailable = false;
    }    
    this.body = {
      pageSize: this.pageSize,
      sortingField: this.sortingField,
      isSortingAscending: this.isSortingAscending,
      Filters: this.Filters
    }
    this.service = this.appService.GetList(this.url, this.body, this.pagingInfo).subscribe(data => {
      this.first = false;
      //if (!this.first)
      this.appService.showSpinner = false;
      if (data.Items.length > 0) {
        
        if (data.Items.length > 0 && data.PagingInfo === "") {
          this.isEnd = true;
        }
        if (isReset) {
          this.Data = data.Items;
        }
        else {
          this.Data = [...this.Data, ...data.Items];
        }

        this.NoDataAvailable = false;
      }
      else {
        this.Data = []
        this.NoDataAvailable = true;
      }
      this.loader = false;
      this.pagingInfo = data.PagingInfo;
   });
  }

  sortData() {
    this.isSortingAscending = !this.isSortingAscending;
    this.init();
    this.GetListData(true);
  }
  onSortChange(value) {
    this.sortingField = value ? value: this.sortingField;
    this.init();
    this.GetListData(true);
  }
}
