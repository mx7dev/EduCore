import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { AlumnoList } from './features/alumnos/alumno-list/alumno-list';

export const routes: Routes = [
  {
    path: 'login',
    component: Login
  },
  {
    path: 'alumnos',
    component: AlumnoList
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];