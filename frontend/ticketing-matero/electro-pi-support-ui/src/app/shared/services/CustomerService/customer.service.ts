import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '@env/environment';
import { PaginationResponse } from '@shared/models/Ticket.models';
import { Observable } from 'rxjs';
import { Customer } from '../../models/Customer.model';
import { CreateCustomerRequest } from '@shared/models/Customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

     private readonly apiUrl =
        `${environment.apiUrl}/api/customerManagement`;

  createCustomer(request: CreateCustomerRequest): Observable<string> {
    return this.http.post<string>(
      `${this.apiUrl}`,
      request
    );
  }
  private readonly http = inject(HttpClient);

  filter(
    pageNumber: number,
    pageSize: number,
    name?: string
  ): Observable<PaginationResponse<Customer>> {

    let params = new HttpParams()
     .set('Pagination.PageNumber', pageNumber + 1)
      .set('Pagination.PageSize', pageSize);

    if (name?.trim()) {
      params = params.set('name', name.trim());
    }

    return this.http.get<PaginationResponse<Customer>>(
      `${this.apiUrl}/filter`,
      { params }
    );
  }
}