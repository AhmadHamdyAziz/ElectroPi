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

import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CreateCustomerRequest } from '@shared/models/Customer.model';

@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatProgressSpinnerModule
],
  templateUrl: './create-customer.html',
  styleUrl: './create-customer.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreateCustomerComponent {

  private readonly customerService = inject(CustomerService);
  private readonly router = inject(Router);
  
  readonly name = signal('');

  readonly saving = signal(false);

  onSubmit(): void {

    if (!this.name().trim()) {
      return;
    }

    const request: CreateCustomerRequest = {
      name: this.name().trim()
    };

    this.saving.set(true);

    this.customerService
      .createCustomer(request)
      .subscribe({
        next: () => {
          this.router.navigate(['/admin/customers']);
        },
        error: () => {
          this.saving.set(false);
        }
      });
  }

  cancel(): void {
    this.router.navigate(['/admin/customers']);
  }
}