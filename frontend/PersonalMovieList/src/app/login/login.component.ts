import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { AuthService } from './../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    private titleService: Title,
    private authService: AuthService) {}


  ngOnInit(): void {
    this.titleService.setTitle('Personal Movie List - Login');
  }

  creationForm = this.formBuilder.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    username: ['', Validators.required],
    email: ['', Validators.required],
    password: ['', Validators.required]
  });

  login() : void {

    if(this.creationForm.valid) {
      
      var values = this.creationForm.value;
      console.log(values);
      this.authService.login(values);
      this.creationForm.reset();
    }
  }
}
