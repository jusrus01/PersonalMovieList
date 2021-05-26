import { NgModule } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { MoviesComponent } from './movies/movies.component';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MovieCreateModalComponent } from './movies/movie-create-modal/movie-create-modal.component';

export const routes: Routes = [
  { path: "login", component: LoginComponent, data: { title: 'Login'} },
  { path: "register", component: RegisterComponent, data: { title: 'Register'}},
  { path: '', component: HomeComponent, data: { title: 'Home'}},
  { path: '**', component: HomeComponent, data: { title: 'Home'}}
]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    MoviesComponent,
    MovieCreateModalComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule.forRoot(routes),
    NgbModule
  ],
  providers: [Title],
  bootstrap: [AppComponent]
})
export class AppModule { }
