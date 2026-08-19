import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { PaginationResponse } from '@shared/models/Ticket.models';
import { Observable } from 'rxjs';

export interface Customer {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private readonly http = inject(HttpClient);

  filter(
    pageNumber: number,
    pageSize: number,
    name?: string
  ): Observable<PaginationResponse<Customer>> {

    let params = new HttpParams()
     .set('Pagination.PageNumber', pageNumber)
      .set('Pagination.PageSize', pageSize);

    if (name?.trim()) {
      params = params.set('name', name.trim());
    }

    return this.http.get<PaginationResponse<Customer>>(
      '/api/customerManagement/filter',
      { params }
    );
  }
}