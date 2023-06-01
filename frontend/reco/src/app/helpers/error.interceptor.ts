import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private loginService: LoginService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((response) => {
        let handled: boolean = false;

        if (response instanceof HttpErrorResponse) {
          if (!(response.error instanceof ErrorEvent)) {
            switch (response.status) {
              case 401:
                if(this.loginService.areTokensExist())
                {
                  this.loginService.logOut();
                  this.router.navigate(['/']);
                  handled = true;
                }
                else{
                  handled = false;
                }
                break;
            }
          }
        }

        if(handled) {
          return of(response);
        }
        else {
          return throwError(()=>response);
        }
      })
    );
  }
}


