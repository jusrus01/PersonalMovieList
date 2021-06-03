import { NgModule } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { MoviesComponent } from './movies/movies-component/movies.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MovieCreateModalComponent } from './movies/movie-create-modal/movie-create-modal.component';
import { AuthGuard } from './guards/auth.guard';
import { LoggedInGuard } from './guards/logged-in.guard';

export const routes: Routes = [
  { path: "login", component: LoginComponent, data: { title: 'Login'}, canActivate: [LoggedInGuard] },
  { path: "register", component: RegisterComponent, data: { title: 'Register'}, canActivate: [LoggedInGuard]},
  { path: '', component: HomeComponent, data: { title: 'Home'}, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
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
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    NgbModule
  ],
  providers: [
    Title,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
