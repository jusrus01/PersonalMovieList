import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { title } from 'process';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  public isMenuCollapsed = true;
  title = 'Personal Movie List - Home';

  constructor(private titleService: Title,
    private authService: AuthService,
    private router: Router) {
  }

  ngOnInit() {
    this.titleService.setTitle(this.title);
  }

  getToken() : string {

    return this.authService.getToken();
  }

  logOut() : void {

    this.authService.setToken('');
    this.router.navigate(['/login']);
  }
}
