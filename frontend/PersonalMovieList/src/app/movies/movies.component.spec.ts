import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MOVIES } from '../mock/mock-movies';
import { MoviesService } from '../movies.service';
import { Movie } from './movie';

import { MoviesComponent } from './movies.component';

describe('MoviesComponent', () => {
  let component: MoviesComponent;
  let fixture: ComponentFixture<MoviesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MoviesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoviesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('movies component should have title', () => {
    expect(fixture.nativeElement.querySelector('[data-test="movie-title"]')).toBeTruthy();
  });

  it('movies component should have comment', () => {
    expect(fixture.nativeElement.querySelector('[data-test="movie-comment"]')).toBeTruthy();
  });

  it('movies component should have rating', () => {
    expect(fixture.nativeElement.querySelector('[data-test="movie-rating"]')).toBeTruthy();
  });

  it('movies component should have remove button', () => {
    expect(fixture.nativeElement.querySelector('[data-test="movie-remove"]')).toBeTruthy();
  });

  it('movies component should have add button', () => {
    expect(fixture.nativeElement.querySelector('[data-test="movie-add"]')).toBeTruthy();
  });

  it('movies component should have edit button', () => {
    expect(fixture.nativeElement.querySelector('[data-test="movie-edit"]')).toBeTruthy();
  });

  it('movies component should have sort option', () => {
    expect(fixture.nativeElement.querySelector('[data-test="movie-sort"]')).toBeTruthy();
  });

  it('movies component should have image', () => {
    expect(fixture.nativeElement.querySelector('[data-test="movie-image"]')).toBeTruthy();
  });
});
