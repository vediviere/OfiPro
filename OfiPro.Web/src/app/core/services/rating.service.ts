import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { CreateRatingRequest, Rating } from '../models/rating.models';

@Injectable({
  providedIn: 'root',
})
export class RatingService {
  private readonly apiUrl = `${environment.apiBaseUrl}/api`;

  constructor(private readonly http: HttpClient) {}

  getByContract(contractId: string): Observable<Rating[]> {
    return this.http.get<Rating[]>(`${this.apiUrl}/contracts/${contractId}/ratings`);
  }

  create(contractId: string, request: CreateRatingRequest): Observable<Rating> {
    return this.http.post<Rating>(`${this.apiUrl}/contracts/${contractId}/ratings`, request);
  }
}
