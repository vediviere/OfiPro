import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { PaginatedResponse } from '../models/project.models';
import { Contract, UpdateContractStatusRequest } from '../models/contract.models';

@Injectable({
  providedIn: 'root',
})
export class ContractService {
  private readonly apiUrl = `${environment.apiBaseUrl}/api/contracts`;

  constructor(private readonly http: HttpClient) {}

  getMyContracts(): Observable<Contract[]> {
    return this.http.get<Contract[] | PaginatedResponse<Contract>>(`${this.apiUrl}/mine`).pipe(
      map((response) => {
        if (Array.isArray(response)) {
          return response;
        }

        return response.items ?? response.data ?? response.records ?? [];
      }),
    );
  }

  getById(contractId: string): Observable<Contract> {
    return this.http.get<Contract>(`${this.apiUrl}/${contractId}`);
  }

  updateStatus(
    contractId: string,
    request: UpdateContractStatusRequest,
  ): Observable<{ message: string }> {
    return this.http.patch<{ message: string }>(`${this.apiUrl}/${contractId}/status`, request);
  }
}
