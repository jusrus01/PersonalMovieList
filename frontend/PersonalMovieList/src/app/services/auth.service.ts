import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

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
      FirstName: values.firstName, LastName: values.lastName, Username: values.username,
      Email: values.email, Password: values.password})
        .subscribe();
  }

  login(values) : void {

    this.http.post("http://localhost:5000/api/users/token", { 
      FirstName: values.firstName, LastName: values.lastName, Username: values.username,
      Email: values.email, Password: values.password})
        .subscribe((data : any) => this.setToken(data.token));
  }

  setToken(token: string) : void {
    this.jwtToken = token;
    console.log("Received token ", token);
  }

  getToken() : string {
    return this.jwtToken;
  }
}
