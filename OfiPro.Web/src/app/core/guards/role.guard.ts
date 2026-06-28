import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserRole } from '../models/auth.models';

export const roleGuard: CanActivateFn = (route) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const allowedRoles = route.data['roles'] as UserRole[] | undefined;
  const currentRole = authService.getUserRole();

  if (!allowedRoles || allowedRoles.length === 0) {
    return true;
  }

  if (currentRole && allowedRoles.includes(currentRole)) {
    return true;
  }

  if (currentRole === 'Cliente') {
    router.navigate(['/cliente/dashboard']);
    return false;
  }

  if (currentRole === 'Contratista') {
    router.navigate(['/contratista/dashboard']);
    return false;
  }

  if (currentRole === 'Administrador') {
    router.navigate(['/admin/dashboard']);
    return false;
  }

  authService.logout();
  router.navigate(['/login']);

  return false;
};