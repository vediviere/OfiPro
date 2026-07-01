import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { catchError, map, of } from 'rxjs';

import { UserRole } from '../models/auth.models';
import { AuthService } from '../services/auth.service';

export const roleGuard: CanActivateFn = (route) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const allowedRoles = route.data['roles'] as UserRole[] | undefined;

  if (!allowedRoles || allowedRoles.length === 0) {
    return true;
  }

  if (authService.isAuthenticated()) {
    return validateRole(authService, router, allowedRoles);
  }

  if (!authService.getRefreshToken()) {
    authService.logout();
    return router.createUrlTree(['/login']);
  }

  return authService.refreshToken().pipe(
    map(() => validateRole(authService, router, allowedRoles)),
    catchError(() => {
      authService.logout();
      return of(router.createUrlTree(['/login']));
    }),
  );
};

function validateRole(authService: AuthService, router: Router, allowedRoles: UserRole[]) {
  const currentRole = authService.getUserRole();

  if (currentRole && allowedRoles.includes(currentRole)) {
    return true;
  }

  if (currentRole === 'Cliente') {
    return router.createUrlTree(['/cliente/dashboard']);
  }

  if (currentRole === 'Contratista') {
    return router.createUrlTree(['/contratista/dashboard']);
  }

  if (currentRole === 'Administrador') {
    return router.createUrlTree(['/admin/dashboard']);
  }

  authService.logout();
  return router.createUrlTree(['/login']);
}
