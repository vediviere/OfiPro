import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';

import { Project } from '../../../../core/models/project.models';
import { Proposal } from '../../../../core/models/proposal.models';
import { ProjectService } from '../../../../core/services/project.service';
import { ProposalService } from '../../../../core/services/proposal.service';

import { ProjectStatusNamePipe } from '../../../../core/pipes/project-status-name.pipe';
import { ProposalStatusNamePipe } from '../../../../core/pipes/proposal-status-name.pipe';
import { UrgencyNamePipe } from '../../../../core/pipes/urgency-name.pipe';

@Component({
  selector: 'app-client-project-detail',
  imports: [CommonModule, RouterLink, ProjectStatusNamePipe, ProposalStatusNamePipe, UrgencyNamePipe],
  templateUrl: './client-project-detail.html',
  styleUrl: './client-project-detail.css',
})
export class ClientProjectDetail implements OnInit {
  project: Project | null = null;
  proposalsByRequirement: Record<string, Proposal[]> = {};

  isLoading = true;
  isLoadingProposals = false;
  isProcessingProposal = false;

  errorMessage = '';
  successMessage = '';

  constructor(
    private readonly route: ActivatedRoute,
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


  getRequirementProposals(projectRequirementId: string): Proposal[] {
    return this.proposalsByRequirement[projectRequirementId] ?? [];
  }

  acceptProposal(proposal: Proposal): void {
    if (!confirm('¿Seguro que deseas aceptar esta propuesta?')) {
      return;
    }

    this.errorMessage = '';
    this.successMessage = '';
    this.isProcessingProposal = true;

    this.proposalService.accept(proposal.proposalId).subscribe({
      next: (response) => {
        this.successMessage = response.message || 'Propuesta aceptada correctamente.';
        this.isProcessingProposal = false;
        this.reloadProposals();
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error al aceptar propuesta:', error.error);

        this.errorMessage =
          this.getBackendErrorMessage(error) || 'No se pudo aceptar la propuesta.';

        this.isProcessingProposal = false;
      },
    });
  }

  rejectProposal(proposal: Proposal): void {
    if (!confirm('¿Seguro que deseas rechazar esta propuesta?')) {
      return;
    }

    this.errorMessage = '';
    this.successMessage = '';
    this.isProcessingProposal = true;

    this.proposalService.reject(proposal.proposalId).subscribe({
      next: (response) => {
        this.successMessage = response.message || 'Propuesta rechazada correctamente.';
        this.isProcessingProposal = false;
        this.reloadProposals();
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error al rechazar propuesta:', error.error);

        this.errorMessage =
          this.getBackendErrorMessage(error) || 'No se pudo rechazar la propuesta.';

        this.isProcessingProposal = false;
      },
    });
  }

  private loadProject(projectId: string): void {
    this.projectService.getById(projectId).subscribe({
      next: (response) => {
        this.project = response;
        this.isLoading = false;
        this.loadProposalsForRequirements(response);
      },
      error: () => {
        this.errorMessage = 'No se pudo cargar el detalle del proyecto.';
        this.isLoading = false;
      },
    });
  }

  private reloadProposals(): void {
    if (!this.project) {
      return;
    }

    this.loadProposalsForRequirements(this.project);
  }

  private loadProposalsForRequirements(project: Project): void {
    if (project.requirements.length === 0) {
      return;
    }

    this.isLoadingProposals = true;

    const requests = project.requirements.map((requirement) =>
      this.proposalService.getByProjectRequirement(requirement.projectRequirementId),
    );

    forkJoin(requests).subscribe({
      next: (responses) => {
        const result: Record<string, Proposal[]> = {};

        project.requirements.forEach((requirement, index) => {
          result[requirement.projectRequirementId] = responses[index];
        });

        this.proposalsByRequirement = result;
        this.isLoadingProposals = false;
      },
      error: () => {
        this.errorMessage = 'No se pudieron cargar las propuestas recibidas.';
        this.isLoadingProposals = false;
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
      return Object.values(error.error.errors).flat().join(' ');
    }

    return '';
  }
}
