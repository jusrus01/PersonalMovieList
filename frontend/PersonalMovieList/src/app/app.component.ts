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

  isLoggedIn() : boolean {

    return this.authService.isLoggedIn();
  }

  logOut() : void {

    this.authService.logOut();
    this.router.navigate(['/login']);
  }
}
