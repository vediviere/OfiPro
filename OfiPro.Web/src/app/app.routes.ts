import { Routes } from '@angular/router';

import { PublicLayout } from './layout/public-layout/public-layout';
import { AppLayout } from './layout/app-layout/app-layout';

import { Home } from './features/public/home/home';
import { Login } from './features/auth/login/login';

import { ClienteDashboard } from './features/cliente/dashboard/dashboard';
import { ContratistaDashboard } from './features/contratista/dashboard/dashboard';
import { AdminDashboard } from './features/admin/dashboard/dashboard';

import { ClientProjects } from './features/cliente/projects/client-projects/client-projects';
import { ClientProjectCreate } from './features/cliente/projects/client-project-create/client-project-create';
import { ClientProjectDetail } from './features/cliente/projects/client-project-detail/client-project-detail';

import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
  {
    path: '',
    component: PublicLayout,
    children: [
      {
        path: '',
        component: Home,
      },
      {
        path: 'login',
        component: Login,
      },
    ],
  },
  {
    path: '',
    component: AppLayout,
    children: [
      {
        path: 'cliente/dashboard',
        component: ClienteDashboard,
        canActivate: [authGuard, roleGuard],
        data: {
          roles: ['Cliente'],
        },
      },
      {
        path: 'cliente/proyectos',
        component: ClientProjects,
        canActivate: [authGuard, roleGuard],
        data: {
          roles: ['Cliente'],
        },
      },
      {
        path: 'cliente/proyectos/nuevo',
        component: ClientProjectCreate,
        canActivate: [authGuard, roleGuard],
        data: {
          roles: ['Cliente'],
        },
      },
      {
        path: 'cliente/proyectos/:id',
        component: ClientProjectDetail,
        canActivate: [authGuard, roleGuard],
        data: {
          roles: ['Cliente'],
        },
      },
      {
        path: 'contratista/dashboard',
        component: ContratistaDashboard,
        canActivate: [authGuard, roleGuard],
        data: {
          roles: ['Contratista'],
        },
      },
      {
        path: 'admin/dashboard',
        component: AdminDashboard,
        canActivate: [authGuard, roleGuard],
        data: {
          roles: ['Administrador'],
        },
      },
    ],
  },
  {
    path: '**',
    redirectTo: '',
  },
];
