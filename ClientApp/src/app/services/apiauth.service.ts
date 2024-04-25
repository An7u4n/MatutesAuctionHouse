import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, map } from "rxjs";
import { Response } from '../models/models';

interface User {
  user_id: number,
  user_name: string,
  email: string,
  token: string
}

const httpOption = {
  headers: new HttpHeaders({
    'Contend-Type': 'application/json'
  })
}


@Injectable({ providedIn: 'root' })
export class ApiAuthService {
  url: string = 'https://localhost:7268/api/Users/login';
  private _usuarioSubject: BehaviorSubject<User>;

  public get userData(): User {
    return this._usuarioSubject.value;
  }

  constructor(private _http: HttpClient) {
    const storedUser = localStorage.getItem('user');
    this._usuarioSubject = new BehaviorSubject<User>(storedUser ? JSON.parse(storedUser) : null);
  }

  login(email: string, password: string): Observable<Response> {
    return this._http.post<Response>(this.url, { email, password }, httpOption).pipe(
      map(res => {
        if (res.success === 1) {
          const user: User = res.data as User;
          localStorage.setItem('user', JSON.stringify(user));
          this._usuarioSubject.next(user);
        }
        return res;
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
  }
}
