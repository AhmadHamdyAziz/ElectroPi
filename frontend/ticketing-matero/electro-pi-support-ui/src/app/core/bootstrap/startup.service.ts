import { Injectable, inject } from '@angular/core';
import { AuthService2, User } from '@core/authentication';
import { NgxPermissionsService, NgxRolesService } from 'ngx-permissions';
import { switchMap, tap } from 'rxjs';
import { Menu, MenuService } from './menu.service';

@Injectable({
  providedIn: 'root',
})
export class StartupService {
  private readonly authService = inject(AuthService2);
  private readonly menuService = inject(MenuService);
  private readonly permissonsService = inject(NgxPermissionsService);
  private readonly rolesService = inject(NgxRolesService);

  /**
   * Load the application only after get the menu or other essential informations
   * such as permissions and roles.
   */
load() {
  return new Promise<void>((resolve, reject) => {
    this.authService
      .change()
      .pipe(
        tap(user => {
          console.log('AUTH CHANGE:', user);
          this.setPermissions(user);
        }),
        switchMap(() => {
          console.log('CALLING AUTH MENU');
          return this.authService.menu();
        }),
        tap(menu => {
          console.log('MENU RECEIVED:', menu);
          this.setMenu(menu);
        })
      )
      .subscribe({
        next: () => resolve(),
        error: error => {
          console.error('STARTUP ERROR:', error);
          resolve();
        },
      });
  });
}

  private setMenu(menu: Menu[]) {
    //this.menuService.addNamespace(menu, 'menu');
    this.menuService.set(menu);
  }

  private setPermissions(user: User) {
    // In a real app, you should get permissions and roles from the user information.
    const permissions = ['canAdd', 'canDelete', 'canEdit', 'canRead'];
    this.permissonsService.loadPermissions(permissions);
    this.rolesService.flushRoles();
    this.rolesService.addRoles({ ADMIN: permissions });

    // Tips: Alternatively you can add permissions with role at the same time.
    // this.rolesService.addRolesWithPermissions({ ADMIN: permissions });
  }
}
