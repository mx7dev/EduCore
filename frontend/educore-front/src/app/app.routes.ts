import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { AlumnoList } from './features/alumnos/alumno-list/alumno-list';
import { ProfesorList } from './features/profesores/profesor-list/profesor-list';
import { CursoList } from './features/cursos/curso-list/curso-list';
import { MatriculaList } from './features/matricula/matricula-list/matricula-list';
import { NotaList } from './features/notas/nota-list/nota-list';
import { PeriodoList } from './features/periodos/periodo-list/periodo-list';
import { GradoList } from './features/grados/grado-list/grado-list';
import { SeccionList } from './features/secciones/seccion-list/seccion-list';
import { authGuard } from './core/guards/auth-guard';

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
    path: 'notas',
    component: NotaList,
    canActivate: [authGuard]
  },
  {
    path: 'periodos',
    component: PeriodoList,
    canActivate: [authGuard]
  },
  {
    path: 'grados',
    component: GradoList,
    canActivate: [authGuard]
  },
  {
    path: 'secciones',
    component: SeccionList,
    canActivate: [authGuard]
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];