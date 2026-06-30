export interface Proposal {
  proposalId: string;
  projectRequirementId: string;
  projectId: string;
  projectTitle: string;
  requirementDescription: string;
  contractorUserId: string;
  contractorName: string;
  price: number;
  estimatedTime: string;
  includesMaterials: boolean;
  scopeDescription: string;
  includes: string;
  doesNotInclude: string;
  hasWarranty: boolean;
  warrantyDescription: string;
  comment: string;
  status: number;
  statusName: string;
  createdAt: string;
}

export interface CreateProposalRequest {
  projectRequirementId: string;
  price: number;
  estimatedTime: string;
  includesMaterials: boolean;
  scopeDescription: string;
  includes: string;
  doesNotInclude: string;
  hasWarranty: boolean;
  warrantyDescription: string;
  comment: string;
}
