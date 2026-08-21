import { computed, inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

import { Router, RouterLink } from '@angular/router';

import {
  AuthUser,
  LoginRequest,
  LoginResponse,
  UserRole
} from './auth.models';

import { TokenStorageService } from './token-storage';

import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly http = inject(HttpClient);
  private readonly tokenStorage = inject(TokenStorageService);

  private readonly router = inject(Router);

  private readonly currentUser = signal<AuthUser | null>(
    this.tokenStorage.getUser()
  );

  readonly user = this.currentUser.asReadonly();

  readonly isAuthenticated = computed(
    () => this.tokenStorage.getAccessToken() !== null
  );

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
        `${environment.apiUrl}/api/auth/login`,
        request
      )
      .pipe(
        tap(response => {
          this.tokenStorage.setAccessToken(
            response.accessToken
          );

          this.tokenStorage.setUser(
            response.user
          );

          this.currentUser.set(
            response.user
          );
        })
      );
  }

  logout(): void {
    this.tokenStorage.clear();
    this.currentUser.set(null);
    this.router.navigate(['/auth/login']);
  }

  hasRole(role: UserRole): boolean {
    return this.currentUser()?.role === role;
  }

  hasAnyRole(roles: UserRole[]): boolean {
    const currentRole = this.currentUser()?.role;

    return currentRole !== undefined &&
      roles.includes(currentRole);
  }
}