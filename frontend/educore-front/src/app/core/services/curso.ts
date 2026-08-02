import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export interface CursoDto {
  id: number;
  codigo: string;
  nombre: string;
  descripcion?: string;
  activo: boolean;
}

export interface CrearCursoDto {
  nombre: string;
  descripcion?: string;
}

@Injectable({
  providedIn: 'root'
})
export class CursoService {
  readonly apiUrl = `${environment.apiUrl}/Curso`;

  constructor(private http: HttpClient) {}

  crear(dto: CrearCursoDto) {
    return this.http.post(this.apiUrl, dto);
  }

  eliminar(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}