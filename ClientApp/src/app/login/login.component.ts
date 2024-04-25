import { Component, OnInit } from '@angular/core';
import { ApiAuthService } from '../services/apiauth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../../styles.css'],
})
export class LoginComponent implements OnInit {

  constructor(public apiauth: ApiAuthService, public router: Router) {

  }

  ngOnInit() {

  }

  login(logInForm: any) {
    this.apiauth.login(logInForm.email, logInForm.password).subscribe(res => {
      if (res.success === 1) this.router.navigate(['/']);
    })
  }
}
