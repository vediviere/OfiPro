import { Component, OnInit } from '@angular/core';

import { DashboardService } from '../../../core/services/dashboard.service';
import { DashboardUserContext } from '../../../core/models/dashboard.models';

@Component({
  selector: 'app-cliente-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class ClienteDashboard implements OnInit {
  userContext: DashboardUserContext | null = null;
  isLoading = true;
  errorMessage = '';

  constructor(private readonly dashboardService: DashboardService) {}

  ngOnInit(): void {
    this.loadUserContext();
  }

  private loadUserContext(): void {
    this.dashboardService.getUserContext().subscribe({
      next: (response) => {
        this.userContext = response;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'No se pudo cargar el contexto del usuario.';
        this.isLoading = false;
      }
    });
  }
}