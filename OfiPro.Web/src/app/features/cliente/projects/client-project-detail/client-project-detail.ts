import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { Project } from '../../../../core/models/project.models';
import { ProjectService } from '../../../../core/services/project.service';

@Component({
  selector: 'app-client-project-detail',
  imports: [CommonModule, RouterLink],
  templateUrl: './client-project-detail.html',
  styleUrl: './client-project-detail.css',
})
export class ClientProjectDetail implements OnInit {
  project: Project | null = null;
  isLoading = true;
  errorMessage = '';

  constructor(
    private readonly route: ActivatedRoute,
    private readonly projectService: ProjectService
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

  getStatusName(status: number): string {
    switch (status) {
      case 1:
        return 'Publicado';
      case 2:
        return 'Asignado';
      case 3:
        return 'En proceso';
      case 4:
        return 'Pendiente de confirmación';
      case 5:
        return 'Finalizado';
      case 6:
        return 'Cancelado';
      case 7:
        return 'Expirado';
      default:
        return 'Desconocido';
    }
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

  private loadProject(projectId: string): void {
    this.projectService.getById(projectId).subscribe({
      next: (response) => {
        this.project = response;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'No se pudo cargar el detalle del proyecto.';
        this.isLoading = false;
      },
    });
  }
}