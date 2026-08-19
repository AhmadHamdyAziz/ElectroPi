import { Injectable } from '@angular/core';

import { AuthUser } from './auth.models';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {

  private readonly accessTokenKey = 'electropi.accessToken';
  private readonly userKey = 'electropi.user';

  getAccessToken(): string | null {
    return localStorage.getItem(this.accessTokenKey);
  }

  setAccessToken(token: string): void {
    localStorage.setItem(this.accessTokenKey, token);
  }

  getUser(): AuthUser | null {
    const value = localStorage.getItem(this.userKey);

    if (!value || value === 'undefined') {
      return null;
    }

    try {
      return JSON.parse(value) as AuthUser;
    } catch {
      localStorage.removeItem(this.userKey);
      return null;
    }
  }

  setUser(user: AuthUser): void {
    localStorage.setItem(
      this.userKey,
      JSON.stringify(user)
    );
  }

  clear(): void {
    localStorage.removeItem(this.accessTokenKey);
    localStorage.removeItem(this.userKey);
  }
}