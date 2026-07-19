import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { InputText } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { AuthService } from '../../../core/services/auth';
import { MessageService } from 'primeng/api';
import { Toast } from 'primeng/toast';

@Component({
  selector: 'app-login',
  imports: [FormsModule, InputText, PasswordModule, ButtonModule, Toast],
  providers: [MessageService],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  email = '';
  password = '';
  cargando = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private messageService: MessageService
  ) {}

  login() {
    if (!this.email || !this.password) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Atención',
        detail: 'Ingresa tu correo y contraseña'
      });
      return;
    }

    this.cargando = true;
    this.authService.login({ email: this.email, password: this.password })
      .subscribe({
        next: (response) => {
          this.authService.guardarSesion(response, this.email);
          this.cargando = false;
          this.router.navigate(['/alumnos']);
        },
        error: (err) => {
          this.cargando = false;
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: err.error?.error || 'Credenciales incorrectas'
          });
        }
      });
  }
}