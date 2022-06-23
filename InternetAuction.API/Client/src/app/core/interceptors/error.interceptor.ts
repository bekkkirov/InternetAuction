import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private toastr: ToastrService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
        catchError(err => {
            if(err) {
                switch (err.status) {
                    case 400:
                    case 401:
                        this.toastr.error(err.error.message);
                        break;

                    case 404:
                        this.router.navigateByUrl('not-found')
                        break;
                }
            }

            return throwError(err);
        })
    );
  }
}
