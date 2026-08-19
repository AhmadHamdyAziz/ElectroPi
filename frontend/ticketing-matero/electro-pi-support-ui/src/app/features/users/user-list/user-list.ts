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

import {
  User,
  UserRole
} from '../../../shared/models/User.model';

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
    MatProgressSpinnerModule
  ],
  templateUrl: './user-list.html',
  styleUrl: './user-list.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserListComponent {

  private readonly userService = inject(UserService);

  readonly users = signal<User[]>([]);
  readonly loading = signal(false);
  readonly search = signal('');
  readonly selectedRole = signal<UserRole | 'All'>('All');

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

    const role = this.selectedRole();

    return this.users().filter(user => {

      const matchesSearch =
        !search ||
        user.email.toLowerCase().includes(search);

      const matchesRole =
        role === 'All' ||
        user.role === role;

      return matchesSearch && matchesRole;
    });
  });

  constructor() {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loading.set(true);

    this.userService.getUsers().subscribe({
      next: users => {
        this.users.set(users);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
      }
    });
  }

  onSearch(value: string): void {
    this.search.set(value);
  }

  onRoleChange(role: UserRole | 'All'): void {
    this.selectedRole.set(role);
  }
}