export interface ProjectRequirement {
  projectRequirementId: string;
  categoryId: string;
  categoryName: string;
  subcategoryId: string;
  subcategoryName: string;
  description: string;
}

export interface Project {
  projectId: string;
  createdByUserId: string;
  createdByUserName: string;
  title: string;
  description: string;
  state: string;
  city: string;
  zone: string | null;
  urgency: number;
  availableMaterials: string | null;
  status: number;
  createdAt: string;
  requirements: ProjectRequirement[];
}

export interface PaginatedResponse<T> {
  items?: T[];
  data?: T[];
  records?: T[];
  totalCount?: number;
  pageNumber?: number;
  pageSize?: number;
  totalPages?: number;
}

export interface CreateProjectRequirementRequest {
  categoryId: string;
  subcategoryId: string;
  description: string;
}

export interface CreateProjectRequest {
  title: string;
  description: string;
  state: string;
  city: string;
  zone: string;
  urgency: number;
  availableMaterials: string;
  requirements: CreateProjectRequirementRequest[];
}

export interface UrgencyOption {
  value: number;
  label: string;
}

export const urgencyOptions: UrgencyOption[] = [
  { value: 1, label: 'Flexible' },
  { value: 2, label: 'Esta semana' },
  { value: 3, label: 'Urgente' },
];