// user.service.ts

import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import {
  CreateUserRequest,
  User
} from '../../models/User.model';
import { environment } from '@env/environment.prod';
import { PaginationResponse } from '@shared/models/Ticket.models';

@Injectable({
  providedIn: 'root'
})
export class UserService {

   private readonly apiUrl =
      `${environment.apiUrl}/api/usermanagement/users`;
  private readonly http = inject(HttpClient);

  getUsers(pageNumber: number, pageSize: number): Observable<PaginationResponse<User>> {
    const params = new HttpParams()
      .set('Pagination.PageNumber', pageNumber)
      .set('Pagination.PageSize', pageSize);

    return this.http.get<PaginationResponse<User>>(this.apiUrl, { params });
  }

  createUser(request: CreateUserRequest): Observable<string> {
    return this.http.post<string>(
      this.apiUrl,
      request
    );
  }

  getRoles(): Observable<{ name: string; id: string }[]> {
    return this.http.get<{ name: string; id: string }[]>(
      `${environment.apiUrl}/api/usermanagement/roles`
    );
  }
}