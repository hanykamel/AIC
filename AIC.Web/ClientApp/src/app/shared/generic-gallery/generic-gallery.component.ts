import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UrlsRoutes } from '../../core/Enums/urlsRoutes';

@Component({
  selector: 'app-generic-gallery',
  templateUrl: './generic-gallery.component.html',
  styleUrls: ['./generic-gallery.component.css']
})
export class GenericGalleryComponent implements OnInit {
  @Input() icards;
  url;
  title = "Photo Gallery"
  constructor(private _router: Router) { }

  ngOnInit(): void {
  }

  onClickForDetails(icard) {
    console.log("inside onClickForDetails");
    let urlSections = this._router.url.split('/', 6);
    console.log(urlSections);
    this.url = urlSections[urlSections.length - 1];
    console.log(this.url);
    switch (this.url) {
      case UrlsRoutes.Videos: {
        this._router.navigate(['/media-center/videos/video-details/' + icard.title ]);
        //this._router.navigate(['/media-center/videos/video-details']);
        break;
      }
      case UrlsRoutes.Photos: {
        this._router.navigate(['/media-center/photos/photo-details/' + icard.title]);
        break;
      }
    }
  }
}
