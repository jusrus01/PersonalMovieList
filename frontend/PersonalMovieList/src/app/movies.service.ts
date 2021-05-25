import { Injectable } from '@angular/core';
import { MOVIES } from './mock/mock-movies';
import { Movie } from './movies/movie';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  movies: Movie[];

  constructor() { 
    this.movies = MOVIES;
  }

  fetchMovies() : Movie[] {
    return this.movies;
  }
}
