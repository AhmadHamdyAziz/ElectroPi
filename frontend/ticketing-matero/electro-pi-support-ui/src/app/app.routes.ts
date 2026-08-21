import { Routes } from '@angular/router';
import { AdminLayout } from '@theme/admin-layout/admin-layout';
import { AuthLayout } from '@theme/auth-layout/auth-layout';
import { Dashboard } from './routes/dashboard/dashboard';
import { Error403 } from './routes/sessions/error-403';
import { Error404 } from './routes/sessions/error-404';
import { Error500 } from './routes/sessions/error-500';
import { authGuard } from './core/guards/auth-guard';
import { UserListComponent } from './features/users/user-list/user-list';
import { UserFormComponent } from './features/users/user-form/user-form';
import { CustomerListComponent } from './features/customers/customer-list/customer-list';
import { CreateCustomerComponent } from './features/customers/create-customer/create-customer';

export const routes: Routes = [
  {
    path: '',
    component: AdminLayout,
    canActivate: [authGuard],
    canActivateChild: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: Dashboard },
      {
        path: 'tickets',
        children: [
          {
            path: '',
            redirectTo: 'list',
            pathMatch: 'full'
          },
          {
            path: 'list',
            loadComponent: () =>
              import('./features/tickets/ticket-list/ticket-list')
                .then(m => m.TicketList)
          },
          {
            path: 'details2/:id',
            loadComponent: () =>
              import('./features/tickets/ticket-details/ticket-details')
                .then(m => m.TicketDetails)
          },    
          {
            path: 'details/:id',
            loadComponent: () =>
              import('./features/tickets/ticket-details-new/ticket-details-new')
                .then(m => m.TicketDetailsNew)
          },
          {
            path: 'create',
            loadComponent: () =>
              import('./features/tickets/create-ticket/create-ticket')
                .then(m => m.TicketCreate)
          }
        ]
      },
      {
        path: 'admin/users',
        canActivate: [authGuard],
        children: [
          {
            path: '',
            component: UserListComponent
          },
          {
            path: 'new',
            component: UserFormComponent
          }
        ]
      },
      {
        path: 'admin/customers',
        canActivate: [authGuard],
        children: [
          {
            path: '',
            component: CustomerListComponent
          },
          {
            path: 'new',
            component: CreateCustomerComponent
          }
        ]
      },
      { path: '403', component: Error403 },
      { path: '404', component: Error404 },
      { path: '500', component: Error500 },
    ],
  },

  {
    path: 'auth',
    children: [
      {
        path: 'login',
        loadComponent: () =>
          import('./features/auth/login/login')
            .then(m => m.Login)
      }
    ]
  },
  { path: '**', redirectTo: 'dashboard' },
];
