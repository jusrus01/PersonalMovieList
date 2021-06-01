import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

// might need to not let user access other pages
// after successful login
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private jwtToken: string;

  constructor(private http: HttpClient) {

    this.jwtToken = '';
  }

  createAccount(values) : void {

    this.http.post("http://localhost:5000/api/users/register", { 
      Username: values.username, Email: values.email, Password: values.password })
        .subscribe();
    console.log("values have been sent");
  }

  login(values) : Observable<any> {

    // this.http.post("http://localhost:5000/api/users/login", { 
    //   Username: values.username, Email: values.email, Password: values.password})
    //     .subscribe((data : any) => this.setToken(data.token));
    return this.http.post("http://localhost:5000/api/users/login", { 
      Username: values.username, Email: values.email, Password: values.password });
  }

  setToken(token: string) : void {
    this.jwtToken = token;
    console.log("Received token ", token);
  }

  logOut() : void {

    localStorage.removeItem('token');
  }

  isLoggedIn() : boolean {
    
    if(localStorage.getItem('token') == null) {

      return false;
    }

    return true;
  }

  getToken() : string {
    return this.jwtToken;
  }

  setSession(token: string) : void {

    localStorage.setItem('token', token);
    console.log("Set token: ", token);
  }
}
