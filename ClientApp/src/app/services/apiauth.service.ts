import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Response } from '../models/models';

const httpOption = {
  headers: new HttpHeaders({
    'Contend-Type': 'application/json'
  })
}

@Injectable({ providedIn: 'root' })
export class ApiAuthService {
  url: string = 'https://localhost:7268/api/Users/login';
  constructor(private _http: HttpClient) {
    
  }

  login(email: string, password: string): Observable<Response> {
    return this._http.post<Response>(this.url, { email, password }, httpOption);
  }
}
