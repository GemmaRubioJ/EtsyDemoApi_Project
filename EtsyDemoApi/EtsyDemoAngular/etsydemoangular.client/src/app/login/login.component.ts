// login.component.ts
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../Services/user.service';
import { Router } from '@angular/router';
import { LoginRequest } from '../Models/LoginRequest';
import { UserAuthService } from '../Services/user-auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  credentials: LoginRequest = { email: '', password: '' };

  constructor(private userService: UserService,
              private router: Router,
              private auth: UserAuthService) { }

  onLogin(): void {
    if (this.credentials.email && this.credentials.password) {
      this.userService.login(this.credentials).subscribe({
        next: response => {
          console.log('Login successful', response);
          // Manejo de la respuesta exitosa aquí
          this.auth.login(response.token, response.data.username);
          this.router.navigate(['/']);
        },
        error: error => {
          console.error('Login failed', error);
          // Manejo de errores aquí
        }
      });
    }
  }
}
