import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

import { Project } from '../../../../core/models/project.models';
import { ProjectService } from '../../../../core/services/project.service';

import { ProjectStatusNamePipe } from '../../../../core/pipes/project-status-name.pipe';
import { UrgencyNamePipe } from '../../../../core/pipes/urgency-name.pipe';

@Component({
  selector: 'app-client-projects',
  imports: [CommonModule, RouterLink, ProjectStatusNamePipe, UrgencyNamePipe],
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