import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import {
  CategoryOption,
  SubcategoryOption,
} from '../models/catalog.models';

@Injectable({
  providedIn: 'root',
})
export class CatalogService {
  private readonly apiUrl = `${environment.apiBaseUrl}/api/catalog`;

  constructor(private readonly http: HttpClient) {}

  getCategories(): Observable<CategoryOption[]> {
    return this.http.get<CategoryOption[]>(`${this.apiUrl}/categories`);
  }

  getSubcategoriesByCategoryId(categoryId: string): Observable<SubcategoryOption[]> {
    return this.http.get<SubcategoryOption[]>(
      `${this.apiUrl}/categories/${categoryId}/subcategories`
    );
  }
}