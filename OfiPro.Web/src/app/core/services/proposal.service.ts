import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { PaginatedResponse } from '../models/project.models';
import { CreateProposalRequest, Proposal } from '../models/proposal.models';

@Injectable({
  providedIn: 'root',
})
export class ProposalService {
  private readonly apiUrl = `${environment.apiBaseUrl}/api/proposals`;

  constructor(private readonly http: HttpClient) {}

  create(request: CreateProposalRequest): Observable<Proposal> {
    return this.http.post<Proposal>(this.apiUrl, request);
  }

  getMyProposals(): Observable<Proposal[]> {
    return this.http
      .get<Proposal[] | PaginatedResponse<Proposal>>(`${this.apiUrl}/my-proposals`)
      .pipe(
        map((response) => {
          if (Array.isArray(response)) {
            return response;
          }

          return response.items ?? response.data ?? response.records ?? [];
        }),
      );
  }

  getByProjectRequirement(projectRequirementId: string): Observable<Proposal[]> {
    return this.http.get<Proposal[]>(`${this.apiUrl}/requirement/${projectRequirementId}`);
  }

  accept(proposalId: string): Observable<{ message: string }> {
    return this.http.patch<{ message: string }>(`${this.apiUrl}/${proposalId}/accept`, {});
  }

  reject(proposalId: string): Observable<{ message: string }> {
    return this.http.patch<{ message: string }>(`${this.apiUrl}/${proposalId}/reject`, {});
  }
}
