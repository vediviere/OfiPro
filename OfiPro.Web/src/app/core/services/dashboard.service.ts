import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { DashboardUserContext } from '../models/dashboard.models';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private readonly apiUrl = `${environment.apiBaseUrl}/api/dashboard`;

  constructor(private readonly http: HttpClient) {}

  getUserContext(): Observable<DashboardUserContext> {
    return this.http.get<DashboardUserContext>(`${this.apiUrl}/me`);
  }
}