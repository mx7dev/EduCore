import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { AlumnoList } from './features/alumnos/alumno-list/alumno-list';
import { authGuard } from './core/guards/auth-guard';
import { ProfesorList } from './features/profesores/profesor-list/profesor-list';
import { CursoList } from './features/cursos/curso-list/curso-list';
import { MatriculaList } from './features/matricula/matricula-list/matricula-list';

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
    path: 'profesores',
    component: ProfesorList,
    canActivate: [authGuard]
  },
  {
    path: 'cursos',
    component: CursoList,
    canActivate: [authGuard]
  },
  {
  path: 'matricula',
  component: MatriculaList,
  canActivate: [authGuard]
},
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];