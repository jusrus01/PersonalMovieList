import { ThrowStmt } from '@angular/compiler';
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

  removeMovie(movie: Movie) : void {
    const index = this.movies.indexOf(movie);
    if(index > -1) {
      this.movies.splice(index, 1);
    }
  }
}
