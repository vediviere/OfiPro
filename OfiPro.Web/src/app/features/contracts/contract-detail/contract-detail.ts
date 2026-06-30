import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { UserRole } from '../../../core/models/auth.models';
import { Contract, ContractStatus } from '../../../core/models/contract.models';
import { Evidence, fileTypeOptions } from '../../../core/models/evidence.models';
import { AuthService } from '../../../core/services/auth.service';
import { ContractService } from '../../../core/services/contract.service';
import { EvidenceService } from '../../../core/services/evidence.service';
import { ContractStatusNamePipe } from '../../../core/pipes/contract-status-name.pipe';
import { EvidenceTypeNamePipe } from '../../../core/pipes/evidence-type-name.pipe';

@Component({
  selector: 'app-contract-detail',
  imports: [CommonModule, FormsModule, RouterLink, ContractStatusNamePipe, EvidenceTypeNamePipe],
  templateUrl: './contract-detail.html',
  styleUrl: './contract-detail.css',
})
export class ContractDetail implements OnInit {
  contract: Contract | null = null;
  evidences: Evidence[] = [];
  currentRole: UserRole | null = null;

  readonly fileTypeOptions = fileTypeOptions;

  evidenceForm = {
    evidenceType: 1,
    title: '',
    description: '',
    fileUrl: '',
    fileType: 'image/jpeg',
  };

  isLoading = true;
  isLoadingEvidences = false;
  isProcessing = false;
  isSavingEvidence = false;
  isDeletingEvidence = false;

  errorMessage = '';
  successMessage = '';
  evidenceErrorMessage = '';
  evidenceSuccessMessage = '';

  constructor(
    private readonly route: ActivatedRoute,
    private readonly contractService: ContractService,
    private readonly evidenceService: EvidenceService,
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
    this.loadEvidences(contractId);
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

  canUploadEvidence(): boolean {
    return (
      this.currentRole === 'Contratista' &&
      !!this.contract &&
      this.contract.status !== ContractStatus.PendienteInicio &&
      this.contract.status !== ContractStatus.Finalizado &&
      this.contract.status !== ContractStatus.Cancelado
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

  saveEvidence(): void {
    this.evidenceErrorMessage = '';
    this.evidenceSuccessMessage = '';

    if (!this.contract) {
      this.evidenceErrorMessage = 'No se encontró la contratación.';
      return;
    }

    if (!this.isEvidenceFormValid()) {
      this.evidenceErrorMessage = 'Completa tipo, título, URL válida y tipo de archivo.';
      return;
    }

    this.isSavingEvidence = true;

    this.evidenceService
      .create(this.contract.contractId, {
        evidenceType: Number(this.evidenceForm.evidenceType),
        title: this.evidenceForm.title.trim(),
        description: this.evidenceForm.description.trim(),
        fileUrl: this.evidenceForm.fileUrl.trim(),
        fileType: this.evidenceForm.fileType.trim(),
      })
      .subscribe({
        next: () => {
          this.evidenceSuccessMessage = 'Evidencia agregada correctamente.';
          this.isSavingEvidence = false;
          this.resetEvidenceForm();
          this.loadEvidences(this.contract!.contractId);
        },
        error: (error: HttpErrorResponse) => {
          console.error('Error al agregar evidencia:', error.error);

          this.evidenceErrorMessage =
            this.getBackendErrorMessage(error) ||
            'No se pudo agregar la evidencia. Revisa los datos capturados.';

          this.isSavingEvidence = false;
        },
      });
  }

  deleteEvidence(evidence: Evidence): void {
    if (!confirm('¿Seguro que deseas eliminar esta evidencia?')) {
      return;
    }

    this.evidenceErrorMessage = '';
    this.evidenceSuccessMessage = '';
    this.isDeletingEvidence = true;

    this.evidenceService.delete(evidence.evidenceId).subscribe({
      next: () => {
        this.evidenceSuccessMessage = 'Evidencia eliminada correctamente.';
        this.isDeletingEvidence = false;

        if (this.contract) {
          this.loadEvidences(this.contract.contractId);
        }
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error al eliminar evidencia:', error.error);

        this.evidenceErrorMessage =
          this.getBackendErrorMessage(error) || 'No se pudo eliminar la evidencia.';

        this.isDeletingEvidence = false;
      },
    });
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

  private loadEvidences(contractId: string): void {
    this.isLoadingEvidences = true;

    this.evidenceService.getByContract(contractId).subscribe({
      next: (response) => {
        this.evidences = response;
        this.isLoadingEvidences = false;
      },
      error: () => {
        this.evidenceErrorMessage = 'No se pudieron cargar las evidencias del contrato.';
        this.isLoadingEvidences = false;
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

  private isEvidenceFormValid(): boolean {
    return (
      Number(this.evidenceForm.evidenceType) > 0 &&
      this.evidenceForm.title.trim().length > 0 &&
      this.evidenceForm.fileUrl.trim().length > 0 &&
      this.evidenceForm.fileType.trim().length > 0
    );
  }

  private resetEvidenceForm(): void {
    this.evidenceForm = {
      evidenceType: 1,
      title: '',
      description: '',
      fileUrl: '',
      fileType: 'image/jpeg',
    };
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
