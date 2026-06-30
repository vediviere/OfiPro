import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

import { UserRole } from '../../../core/models/auth.models';
import { Contract } from '../../../core/models/contract.models';
import { AuthService } from '../../../core/services/auth.service';
import { ContractService } from '../../../core/services/contract.service';

import { ContractStatusNamePipe } from '../../../core/pipes/contract-status-name.pipe';

@Component({
  selector: 'app-my-contracts',
  imports: [CommonModule, RouterLink, ContractStatusNamePipe],
  templateUrl: './my-contracts.html',
  styleUrl: './my-contracts.css',
})
export class MyContracts implements OnInit {
  contracts: Contract[] = [];
  currentRole: UserRole | null = null;

  isLoading = true;
  errorMessage = '';

  constructor(
    private readonly contractService: ContractService,
    private readonly authService: AuthService,
  ) {}

  ngOnInit(): void {
    this.currentRole = this.authService.getUserRole();
    this.loadContracts();
  }

  getCounterpartLabel(): string {
    return this.currentRole === 'Cliente' ? 'Contratista' : 'Cliente';
  }

  getCounterpartName(contract: Contract): string {
    return this.currentRole === 'Cliente' ? contract.contractorName : contract.clientName;
  }

  private loadContracts(): void {
    this.contractService.getMyContracts().subscribe({
      next: (response) => {
        this.contracts = response;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'No se pudieron cargar tus contratos.';
        this.isLoading = false;
      },
    });
  }
}
