import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { LoaderService } from "../services/loaderService.service";
import { Observable, catchError, throwError } from "rxjs";
import { Injectable } from "@angular/core";
import { NotificationService } from "../services/notification.service";

@Injectable()
export class ErrorHandlerInterceptor implements HttpInterceptor {
  constructor(
    private readonly notificationService: NotificationService,
    private readonly loaderService: LoaderService,
  ) { }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler,
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((httpError: HttpErrorResponse) => {
        const errorMessages: string[] = [];

        this.loaderService.hide();

        if (httpError.status === 403) {
          this.notificationService.showError(
            "Nie posiadasz uprawnień do wykoniania tej operacji.",
          );

          return throwError(() => httpError);
        }

        if (errorMessages.length > 0) {
          const errorsJoined = errorMessages.reduce((p, c) => p + c);
          this.notificationService.showError(errorsJoined);
        } else {
            this.notificationService.showError("Wystąpił błąd");
        }

        return throwError(() => httpError);
      }),
    );
  }
}