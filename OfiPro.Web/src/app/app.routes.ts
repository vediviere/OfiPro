import { Routes } from '@angular/router';

import { PublicLayout } from './layout/public-layout/public-layout';
import { AppLayout } from './layout/app-layout/app-layout';

import { Home } from './features/public/home/home';
import { Login } from './features/auth/login/login';

import { ClienteDashboard } from './features/cliente/dashboard/dashboard';
import { ContratistaDashboard } from './features/contratista/dashboard/dashboard';
import { AdminDashboard } from './features/admin/dashboard/dashboard';

import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
  {
    path: '',
    component: PublicLayout,
    children: [
      {
        path: '',
        component: Home
      },
      {
        path: 'login',
        component: Login
      }
    ]
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
          roles: ['Cliente']
        }
      },
      {
        path: 'contratista/dashboard',
        component: ContratistaDashboard,
        canActivate: [authGuard, roleGuard],
        data: {
          roles: ['Contratista']
        }
      },
      {
        path: 'admin/dashboard',
        component: AdminDashboard,
        canActivate: [authGuard, roleGuard],
        data: {
          roles: ['Administrador']
        }
      }
    ]
  },
  {
    path: '**',
    redirectTo: ''
  }
];