import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {catchError, Observable, throwError} from "rxjs";
import {Router} from "@angular/router";
import {Injectable} from "@angular/core";
import {ToastrService} from "ngx-toastr";
@Injectable()
export class ErrorInterceptor implements HttpInterceptor{
  constructor(private router:Router,private toastr:ToastrService) {
  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error:HttpErrorResponse) => {
        if(error){
          if(error.error.errors){
            throw error.error
          }
          else {
            this.toastr.error(error.error.message,error.status.toString())
          }
          if(error.status === 404){
            this.router.navigateByUrl('/not-found');
            this.toastr.error(error.error.message,error.status.toString())
          }
          if(error.status === 500){
            this.router.navigateByUrl('/server-error')
            this.toastr.error(error.error.message,error.status.toString())
          }
        }
        return throwError(()=> new Error(error.message))
      })
    );
  }


}

