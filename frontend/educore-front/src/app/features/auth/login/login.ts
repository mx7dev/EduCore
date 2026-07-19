import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { InputText } from 'primeng/inputtext';
import { Password } from 'primeng/password';
import { Button } from 'primeng/button';

@Component({
  selector: 'app-login',
  imports: [FormsModule, InputText, Password, Button],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  email = '';
  password = '';
  cargando = false;

  constructor(private router: Router) {}

  login() {
    this.cargando = true;
    setTimeout(() => {
      this.cargando = false;
      this.router.navigate(['/alumnos']);
    }, 1000);
  }
}