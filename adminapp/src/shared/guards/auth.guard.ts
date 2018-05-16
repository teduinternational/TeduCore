import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { SystemConstants } from '@shared/common/system.constants';
import { UrlConstants } from '@shared/common/url.constants';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private router: Router) {

    }
    canActivate(activateRoute: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): boolean {
        if (localStorage.getItem(SystemConstants.CURRENT_USER)) {
            return true;
        }
        else {
            this.router.navigate([UrlConstants.LOGIN], {
                queryParams: {
                    returnUrl: routerState.url
                }
            });
            return false;
        }
    }
}