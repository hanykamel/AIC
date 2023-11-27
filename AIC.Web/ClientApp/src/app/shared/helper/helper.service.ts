import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

export interface ServerResponse {
    success: boolean;
    msg: string;
  }

@Injectable({
    providedIn: 'root'
  })

  export class HelperService {

    constructor(private http: HttpClient) { }

    private isBase64 = (obj) => {
        return obj.content && obj.content.startsWith("data:");
      }
    
      private appendObjectToFormData(formData: FormData, propertName: any, propertyValue: any) {
        formData.append(propertName, propertyValue);
      }
    
      public appendFileToFormData(formData: FormData, propertName: any, propertyValue: any) {
        var file = this.dataURItoBlob(propertyValue.content);
        formData.append(propertName, file, propertyValue.name);
      }
    
      public appendArrayToFormData(formData: FormData, propertName: any, propertyValue: any) {
          debugger;
        for (var i = 0; i < propertyValue.length; i++) {
          if (this.isBase64(propertyValue[i]))
            this.appendFileToFormData(formData, propertName, propertyValue[i]);
          else
            this.appendObjectToFormData(formData, propertName, propertyValue[i]);
        }
        return formData;
      }
    
      private convertToFormData = (dataJson) => {
        let formData = new FormData();
        for (var key in dataJson) {
          var property = dataJson[key];
          if (Array.isArray(property)) {
            this.appendArrayToFormData(formData, key, property);
          }
          else if (!this.isBase64(property)) {
            this.appendObjectToFormData(formData, key, property);
          }
          else {
            this.appendFileToFormData(formData, key, property);
          }
    
          //if (Array.isArray(property)) {
          //  for (var i = 0; i < property.length; i++) {
          //    if (this.isBase64(property[i])) {
          //      var file = this.dataURItoBlob(property[i].content);
          //      formData.append(key, file, property[i].name);
          //    }
          //    else {
          //      formData.append(key, property[i]);
          //    }
          //  }
          //}
          //else if (!this.isBase64(property)) {
          //  formData.append(key, property);
          //}
          //else {
          //  var file = this.dataURItoBlob(property.content);
          //  formData.append(key, file, property.name);
          //}
        }
        return formData;
      }
    
      public saveForm(dataJson: any, postUrl: string, handling: any) {
        var formData = this.convertToFormData(dataJson);
    
        this.http.post(postUrl, formData/*, { headers: this.headers }*/)
          .subscribe(data => {
            handling(data);
          });
      }
    
    
      private dataURItoBlob(dataURI) {
        // convert base64/URLEncoded data component to raw binary data held in a string
        var byteString;
        if (dataURI.split(',')[0].indexOf('base64') >= 0)
          byteString = atob(dataURI.split(',')[1]);
        else
          byteString = unescape(dataURI.split(',')[1]);
    
        // separate out the mime component
        var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];
    
        // write the bytes of the string to a typed array
        var ia = new Uint8Array(byteString.length);
        for (var i = 0; i < byteString.length; i++) {
          ia[i] = byteString.charCodeAt(i);
        }
    
        return new Blob([ia], { type: mimeString });
      }
    
      public getForm(id: number, handling: any) {
        this.http.get('/api/contact-us?id=' + id)
          .subscribe(data => {
            handling(data);
          });
      }
    
  }