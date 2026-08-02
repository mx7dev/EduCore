import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export interface MatriculaDto {
  id: number;
  alumnoId: number;
  nombreAlumno: string;
  codigoAlumno: string;
  seccionId: number;
  nombreSeccion: string;
  fechaMatricula: string;
  activo: boolean;
}

export interface CrearMatriculaDto {
  alumnoId: number;
  seccionId: number;
}

@Injectable({
  providedIn: 'root'
})
export class MatriculaService {
  readonly apiUrl = `${environment.apiUrl}/Matricula`;

  constructor(private http: HttpClient) {}

  crear(dto: CrearMatriculaDto) {
    return this.http.post(this.apiUrl, dto);
  }

  anular(id: number) {
    return this.http.patch(`${this.apiUrl}/${id}/anular`, {});
  }
}