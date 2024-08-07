import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import {
  HttpClient,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private httpHeaders: any;
  private baseURL: string = environment.baseUrl;

  constructor(private httpClient: HttpClient) {
    this.httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
    });
  }

  get<T>(path: string, params: HttpParams = new HttpParams()): Observable<T> {
    return this.httpClient
      .get<T>(`${this.baseURL}${path}`, { headers: this.httpHeaders, params })
      .pipe(catchError(this.handleError));
  }

  getById<T>(
    path: string,
    params: HttpParams = new HttpParams()
  ): Observable<T> {
    return this.httpClient
      .get<T>(`${this.baseURL}${path}`, { headers: this.httpHeaders, params })
      .pipe(catchError(this.handleError));
  }

  post<T>(
    path: string,
    body: object = {},
    params: HttpParams = new HttpParams()
  ): Observable<T> {
    return this.httpClient
      .post<T>(`${this.baseURL}${path}`, JSON.stringify(body), {
        headers: this.httpHeaders,
        params,
      })
      .pipe(catchError(this.handleError));
  }

  put<T>(
    path: string,
    body: object = {},
    params: HttpParams = new HttpParams()
  ): Observable<T> {
    return this.httpClient
      .put<T>(`${this.baseURL}${path}`, JSON.stringify(body), {
        headers: this.httpHeaders,
        params,
      })
      .pipe(catchError(this.handleError));
  }

  delete<T>(
    path: string,
    params: HttpParams = new HttpParams()
  ): Observable<T> {
    return this.httpClient
      .delete<T>(`${this.baseURL}${path}`, {
        headers: this.httpHeaders,
        params,
      })
      .pipe(catchError(this.handleError));
  }

  deleteById<T>(
    path: string,
    params: HttpParams = new HttpParams()
  ): Observable<T> {
    return this.httpClient
      .delete<T>(`${this.baseURL}${path}`, {
        headers: this.httpHeaders,
        params,
      })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: any) {
    return throwError(() => new Error(error));
  }
}
