import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

import { AuthService } from '../../core/services/auth.service';
import { UserRole } from '../../core/models/auth.models';

@Component({
  selector: 'app-app-layout',
  imports: [RouterLink, RouterOutlet],
  templateUrl: './app-layout.html',
  styleUrl: './app-layout.css',
})
export class AppLayout {
  constructor(
    private readonly authService: AuthService,
    private readonly router: Router
  ) {}

  hasRole(role: UserRole): boolean {
    return this.authService.getUserRoles().includes(role);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}