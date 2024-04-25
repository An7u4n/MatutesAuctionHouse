import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {

  }
  // TODO 25/04 make navigation works
  canActivate(router: ActivatedRouteSnapshot) {
    this.router.navigate(['/login']);
    return false;
  }

}
