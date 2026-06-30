import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

import { Project } from '../../../../core/models/project.models';
import { ProjectService } from '../../../../core/services/project.service';

import { UrgencyNamePipe } from '../../../../core/pipes/urgency-name.pipe';

@Component({
  selector: 'app-available-projects',
  imports: [CommonModule, RouterLink, UrgencyNamePipe],
  templateUrl: './available-projects.html',
  styleUrl: './available-projects.css',
})
export class AvailableProjects implements OnInit {
  projects: Project[] = [];
  isLoading = true;
  errorMessage = '';

  constructor(private readonly projectService: ProjectService) {}

  ngOnInit(): void {
    this.loadProjects();
  }

  private loadProjects(): void {
    this.projectService.getAvailableProjects().subscribe({
      next: (response) => {
        this.projects = response;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'No se pudieron cargar los proyectos disponibles.';
        this.isLoading = false;
      },
    });
  }
}
