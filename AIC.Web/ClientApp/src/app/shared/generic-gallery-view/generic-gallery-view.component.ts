import { Component, Input, OnInit} from '@angular/core';
@Component({
  selector: 'app-generic-gallery-view',
  templateUrl: './generic-gallery-view.component.html',
  styleUrls: ['./generic-gallery-view.component.css'],
})
export class GenericGalleryViewComponent implements OnInit {
  @Input() images: any[];
  showItemNavigators: true;
  responsiveOptions: any[] = [
    {
      breakpoint: '1024px',
      numVisible: 5
    },
    {
      breakpoint: '960px',
      numVisible: 4
    },
    {
      breakpoint: '768px',
      numVisible: 3
    },
    {
      breakpoint: '560px',
      numVisible: 1
    }
  ];

  constructor() { }

  ngOnInit() {
  }
}
