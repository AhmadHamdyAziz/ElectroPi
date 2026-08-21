import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
  signal
} from '@angular/core';

import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';

import {
  User,
  RoleName
} from '../../../shared/models/User.model';

import { PaginationResponse } from '../../../shared/models/Ticket.models';
  
import { UserService } from '../../../shared/services/UserService/user.service';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [
    DatePipe,
    RouterLink,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatPaginatorModule
  ],
  templateUrl: './user-list.html',
  styleUrl: './user-list.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserListComponent {

  pageIndex = 0;
  pageSize = 10;

  private readonly userService = inject(UserService);

  readonly users = signal<PaginationResponse<User>>({ items: [], totalCount: 0, pageNumber: 1, pageSize: 10, totalPages: 0, hasPreviousPage: false, hasNextPage: false });
  readonly loading = signal(false);
  readonly totalUsers = signal(0);
  readonly search = signal('');
  readonly selectedRole = signal<RoleName | 'All'>('All');

  readonly displayedColumns = [
    'email',
    'role',
    'customer',
    'createdAt'
  ];

  readonly filteredUsers = computed(() => {
    const search = this.search()
      .trim()
      .toLowerCase();

    const selectedRole = this.selectedRole();

    return this.users().items.filter(user => {

      const matchesSearch =
        !search ||
        user.email.toLowerCase().includes(search);

      const matchesRole =
        selectedRole === 'All' ||
        user.roleName === selectedRole;

      return matchesSearch && matchesRole;
    });
  });

  constructor() {
    this.loadUsers();
  }

  loadUsers(): void{
    this.loading.set(true);

    this.userService.getUsers(this.pageIndex + 1, this.pageSize).subscribe({
      next: users => {
        this.users.set(users);
        this.totalUsers.set(users.totalCount);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
      }
    });
    this.loading.set(false);
  }

  onSearch(value: string): void {
    this.search.set(value);
  }

  onRoleChange(role: RoleName | 'All'): void {
    this.selectedRole.set(role);
  }

    onPageChange(event: PageEvent): void {

    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;

    this.loadUsers();
  }
}