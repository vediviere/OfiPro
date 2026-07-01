import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { finalize, Observable, shareReplay, tap } from 'rxjs';

import { environment } from '../../../environments/environment';
import { AuthResponse, LoginRequest, RefreshTokenRequest, UserRole } from '../models/auth.models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly apiUrl = `${environment.apiBaseUrl}/api/auth`;

  private readonly tokenKey = 'ofipro_token';
  private readonly refreshTokenKey = 'ofipro_refresh_token';
  private readonly userKey = 'ofipro_user';

  private refreshTokenRequest$: Observable<AuthResponse> | null = null;

  constructor(private readonly http: HttpClient) {}

  login(request: LoginRequest): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${this.apiUrl}/login`, request)
      .pipe(tap((response) => this.saveSession(response)));
  }

  refreshToken(): Observable<AuthResponse> {
    if (this.refreshTokenRequest$) {
      return this.refreshTokenRequest$;
    }

    const refreshToken = this.getRefreshToken();

    const request: RefreshTokenRequest = {
      refreshToken: refreshToken ?? '',
    };

    this.refreshTokenRequest$ = this.http
      .post<AuthResponse>(`${this.apiUrl}/refresh-token`, request)
      .pipe(
        tap((response) => this.saveSession(response)),
        finalize(() => {
          this.refreshTokenRequest$ = null;
        }),
        shareReplay(1),
      );

    return this.refreshTokenRequest$;
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.refreshTokenKey);
    localStorage.removeItem(this.userKey);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(this.refreshTokenKey);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();

    if (!token) {
      return false;
    }

    const expiration = this.getTokenExpiration(token);

    if (!expiration) {
      return false;
    }

    return expiration > new Date();
  }

  getUserRole(): UserRole | null {
    const roles = this.getUserRoles();

    if (roles.includes('Administrador')) {
      return 'Administrador';
    }

    if (roles.includes('Contratista')) {
      return 'Contratista';
    }

    if (roles.includes('Cliente')) {
      return 'Cliente';
    }

    return null;
  }

  getUserRoles(): UserRole[] {
    const token = this.getToken();

    if (!token) {
      return [];
    }

    const payload = this.decodeTokenPayload(token);

    if (!payload) {
      return [];
    }

    const roleClaim =
      payload['role'] ?? payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

    const roles = Array.isArray(roleClaim) ? roleClaim : [roleClaim];

    return roles.filter(
      (role): role is UserRole =>
        role === 'Cliente' || role === 'Contratista' || role === 'Administrador',
    );
  }

  private saveSession(response: AuthResponse): void {
    localStorage.setItem(this.tokenKey, response.token);

    if (response.refreshToken) {
      localStorage.setItem(this.refreshTokenKey, response.refreshToken);
    }

    localStorage.setItem(
      this.userKey,
      JSON.stringify({
        userId: response.userId,
        fullName: response.fullName,
        email: response.email,
        expiresAt: response.expiresAt,
        refreshTokenExpiresAt: response.refreshTokenExpiresAt,
      }),
    );
  }

  private getTokenExpiration(token: string): Date | null {
    const payload = this.decodeTokenPayload(token);

    const expiration = payload?.['exp'];

    if (!expiration) {
      return null;
    }

    return new Date(expiration * 1000);
  }

  private decodeTokenPayload(token: string): Record<string, any> | null {
    try {
      const payload = token.split('.')[1];
      const normalizedPayload = payload.replace(/-/g, '+').replace(/_/g, '/');
      const decodedPayload = atob(normalizedPayload);

      return JSON.parse(decodedPayload);
    } catch {
      return null;
    }
  }
}
