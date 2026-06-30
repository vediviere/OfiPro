import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

import { Proposal } from '../../../../core/models/proposal.models';
import { ProposalService } from '../../../../core/services/proposal.service';

@Component({
  selector: 'app-my-proposals',
  imports: [CommonModule, RouterLink],
  templateUrl: './my-proposals.html',
  styleUrl: './my-proposals.css',
})
export class MyProposals implements OnInit {
  proposals: Proposal[] = [];
  isLoading = true;
  errorMessage = '';

  constructor(private readonly proposalService: ProposalService) {}

  ngOnInit(): void {
    this.loadProposals();
  }

  getStatusName(status: number): string {
    switch (status) {
      case 1:
        return 'Pendiente';
      case 2:
        return 'Aceptada';
      case 3:
        return 'Rechazada';
      case 4:
        return 'Retirada';
      default:
        return 'Desconocida';
    }
  }

  private loadProposals(): void {
    this.proposalService.getMyProposals().subscribe({
      next: (response) => {
        this.proposals = response;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'No se pudieron cargar tus propuestas.';
        this.isLoading = false;
      },
    });
  }
}
