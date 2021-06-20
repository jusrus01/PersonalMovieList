import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  createAccount(values) : Observable<any> {
    return this.http.post("http://localhost:5000/api/users/register", { 
      Username: values.username, Email: values.email, Password: values.password });
  }

  login(values) : Observable<any> {
    return this.http.post("http://localhost:5000/api/users/login", { 
      Username: values.username, Email: values.email, Password: values.password });
  }

  logOut() : void {
    localStorage.removeItem('token');
  }

  isLoggedIn() : boolean {
    if(localStorage.getItem('token') == null ||
        localStorage.getItem('token') == 'null') {
      return false;
    }
    return true;
  }

  getToken() : string {
    return localStorage.getItem('token');
  }

  setSession(token: string) : void {
    localStorage.setItem('token', token);
  }
}
