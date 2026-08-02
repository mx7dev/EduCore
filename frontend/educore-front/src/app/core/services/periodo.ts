import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export interface PeriodoDto {
  id: number;
  anio: number;
  descripcion?: string;
  activo: boolean;
}

export interface CrearPeriodoDto {
  anio: number;
  descripcion?: string;
}

@Injectable({
  providedIn: 'root'
})
export class PeriodoService {
  readonly apiUrl = `${environment.apiUrl}/Periodo`;

  constructor(private http: HttpClient) {}

  crear(dto: CrearPeriodoDto) {
    return this.http.post(this.apiUrl, dto);
  }
}