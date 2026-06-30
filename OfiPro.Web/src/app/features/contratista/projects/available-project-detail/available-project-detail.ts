import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { CreateProposalRequest } from '../../../../core/models/proposal.models';
import { Project } from '../../../../core/models/project.models';
import { ProjectService } from '../../../../core/services/project.service';
import { ProposalService } from '../../../../core/services/proposal.service';

@Component({
  selector: 'app-available-project-detail',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './available-project-detail.html',
  styleUrl: './available-project-detail.css',
})
export class AvailableProjectDetail implements OnInit {
  project: Project | null = null;

  form = {
    projectRequirementId: '',
    price: 0,
    estimatedTime: '',
    includesMaterials: false,
    scopeDescription: '',
    includes: '',
    doesNotInclude: '',
    hasWarranty: false,
    warrantyDescription: '',
    comment: '',
  };

  isLoading = true;
  isSaving = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly projectService: ProjectService,
    private readonly proposalService: ProposalService,
  ) {}

  ngOnInit(): void {
    const projectId = this.route.snapshot.paramMap.get('id');

    if (!projectId) {
      this.errorMessage = 'No se encontró el identificador del proyecto.';
      this.isLoading = false;
      return;
    }

    this.loadProject(projectId);
  }

  getUrgencyName(urgency: number): string {
    switch (urgency) {
      case 1:
        return 'Flexible';
      case 2:
        return 'Esta semana';
      case 3:
        return 'Urgente';
      default:
        return 'No especificada';
    }
  }

  saveProposal(): void {
    this.errorMessage = '';
    this.successMessage = '';

    if (!this.isFormValid()) {
      this.errorMessage = 'Completa los campos obligatorios antes de enviar la propuesta.';
      return;
    }

    const request: CreateProposalRequest = {
      projectRequirementId: this.form.projectRequirementId,
      price: Number(this.form.price),
      estimatedTime: this.form.estimatedTime.trim(),
      includesMaterials: this.form.includesMaterials,
      scopeDescription: this.form.scopeDescription.trim(),
      includes: this.form.includes.trim(),
      doesNotInclude: this.form.doesNotInclude.trim(),
      hasWarranty: this.form.hasWarranty,
      warrantyDescription: this.form.warrantyDescription.trim(),
      comment: this.form.comment.trim(),
    };

    this.isSaving = true;

    this.proposalService.create(request).subscribe({
      next: () => {
        this.successMessage = 'Propuesta enviada correctamente.';
        this.router.navigate(['/contratista/propuestas']);
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error al enviar propuesta:', error.error);

        this.errorMessage =
          this.getBackendErrorMessage(error) ||
          'No se pudo enviar la propuesta. Revisa los datos capturados.';

        this.isSaving = false;
      },
    });
  }

  private loadProject(projectId: string): void {
    this.projectService.getById(projectId).subscribe({
      next: (response) => {
        this.project = response;

        if (response.requirements.length > 0) {
          this.form.projectRequirementId = response.requirements[0].projectRequirementId;
        }

        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'No se pudo cargar el detalle del proyecto.';
        this.isLoading = false;
      },
    });
  }

  private isFormValid(): boolean {
    return (
      this.form.projectRequirementId.trim().length > 0 &&
      Number(this.form.price) > 0 &&
      this.form.estimatedTime.trim().length >= 3 &&
      this.form.scopeDescription.trim().length >= 20
    );
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
      return Object.values(error.error.errors).flat().join(' ');
    }

    return '';
  }
}
