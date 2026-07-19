import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export interface AlumnoDto {
  id: number;
  codigo: string;
  dni: string;
  nombre: string;
  apellidoPaterno: string;
  apellidoMaterno?: string;
  direccion?: string;
  fechaNacimiento: string;
  numeroCelular?: string;
  correoPersonal?: string;
  correoInstitucional?: string;
}

export interface CrearAlumnoDto {
  dni: string;
  nombre: string;
  apellidoPaterno: string;
  apellidoMaterno?: string;
  direccion?: string;
  fechaNacimiento: string;
  numeroCelular?: string;
  contactoEmergenciaNombre?: string;
  contactoEmergenciaTelefono?: string;
  contactoEmergenciaRelacion?: string;
  correoPersonal?: string;
}

@Injectable({
  providedIn: 'root'
})
export class AlumnoService {
  readonly apiUrl = `${environment.apiUrl}/Alumno`;

  constructor(private http: HttpClient) {}

  // Para operaciones de escritura usamos HttpClient
  crear(dto: CrearAlumnoDto) {
    return this.http.post(this.apiUrl, dto);
  }

  eliminar(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}