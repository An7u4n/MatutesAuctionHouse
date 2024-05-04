import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { ApiAuthService } from '../services/apiauth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private apiauthservice: ApiAuthService) {
   
  }
  
  canActivate(router: ActivatedRouteSnapshot) {
    const user = this.apiauthservice.userData;
    if (user) {
      return true;
    }
    this.router.navigate(['/login']);
    return false;
  }

}
