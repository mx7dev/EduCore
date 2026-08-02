import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export interface NotaDto {
  id: number;
  matriculaId: number;
  nombreAlumno: string;
  codigoAlumno: string;
  cursoId: number;
  nombreCurso: string;
  bimestre: string;
  calificacion: number;
  comentario?: string;
}

export interface LibretaDto {
  nombreAlumno: string;
  codigoAlumno: string;
  cursos: NotaCursoDto[];
}

export interface NotaCursoDto {
  nombreCurso: string;
  b1?: number;
  b2?: number;
  b3?: number;
  b4?: number;
  notaFinal?: number;
}

export interface RegistrarNotaDto {
  matriculaId: number;
  cursoId: number;
  bimestre: number;
  calificacion: number;
  comentario?: string;
}

@Injectable({
  providedIn: 'root'
})
export class NotaService {
  readonly apiUrl = `${environment.apiUrl}/Nota`;

  constructor(private http: HttpClient) {}

  getLibretaUrl(matriculaId: number) {
    return `${this.apiUrl}/libreta/${matriculaId}`;
  }

  registrar(dto: RegistrarNotaDto) {
    return this.http.post(this.apiUrl, dto);
  }
}