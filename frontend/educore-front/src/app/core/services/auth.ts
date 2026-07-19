import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  refreshToken: string;
  expiracion: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/Auth`;
  
  isAuthenticated = signal(false);
  currentUser = signal<string | null>(null);

  constructor(
    private http: HttpClient,
    private router: Router
  ) {
    // Verificar si hay token al iniciar
    const token = localStorage.getItem('token');
    if (token) {
      this.isAuthenticated.set(true);
      this.currentUser.set(localStorage.getItem('userName'));
    }
  }

  login(request: LoginRequest) {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, request);
  }

  guardarSesion(response: LoginResponse, userName: string) {
    localStorage.setItem('token', response.token);
    localStorage.setItem('refreshToken', response.refreshToken);
    localStorage.setItem('userName', userName);
    this.isAuthenticated.set(true);
    this.currentUser.set(userName);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('userName');
    this.isAuthenticated.set(false);
    this.currentUser.set(null);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}