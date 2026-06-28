import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

import { Project } from '../../../../core/models/project.models';
import { ProjectService } from '../../../../core/services/project.service';

@Component({
  selector: 'app-client-projects',
  imports: [CommonModule, RouterLink],
  templateUrl: './client-projects.html',
  styleUrl: './client-projects.css',
})
export class ClientProjects implements OnInit {
  projects: Project[] = [];
  isLoading = true;
  errorMessage = '';

  constructor(private readonly projectService: ProjectService) {}

  ngOnInit(): void {
    this.loadProjects();
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

  private loadProjects(): void {
    this.projectService.getMyProjects().subscribe({
      next: (response) => {
        this.projects = response;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'No se pudieron cargar tus proyectos.';
        this.isLoading = false;
      },
    });
  }
}