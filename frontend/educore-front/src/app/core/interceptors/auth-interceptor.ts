import { HttpInterceptorFn, HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth';
import { Router } from '@angular/router';
import { catchError, switchMap, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const token = authService.getToken();

  const reqConToken = token ? req.clone({
    headers: req.headers.set('Authorization', `Bearer ${token}`)
  }) : req;

  return next(reqConToken).pipe(
    catchError((error) => {
      if (error.status === 401) {
        const refreshToken = localStorage.getItem('refreshToken');

        if (refreshToken) {
          // Intentar renovar el token
          return authService.refreshToken(refreshToken).pipe(
            switchMap((response: any) => {
              if (response.success && response.data) {
                authService.guardarSesion(response.data, localStorage.getItem('userName') || '');
                // Reintentar el request original con el nuevo token
                const nuevoReq = req.clone({
                  headers: req.headers.set('Authorization', `Bearer ${response.data.token}`)
                });
                return next(nuevoReq);
              } else {
                authService.logout();
                return throwError(() => error);
              }
            }),
            catchError(() => {
              authService.logout();
              return throwError(() => error);
            })
          );
        } else {
          authService.logout();
        }
      }
      return throwError(() => error);
    })
  );
};