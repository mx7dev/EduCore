import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export interface ProfesorDto {
  id: number;
  dni: string;
  nombre: string;
  apellidoPaterno: string;
  apellidoMaterno?: string;
  especialidad: string;
  correoElectronico: string;
  direccion?: string;
  fechaNacimiento?: string;
  numeroCelular?: string;
  activo: boolean;
}

export interface CrearProfesorDto {
  dni: string;
  nombre: string;
  apellidoPaterno: string;
  apellidoMaterno?: string;
  especialidad: string;
  correoElectronico: string;
  direccion?: string;
  fechaNacimiento?: string;
  numeroCelular?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ProfesorService {
  readonly apiUrl = `${environment.apiUrl}/Profesor`;

  constructor(private http: HttpClient) {}

  crear(dto: CrearProfesorDto) {
    return this.http.post(this.apiUrl, dto);
  }

  eliminar(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}