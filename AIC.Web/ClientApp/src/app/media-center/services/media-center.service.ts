import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MediaCenterService {

  constructor(private http: HttpClient) { }
  data: any[] = [];


  GetNewsList(body : {}){
    return this.http.post<any>("api/News/List",body);
  }

  GetNewsById(id){
    return this.http.get<any>("api/News/GetById?id=" + id);
  }
  GetPhotosList(body: {}) {
    return this.http.post<any>("api/media/PhotoAlbumsList", body);
  }

  GetPhotosAlbum(body: {}, albumname) {
    return this.http.post<any>("/api/Media/PhotoAlbumPhotosList?albumName=" + albumname, body);
  }

  GetVideosList(body: {}) {
    return this.http.post<any>("api/media/VideoAlbumsList", body);
  }

  GetVideosAlbum(body: {}, albumname) {
    return this.http.post<any>("/api/Media/VideoAlbumVideosList?albumName=" + albumname, body);
  }
  GetAlbumDetails(url: string) {
    return this.http.get<any>(url);
  }

  GetArchivedNewsList(body : {}){
    return this.http.post<any>("api/News/GetArchivedList",body);
  }
  GetEventsList(body : {}){
    return this.http.post<any>("api/Events/List",body);
  }

  GetEventById(id){
    return this.http.get<any>("api/Events/GetById?id=" + id);
  }

  GetArchivedEventsList(body : {}){
    return this.http.post<any>("api/Events/GetArchivedList",body);
  }
  GetSocialMediaFeeds(body : {}){
    return this.http.post<any>("api/SocialMediaFeeds/List",body);
  }
  ParsingNewsDataToSharedComponent(data:any){
    data.forEach((element) => {
      element.id = element.Id;
      element.type = element.NewsType;
      element.date = element.PublishDate;
      element.title = element.Title;
      element.img = element.ThumbnailImage;
      element.desc = element.AICBrief;
      element.isVisible = element.ShowInHomePage;
    });
    return data

  }
  ParsingEventsDataToSharedComponent(data:any){
    data.forEach((element) => {
      element.id = element.Id;
      element.type = element.EventType;
      element.date = element.EventDate;
      element.title = element.Title;
      element.img = element.ThumbnailImage;
      element.desc = element.AICBrief;
      element.isVisible = element.ShowInHomePage;
    });
    return data;
  }
  GetVideoAlbumDetails(albumname) {
    return this.http.get<any>("/api/media/GetVideoAlbumDetails?albumName=" + albumname);
  }
}
