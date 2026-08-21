import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService2 } from './auth.service';

export const authGuard = (route?: ActivatedRouteSnapshot, state?: RouterStateSnapshot) => {
  const auth = inject(AuthService2);
  const router = inject(Router);

  return auth.check() ? true : router.parseUrl('/auth/login');
};
