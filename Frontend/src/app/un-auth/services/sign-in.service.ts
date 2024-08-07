import {
  HttpClient,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SignIn } from '../models/sign-in';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class SignInService {
  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) {}

  signIn(signin: SignIn): Observable<any> {
    return this.http.post(`${this.baseUrl}/User/signin`, signin, {
      responseType: 'text',
    });
  }

  signUp(signUpData: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/User/signup`, signUpData);
  }

  signOut(): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };
    localStorage.removeItem('token');
    return this.http.post(`${this.baseUrl}/User/signout`, httpOptions);
  }

  storeToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
    localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}
