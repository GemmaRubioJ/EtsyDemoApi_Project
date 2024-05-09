import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterUserRequest } from '../Models/RegisterUserRequest';
import { LoginRequest } from '../Models/LoginRequest';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://localhost:7088/api/Etsy/register';
  private apiUrlLogin = 'https://localhost:7088/api/Etsy/login';
  constructor(private http: HttpClient) { }

  registerUser(userData: RegisterUserRequest): Observable<any> {
    return this.http.post(this.apiUrl, userData);
  }

  login(credentials: LoginRequest): Observable<any> {
    return this.http.post<any>(this.apiUrlLogin, credentials);
  }
}
