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

import { Customer } from '../../../shared/models/Customer.model';

import { PaginationResponse } from '../../../shared/models/Ticket.models';
  
import { CustomerService } from '../../../shared/services/CustomerService/customer.service';

@Component({
  selector: 'app-customer-list',
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
  templateUrl: './customer-list.html',
  styleUrl: './customer-list.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CustomerListComponent {

  pageIndex = 0;
  pageSize = 10;

  private readonly customerService = inject(CustomerService);

  readonly customers = signal<PaginationResponse<Customer>>({ items: [], totalCount: 0, pageNumber: 1, pageSize: 10, totalPages: 0, hasPreviousPage: false, hasNextPage: false });
  readonly loading = signal(false);
  readonly search = signal('');

  readonly displayedColumns = [
    'name',
    'createdAt'
  ];

  readonly filteredCustomers = computed(() => {
    const search = this.search()
      .trim()
      .toLowerCase();


    return this.customers().items.filter(customer => {

      const matchesSearch =
        !search ||
        customer.name.toLowerCase().includes(search);

      return matchesSearch;
    });
  });

  constructor() {
    this.loadCustomers();
  }

  loadCustomers(): void{
    this.loading.set(true);

    this.customerService.filter(this.pageIndex, this.pageSize).subscribe({
      next: customers => {
        this.customers.set(customers);
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
}