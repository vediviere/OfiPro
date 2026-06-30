import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { UserRole } from '../../../core/models/auth.models';
import { Contract, ContractStatus } from '../../../core/models/contract.models';
import { AuthService } from '../../../core/services/auth.service';
import { ContractService } from '../../../core/services/contract.service';

import { ContractStatusNamePipe } from '../../../core/pipes/contract-status-name.pipe';

@Component({
  selector: 'app-contract-detail',
  imports: [CommonModule, RouterLink, ContractStatusNamePipe],
  templateUrl: './contract-detail.html',
  styleUrl: './contract-detail.css',
})
export class ContractDetail implements OnInit {
  contract: Contract | null = null;
  currentRole: UserRole | null = null;

  isLoading = true;
  isProcessing = false;

  errorMessage = '';
  successMessage = '';

  constructor(
    private readonly route: ActivatedRoute,
    private readonly contractService: ContractService,
    private readonly authService: AuthService,
  ) {}

  ngOnInit(): void {
    this.currentRole = this.authService.getUserRole();

    const contractId = this.route.snapshot.paramMap.get('id');

    if (!contractId) {
      this.errorMessage = 'No se encontró el identificador del contrato.';
      this.isLoading = false;
      return;
    }

    this.loadContract(contractId);
  }

  canStart(): boolean {
    return (
      this.currentRole === 'Contratista' && this.contract?.status === ContractStatus.PendienteInicio
    );
  }

  canSendToConfirmation(): boolean {
    return this.currentRole === 'Contratista' && this.contract?.status === ContractStatus.EnProceso;
  }

  canFinish(): boolean {
    return (
      this.currentRole === 'Cliente' &&
      this.contract?.status === ContractStatus.PendienteConfirmacion
    );
  }

  canCancel(): boolean {
    return (
      !!this.contract &&
      this.contract.status !== ContractStatus.Finalizado &&
      this.contract.status !== ContractStatus.Cancelado &&
      (this.currentRole === 'Cliente' || this.currentRole === 'Contratista')
    );
  }

  startContract(): void {
    this.updateStatus(ContractStatus.EnProceso, '¿Seguro que deseas iniciar esta contratación?');
  }

  sendToConfirmation(): void {
    this.updateStatus(
      ContractStatus.PendienteConfirmacion,
      '¿Seguro que deseas marcar esta contratación como pendiente de confirmación?',
    );
  }

  finishContract(): void {
    this.updateStatus(ContractStatus.Finalizado, '¿Seguro que deseas finalizar esta contratación?');
  }

  cancelContract(): void {
    this.updateStatus(ContractStatus.Cancelado, '¿Seguro que deseas cancelar esta contratación?');
  }

  private loadContract(contractId: string): void {
    this.contractService.getById(contractId).subscribe({
      next: (response) => {
        this.contract = response;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'No se pudo cargar el detalle del contrato.';
        this.isLoading = false;
      },
    });
  }

  private updateStatus(newStatus: number, confirmationMessage: string): void {
    if (!this.contract) {
      return;
    }

    if (!confirm(confirmationMessage)) {
      return;
    }

    this.errorMessage = '';
    this.successMessage = '';
    this.isProcessing = true;

    this.contractService.updateStatus(this.contract.contractId, { status: newStatus }).subscribe({
      next: (response) => {
        this.successMessage = response.message || 'Estado actualizado correctamente.';

        this.isProcessing = false;
        this.loadContract(this.contract!.contractId);
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error al actualizar contrato:', error.error);

        this.errorMessage =
          this.getBackendErrorMessage(error) || 'No se pudo actualizar el estado del contrato.';

        this.isProcessing = false;
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
      const validationErrors = Object.values(error.error.errors) as string[][];
      return validationErrors.flat().join(' ');
    }

    return '';
  }
}
