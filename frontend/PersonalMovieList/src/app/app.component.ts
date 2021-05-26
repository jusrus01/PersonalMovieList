import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { title } from 'process';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  public isMenuCollapsed = true;
  title = 'Personal Movie List - Home';

  constructor(private titleService: Title) {
    
  }

  ngOnInit() {
    this.titleService.setTitle(this.title);
  }

}
