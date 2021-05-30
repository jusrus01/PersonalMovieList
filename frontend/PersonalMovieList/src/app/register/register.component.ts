import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  creationForm = this.formBuilder.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    username: ['', Validators.required],
    email: ['', Validators.required],
    password: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder,
      private titleService: Title,
      private authService: AuthService) {}

  ngOnInit(): void {
    this.titleService.setTitle('Personal Movie List - Register');
  }

  createAccount() : void {

    if(this.creationForm.valid) {
      
      var values = this.creationForm.value;
      console.log(values);
      this.authService.createAccount(values);
      this.creationForm.reset();
    }
  }
}
