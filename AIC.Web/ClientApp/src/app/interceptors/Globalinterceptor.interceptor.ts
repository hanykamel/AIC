import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from "rxjs/operators";
import { ActivatedRoute, Router } from '@angular/router';

@Injectable()

export class Globalinterceptor implements HttpInterceptor {
  
  constructor(private route: Router, private activatedRoute: ActivatedRoute) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    //let root = request.headers.get("Root");
    //let lang = "";
    //if (root != null) 
    //  lang = "\\";
    //else
     let lang = localStorage.getItem('oldLanguage') ? localStorage.getItem('oldLanguage') : "en"

    request = request.clone({
      setHeaders: {
        'lang':lang
      },
    });

    return next.handle(request);
  }
}
