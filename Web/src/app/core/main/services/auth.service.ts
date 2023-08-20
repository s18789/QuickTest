import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, tap } from 'rxjs';
import { Api } from 'src/app/shared/utils/api';
import { AuthDataModel } from '../models/authDataModel';
import { AuthResponseDto } from '../models/authResponseDto';
import { AuthServiceInterface } from '../models/authServiceInterface';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements AuthServiceInterface {
  private readonly apiUrl = `${Api.LOCAL_URL}Users`;
  private _access: boolean = false;

  public get access(): boolean {
    return this._access;
  }

  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private router: Router,
  ) {
   this.isUserAuthenticated();
  }

  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");
    this._access = !!token && !this.jwtHelper.isTokenExpired(token);

    return this._access;
  }

  public isUserTeacher = (): boolean => {
    const token = localStorage.getItem("token");
    const decodedToken = this.jwtHelper.decodeToken(<string>token);
    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']

    return role === 'teacher';
  }

  logged(): void {
    const teken = localStorage.getItem('token') || '';
    this.http.get<AuthResponseDto>(this.apiUrl, { headers: { authorization: teken } }).subscribe((resp: any) => {
      if (!resp.errorMessage && !!resp.isAuthSuccessful) {
        this._access = true
      }
    })
  }

  logIn(body: AuthDataModel): Observable<AuthResponseDto> {
    return this.http.post<AuthResponseDto>(`${this.apiUrl}/Login`, body).pipe(
      tap((response) => {
        this._access = response.isAuthSuccessful;
        localStorage.setItem('token', response.token);
        localStorage.setItem('userId', response.userId);
      })
    );
  }

  logOut(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    this._access = false;
    this.router.navigate(['']);
  }
}
