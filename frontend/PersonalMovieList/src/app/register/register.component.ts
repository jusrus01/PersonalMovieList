import { Component, OnInit } from '@angular/core';
import { waitForAsync } from '@angular/core/testing';
import { FormBuilder, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  creationForm = this.formBuilder.group({
    username: ['', Validators.required],
    email: ['', Validators.required],
    password: ['', Validators.required]
  });

  errorMessage? : string;

  constructor(private formBuilder: FormBuilder,
      private titleService: Title,
      private authService: AuthService,
      private router: Router) {}

  ngOnInit(): void {
    this.titleService.setTitle('Personal Movie List - Register');
  }

  createAccount() : void {
    if(this.creationForm.valid) {
      var values = this.creationForm.value;
      this.authService.createAccount(values).pipe(catchError(null)).subscribe(() => {
          this.router.navigate(['/login']);
          });
      
      this.errorMessage = "Email: " + values.email +
          " or username: " + values.username + " is already registered";
      
      this.creationForm.reset();
    } else {
      this.errorMessage = "Please fill in all fields"
    }
  }
}
