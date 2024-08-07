import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize, tap } from 'rxjs/operators';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('token')!;
  const authReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`,
    },
  });

  const spinnerService = inject(NgxSpinnerService);
  return next(authReq).pipe(
    tap(() => {
      spinnerService.show();
    }),
    finalize(() => {
      spinnerService.hide();
    })
  );
};
