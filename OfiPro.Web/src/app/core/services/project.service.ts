import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import {
  CreateProjectRequest,
  PaginatedResponse,
  Project,
} from '../models/project.models';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {
  private readonly apiUrl = `${environment.apiBaseUrl}/api/projects`;

  constructor(private readonly http: HttpClient) {}

  getMyProjects(): Observable<Project[]> {
    return this.http
      .get<Project[] | PaginatedResponse<Project>>(`${this.apiUrl}/my-projects`)
      .pipe(
        map((response) => {
          if (Array.isArray(response)) {
            return response;
          }

          return response.items ?? response.data ?? response.records ?? [];
        })
      );
  }

  getById(projectId: string): Observable<Project> {
    return this.http.get<Project>(`${this.apiUrl}/${projectId}`);
  }

  create(request: CreateProjectRequest): Observable<Project> {
    return this.http.post<Project>(this.apiUrl, request);
  }
}