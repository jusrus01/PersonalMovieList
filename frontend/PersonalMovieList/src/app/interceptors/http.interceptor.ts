import { Injectable, Injector } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';

@Injectable()
export class HttpClientInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService, private router: Router) {}

  private handleAuthError(err: HttpErrorResponse) : Observable<any> {
    if(err.status == 401 || err.status == 403 || err.status == 0) {
      //this.authService.logOut();
      //this.router.navigate(['/login']);
      this.authService.logOut();
      this.router.navigate(['/login']);
      return of(err.message);
    }
    return throwError(err);
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    var authReq;
    if(this.authService.isLoggedIn()) {
      authReq = request.clone({
        headers: request.headers.set("Authorization", "Bearer " + this.authService.getToken()),
        withCredentials: false
      });
    } else {
      authReq = request.clone();
    }
    return next.handle(authReq).pipe(catchError(err => this.handleAuthError(err)));
  }
}
