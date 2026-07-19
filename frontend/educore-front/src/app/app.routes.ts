import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { AlumnoList } from './features/alumnos/alumno-list/alumno-list';
import { authGuard } from  './core/guards/auth-guard';

export const routes: Routes = [
  {
    path: 'login',
    component: Login
  },
  {
    path: 'alumnos',
    component: AlumnoList,
    canActivate: [authGuard]
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];