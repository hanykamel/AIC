import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/app.service';
import { MediaCenterService } from '../../services/media-center.service';

@Component({
  selector: 'app-social-media',
  templateUrl: './social-media.component.html',
  styleUrls: ['./social-media.component.css']
})
export class SocialMediaComponent implements OnInit {
  facebookTitle = 'Facebook Posts';
  linkedInTitle = 'Linkedin Posts';
  youtubeTitle = 'Youtube Videos';
  facebookPosts;
  linkedInPosts;
  youtubePosts;
  filters: { field: string; value: string; }[] = [];
  body: {}
  constructor(private _mediaCenterService: MediaCenterService,
    private appService: AppService) { }

  ngOnInit(): void {
    //this.appService.pageLoader.next({ show: true });
    this.GetSocialMediaFeeds();
  }

  GetSocialMediaFeeds() {
    this.body = {
      'Filters': this.filters,
      "SortingField":"Modified"
    }
    this._mediaCenterService.GetSocialMediaFeeds(this.body).subscribe(data => {
      this.facebookPosts = data.Facebook[0];
      this.linkedInPosts = data.LinkedIn[0];
      this.youtubePosts = data.Youtube[0];
      //this.appService.pageLoader.next({ show: false });

    }, err => {
    })
  }

}
