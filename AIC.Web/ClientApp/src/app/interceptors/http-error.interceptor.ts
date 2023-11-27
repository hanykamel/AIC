import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { CustomMessageService } from '../services/custom-message.service';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  skipedErrorsMsgs = ['NotSubscribedBefore','AlreadyUnSubscribed'];
  constructor(private _messageService: CustomMessageService, private _router:Router) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        catchError((error: any) => {
          let errorMessage = '';
          let internalErrorMsg = '';
          if (error.error && error.error.ExceptionType && error.error.ExceptionType == "Custom") {
            errorMessage = error.error.Message;
          } else if (error?.error) {
            internalErrorMsg = JSON.stringify(error.error.errors);
          }
          if (errorMessage && !this.skipedErrorsMsgs.includes(errorMessage)) {
            this._messageService.message.next({ severity: 'error', summary: 'Error', detail: errorMessage });
          }
          if (error?.statusText == 'Bad Request') {
            this._router.navigate(['/']);
          }
          return throwError(error);

        })
      )
  }
}
