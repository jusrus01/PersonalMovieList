import { Component, OnInit } from '@angular/core';
import { waitForAsync } from '@angular/core/testing';
import { FormBuilder, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
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
      console.log(values);
      this.authService.createAccount(values);
      this.creationForm.reset();
      this.router.navigate(['/login']);
    }
  }
}
