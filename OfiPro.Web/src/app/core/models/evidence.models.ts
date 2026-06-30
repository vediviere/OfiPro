export interface Evidence {
  evidenceId: string;
  contractId: string;
  uploadedByUserId: string;
  uploadedByUserName: string;
  title: string;
  description: string;
  fileUrl: string;
  fileType: string;
  createdAt: string;
  evidenceType: number;
}

export interface CreateEvidenceRequest {
  evidenceType: number;
  title: string;
  description: string;
  fileUrl: string;
  fileType: string;
}

export const EvidenceType = {
  Antes: 1,
  Durante: 2,
  Despues: 3,
} as const;

export const fileTypeOptions = [
  { value: 'image/jpeg', label: 'Imagen JPEG' },
  { value: 'image/png', label: 'Imagen PNG' },
  { value: 'application/pdf', label: 'PDF' },
];
