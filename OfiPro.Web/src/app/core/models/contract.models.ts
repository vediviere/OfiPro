export interface Contract {
  contractId: string;
  proposalId: string;
  projectRequirementId: string;
  clientUserId: string;
  clientName: string;
  contractorUserId: string;
  contractorName: string;
  projectTitle: string;
  requirementDescription: string;
  agreedPrice: number;
  estimatedTime: string;
  status: number;
  createdAt: string;
  startedAt: string | null;
  finishedAt: string | null;
  cancelledAt: string | null;
}

export interface UpdateContractStatusRequest {
  status: number;
}

export const ContractStatus = {
  PendienteInicio: 1,
  EnProceso: 2,
  PendienteConfirmacion: 3,
  Finalizado: 4,
  Cancelado: 5,
} as const;
