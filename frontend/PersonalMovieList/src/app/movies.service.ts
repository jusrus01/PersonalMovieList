import { HttpClient } from '@angular/common/http';
import { ThrowStmt } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MOVIES } from './mock/mock-movies';
import { Movie } from './movies/movie';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  movies: Movie[];

  constructor(private http: HttpClient) { 
    this.movies = MOVIES;
  }

  fetchMovies() : Movie[] {
    return this.movies;
  }

  fetchMoviesFromApi() : Observable<Movie[]> {
    return this.http.get<Movie[]>("http://localhost:5000/api/movies", { withCredentials: true })
      .pipe(
        map((data: any[]) => data.map((item: Movie) =>
          new Movie(
            item.id,
            item.title,
            item.comment,
            item.rating
          )))
      );
  }

  createMovie(title: string, comment: string, rating: number) : void {
    if(rating <= 0 || rating > 5) {
      rating = 1;
    }
    this.movies.push({id: this.movies.length + 1, title: title, rating: rating, comment: comment});
  }

  removeMovie(movie: Movie) : void {
    const index = this.movies.indexOf(movie);
    if(index > -1) {
      this.movies.splice(index, 1);
    }
  }
}
