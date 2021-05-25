import { ComponentFixtureAutoDetect, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { Directive, HostListener, Input, NgModule } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { routes } from './app.module';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        AppComponent
      ],
    }).compileComponents();
  });

  it('should create the navbar', () => {
    const fixture = TestBed.createComponent(AppComponent);
    expect(fixture.nativeElement.querySelector('[data-test="navbar"]')).toBeTruthy();
  });

  it('navbar should have login button', () => {
    const fixture = TestBed.createComponent(AppComponent);
    expect(fixture.nativeElement.querySelector('[data-test="login-button"]')).toBeTruthy();
  });

  it('navbar should have logo', () => {
    const fixture = TestBed.createComponent(AppComponent);
    expect(fixture.nativeElement.querySelector('[data-test="logo"]')).toBeTruthy();
  });

  it('navbar should have register button', () => {
    const fixture = TestBed.createComponent(AppComponent);
    expect(fixture.nativeElement.querySelector('[data-test="register-button"]')).toBeTruthy();
  });

  it('navbar should have logout button', () => {
    const fixture = TestBed.createComponent(AppComponent);
    expect(fixture.nativeElement.querySelector('[data-test="logout-button"]')).toBeTruthy();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Personal Movie List - Home'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('Personal Movie List - Home');
  });

  // testing routing
  describe('AppComponentRoutingTesting', () => {

    let loc : Location;
    let router : Router;
    let fixture;

    beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [RouterTestingModule.withRoutes(routes)],
        declarations: [
          HomeComponent,
          RegisterComponent,
          LoginComponent
        ]
      });

      router = TestBed.get(Router);
      loc = TestBed.get(Location);
      fixture = TestBed.createComponent(AppComponent);

      router.initialNavigation();
    });

    it('navigate to "" redirects to /', fakeAsync(() => {
      router.navigate(['']);
      tick();
      expect(loc.path()).toBe('/');
    }))

    it(`navigate to "" redirects to / should have as title 'Personal Movie List - Home'`, fakeAsync(() => {
      router.navigate(['']);
      tick();
      const app = fixture.componentInstance;
      expect(app.title).toEqual('Personal Movie List - Home');
    }));

    it('navigate to "asddasd" redirects to /', fakeAsync(() => {
      router.navigate(['asddasd']);
      tick();
      expect(loc.path()).toBe('/asddasd');
    }))

    it(`navigate to "asddasd" redirects to / should have as title 'Personal Movie List - Home'`, fakeAsync(() => {
      router.navigate(['asddasd']);
      tick();
      const app = fixture.componentInstance;
      expect(app.title).toEqual('Personal Movie List - Home');
    }));

    it('navigate to "login" redirects to /login', fakeAsync(() => {
      router.navigate(['login']);
      tick();
      expect(loc.path()).toBe('/login');
    }))

    it(`navigate to "login" redirects to /login should have as title 'Personal Movie List - Home'`, fakeAsync(() => {
      router.navigate(['login']);
      tick();
      const app = fixture.componentInstance;
      expect(app.title).toEqual('Personal Movie List - Login');
    }));
    
    it('navigate to "register" redirects to /register', fakeAsync(() => {
      router.navigate(['register']);
      tick();
      expect(loc.path()).toBe('/register');
    }))

    it(`navigate to "register" redirects to /register should have as title 'Personal Movie List - Home'`, fakeAsync(() => {
      router.navigate(['register']);
      tick();
      const app = fixture.componentInstance;
      expect(app.title).toEqual('Personal Movie List - Register');
    }));
    

  })
});
