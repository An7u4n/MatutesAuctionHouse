import { Component } from '@angular/core';
import { ApiAuthService } from '../services/apiauth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {

  constructor(public apiauth: ApiAuthService, public router: Router) {
    const user = this.apiauth.userData;
    if (user) {
      this.router.navigate(['/']);
    }
  }

  login(logInForm: any) {
    this.apiauth.login(logInForm.email, logInForm.password).subscribe(res => {
      if (res.success === 1) this.router.navigate(['/']);
      location.reload();
    })
  }
}
