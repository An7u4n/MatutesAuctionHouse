import { Component } from '@angular/core';
import { ApiAuthService } from '../services/apiauth.service';
import { Router } from '@angular/router';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {

  constructor(public apiauth: ApiAuthService, public router: Router, private apiservice: ApiService) {
    const user = this.apiauth.userData;
    if (user) {
      this.router.navigate(['/']);
    }
  }

  register(userForm: any) {
    this.apiservice.postUser(userForm.value.user_name, userForm.value.password, userForm.value.email).subscribe(res => {
      alert("User Created");
    }, err => console.error(err));
  }
}
