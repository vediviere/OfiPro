import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { CreateProjectRequest, urgencyOptions } from '../../../../core/models/project.models';
import { ProjectService } from '../../../../core/services/project.service';

import { CategoryOption, SubcategoryOption } from '../../../../core/models/catalog.models';
import { CatalogService } from '../../../../core/services/catalog.service';

@Component({
  selector: 'app-client-project-create',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './client-project-create.html',
  styleUrl: './client-project-create.css',
})
export class ClientProjectCreate implements OnInit {
  readonly urgencyOptions = urgencyOptions;

  categories: CategoryOption[] = [];
  subcategories: SubcategoryOption[] = [];

  isLoadingCategories = true;
  isLoadingSubcategories = false;

  form = {
    title: '',
    description: '',
    state: 'Puebla',
    city: '',
    zone: '',
    urgency: 1,
    availableMaterials: '',
    categoryId: '',
    subcategoryId: '',
    requirementDescription: '',
  };

  isSaving = false;
  errorMessage = '';

  constructor(
    private readonly projectService: ProjectService,
    private readonly catalogService: CatalogService,
    private readonly router: Router,
  ) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  onCategoryChange(): void {
    this.form.subcategoryId = '';
    this.subcategories = [];

    if (!this.form.categoryId) {
      return;
    }

    this.loadSubcategories(this.form.categoryId);
  }

  private loadCategories(): void {
    this.catalogService.getCategories().subscribe({
      next: (response) => {
        this.categories = response;
        this.isLoadingCategories = false;
      },
      error: () => {
        this.errorMessage = 'No se pudieron cargar las categorías.';
        this.isLoadingCategories = false;
      },
    });
  }

  private loadSubcategories(categoryId: string): void {
    this.isLoadingSubcategories = true;

    this.catalogService.getSubcategoriesByCategoryId(categoryId).subscribe({
      next: (response) => {
        this.subcategories = response;
        this.isLoadingSubcategories = false;
      },
      error: () => {
        this.errorMessage = 'No se pudieron cargar las subcategorías.';
        this.isLoadingSubcategories = false;
      },
    });
  }

  save(): void {
    this.errorMessage = '';

    if (!this.isFormValid()) {
      this.errorMessage = 'Completa los campos obligatorios antes de guardar.';
      return;
    }

    const request: CreateProjectRequest = {
      title: this.form.title.trim(),
      description: this.form.description.trim(),
      state: this.form.state.trim(),
      city: this.form.city.trim(),
      zone: this.form.zone.trim(),
      urgency: Number(this.form.urgency),
      availableMaterials: this.form.availableMaterials.trim(),
      requirements: [
        {
          categoryId: this.form.categoryId.trim(),
          subcategoryId: this.form.subcategoryId.trim(),
          description: this.form.requirementDescription.trim(),
        },
      ],
    };

    this.isSaving = true;

    this.projectService.create(request).subscribe({
      next: (createdProject) => {
        this.router.navigate(['/cliente/proyectos', createdProject.projectId]);
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error al crear proyecto:', error.error);

        this.errorMessage =
          this.getBackendErrorMessage(error) ||
          'No se pudo crear el proyecto. Revisa los datos capturados.';

        this.isSaving = false;
      },
    });
  }

  private getBackendErrorMessage(error: HttpErrorResponse): string {
    if (!error.error) {
      return '';
    }

    if (typeof error.error === 'string') {
      return error.error;
    }

    if (error.error.message) {
      return error.error.message;
    }

    if (error.error.errors) {
      const validationErrors = Object.values(error.error.errors).flat().join(' ');

      return validationErrors;
    }

    return '';
  }

  private isFormValid(): boolean {
    return (
      this.form.title.trim().length >= 5 &&
      this.form.description.trim().length >= 20 &&
      this.form.state.trim().length > 0 &&
      this.form.city.trim().length > 0 &&
      this.form.categoryId.trim().length > 0 &&
      this.form.subcategoryId.trim().length > 0 &&
      this.form.requirementDescription.trim().length >= 10
    );
  }
}
