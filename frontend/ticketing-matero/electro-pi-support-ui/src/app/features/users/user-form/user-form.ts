import {
  debounceTime,
  distinctUntilChanged,
  finalize,
  switchMap
} from 'rxjs';

import {
  FormControl,
  ReactiveFormsModule
} from '@angular/forms';

import { MatAutocompleteModule, MatAutocomplete } from '@angular/material/autocomplete';

import { Customer } from '../../../shared/models/Customer.model';
import { CustomerService } from '../../../shared/services/CustomerService/customer.service';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
  signal
} from '@angular/core';

import { Router } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

import {
  Role,
  CreateUserRequest
} from '../../../shared/models/User.model';

import { UserService } from '../../../shared/services/UserService/user.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatAutocomplete,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatProgressSpinnerModule
],
  templateUrl: './user-form.html',
  styleUrl: './user-form.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserFormComponent {

  private readonly userService = inject(UserService);
  private readonly router = inject(Router);

  readonly email = signal('');
  readonly password = signal('');
  readonly selectedRole = signal<Role | null>(null);
  readonly customerId = signal('');

  readonly roles = signal<Role[]>([]);

  readonly isCustomer = computed(
    () => this.selectedRole()?.name === 'Customer'
  );

  readonly saving = signal(false);

  onSubmit(): void {

    if (
      !this.email().trim() ||
      !this.password()
    ) {
      return;
    }

    if (
      this.selectedRole()?.name === 'Customer' &&
      !this.customerId()
    ) {
      return;
    }

    const request: CreateUserRequest = {
      email: this.email().trim(),
      password: this.password(),
      roleId: this.selectedRole()?.id!,
      customerId: this.isCustomer()
        ? this.customerId()
        : undefined
    };

    this.saving.set(true);

    this.userService
      .createUser(request)
      .subscribe({
        next: () => {
          this.router.navigate(['/admin/users']);
        },
        error: () => {
          this.saving.set(false);
        }
      });
  }

  cancel(): void {
    this.router.navigate(['/admin/users']);
  }

  private readonly customerService = inject(CustomerService);

readonly customerSearch = new FormControl('');

readonly customers = signal<Customer[]>([]);
readonly loadingCustomers = signal(false);

constructor() {
  this.loadRoles();
  this.setupCustomerSearch();
}

private setupCustomerSearch(): void {

  this.customerSearch.valueChanges
    .pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(value => {

        this.loadingCustomers.set(true);

        return this.customerService
          .filter(0, 20, value ?? '')
          .pipe(
            finalize(() =>
              this.loadingCustomers.set(false)
            )
          );
      })
    )
    .subscribe(response => {
      this.customers.set(response.items);
    });

  }

  onCustomerSelected(customer: Customer): void {
    this.customerId.set(customer.id);
  }

  displayCustomer(customer: Customer | null): string {
    return customer?.name ?? '';
  }

  loadRoles(): void {
    this.userService.getRoles().subscribe({
      next: roles => {
        if (roles.length > 0) {
          this.roles.set(roles);
          this.selectedRole.set(roles[0]);
        }
      }
    });
  }
}