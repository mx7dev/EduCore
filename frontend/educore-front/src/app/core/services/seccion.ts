import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export interface SeccionDto {
  id: number;
  nombre: string;
  turno: string;
  numeroGrado: number;
  nivelGrado: string;
  anioPeriodo: number;
  activo: boolean;
}

export interface CrearSeccionDto {
  nombre: string;
  turno: number;
  gradoId: number;
  periodoId: number;
}

@Injectable({
  providedIn: 'root'
})
export class SeccionService {
  readonly apiUrl = `${environment.apiUrl}/Seccion`;

  constructor(private http: HttpClient) {}

  crear(dto: CrearSeccionDto) {
    return this.http.post(this.apiUrl, dto);
  }
}