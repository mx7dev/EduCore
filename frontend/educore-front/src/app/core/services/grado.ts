import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export interface GradoDto {
  id: number;
  numero: number;
  nivel: string;
}

export interface CrearGradoDto {
  numero: number;
  nivel: number;
}

@Injectable({
  providedIn: 'root'
})
export class GradoService {
  readonly apiUrl = `${environment.apiUrl}/Grado`;

  constructor(private http: HttpClient) {}

  crear(dto: CrearGradoDto) {
    return this.http.post(this.apiUrl, dto);
  }
}