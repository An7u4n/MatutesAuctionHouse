import { Component, OnInit } from '@angular/core';
import { ApiAuthService } from '../services/apiauth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../../styles.css'],
})
export class LoginComponent implements OnInit {

  constructor(public apiauth: ApiAuthService) {

  }

  ngOnInit() {

  }

  login(logInForm: any) {
    this.apiauth.login(logInForm.email, logInForm.password).subscribe(res => {
      console.log(res);
    })
  }
}
