import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { CreateEvidenceRequest, Evidence } from '../models/evidence.models';

@Injectable({
  providedIn: 'root',
})
export class EvidenceService {
  private readonly apiUrl = `${environment.apiBaseUrl}/api`;

  constructor(private readonly http: HttpClient) {}

  getByContract(contractId: string): Observable<Evidence[]> {
    return this.http.get<Evidence[]>(`${this.apiUrl}/contracts/${contractId}/evidences`);
  }

  create(contractId: string, request: CreateEvidenceRequest): Observable<Evidence> {
    return this.http.post<Evidence>(`${this.apiUrl}/contracts/${contractId}/evidences`, request);
  }

  delete(evidenceId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/evidences/${evidenceId}`);
  }
}
