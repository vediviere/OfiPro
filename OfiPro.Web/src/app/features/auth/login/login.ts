import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { LoginRequest, UserRole } from '../../../core/models/auth.models';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  credentials: LoginRequest = {
    email: '',
    password: ''
  };

  isLoading = false;
  errorMessage = '';

  constructor(
    private readonly authService: AuthService,
    private readonly router: Router
  ) {}

  login(): void {
    this.errorMessage = '';

    if (!this.credentials.email || !this.credentials.password) {
      this.errorMessage = 'Ingresa correo y contraseña.';
      return;
    }

    this.isLoading = true;

    this.authService.login(this.credentials).subscribe({
      next: () => {
        const role = this.authService.getUserRole();
        this.redirectByRole(role);
      },
      error: () => {
        this.errorMessage = 'Correo o contraseña incorrectos.';
        this.isLoading = false;
      }
    });
  }

  private redirectByRole(role: UserRole | null): void {
    this.isLoading = false;

    if (role === 'Cliente') {
      this.router.navigate(['/cliente/dashboard']);
      return;
    }

    if (role === 'Contratista') {
      this.router.navigate(['/contratista/dashboard']);
      return;
    }

    if (role === 'Administrador') {
      this.router.navigate(['/admin/dashboard']);
      return;
    }

    this.errorMessage = 'No se pudo identificar el rol del usuario.';
  }
}