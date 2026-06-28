export interface DashboardModes {
  userId: string;
  canUseClientMode: boolean;
  canUseContractorMode: boolean;
  canUseAdminMode: boolean;
  availableModes: string[];
  defaultMode: string;
}

export interface DashboardUserContext {
  userId: string;
  name: string;
  lastName: string;
  fullName: string;
  email: string;
  modes: DashboardModes;
}