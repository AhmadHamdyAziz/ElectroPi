// user.service.ts

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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

  getUsers(): Observable<PaginationResponse<User>> {
    return this.http.get<PaginationResponse<User>>(this.apiUrl);
  }

  createUser(request: CreateUserRequest): Observable<string> {
    return this.http.post<string>(
      this.apiUrl,
      request
    );
  }
}