import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AuthService } from './../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    private titleService: Title,
    private authService: AuthService,
    private router: Router) {}


  ngOnInit(): void {
    this.titleService.setTitle('Personal Movie List - Login');
  }

  creationForm = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  });

  errorMessage? : string;

  login() : void {

    if(this.creationForm.valid) {
      var values = this.creationForm.value;

      this.authService.login(values)
        .subscribe((data : any) => {
          if(data.isAuthenticated) { 
            this.authService.setSession(data.token);
            this.router.navigate(['/']);
          } else {
            // display error message
            this.errorMessage = data.message;
          }
          this.creationForm.reset();
        });
    } else {
      this.errorMessage = "Invalid email or password";
    }
  }
}
