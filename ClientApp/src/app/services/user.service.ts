import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url: string = 'https://localhost:7268/api/'

  constructor(private _http: HttpClient) { }

  // User Methods
  getUser(id: number): Observable<User> {
    return this._http.get<User>(this.url + 'User/' + id)
  }

  getImage(id: number) {
    return this._http.get(this.url + 'Users/' + id + '/image', { responseType: 'blob' });
  }

  postUser(user_name: string, email: string, password: string): Observable<any> {
    return this._http.post<any>(this.url + 'Users', {
      user_name: user_name,
      email: email,
      password: password,
    });
  }

  deleteUser(user_id: number): Observable<any> {
    return this._http.delete<any>(this.url + 'Users/' + user_id);
  }
}
