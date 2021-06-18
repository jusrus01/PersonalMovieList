import { HttpClient } from '@angular/common/http';
import { ThrowStmt } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from '../movies/movie';
import { map } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { waitForAsync } from '@angular/core/testing';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  movies: Movie[];
  createdMovie : Movie;

  constructor(private http: HttpClient, private authService: AuthService,
      private router: Router) { 
    this.createdMovie = null;
  }

  fetchMoviesFromApi() : Observable<Movie[]> {
    return this.http.get<Movie[]>("http://localhost:5000/api/movies",
      { headers: { "Authorization" : "Bearer " + this.authService.getToken() }, withCredentials: false })
      .pipe(map((data: any[]) => data.map((item: Movie) =>
          new Movie(
            item.id,
            item.title,
            item.comment,
            item.rating
          )))
      );
  }

  createMovie(title: string, comment: string, rating: number) : Observable<Movie> {
    if(rating <= 0 || rating > 5) {
      rating = 1;
    }
    // this.createdMovie = new Movie(this.movies.length + 1, title, comment, rating);

    // this.http.post("http://localhost:5000/api/movies", { title, rating, comment },
    //   { headers: { "Authorization" : "Bearer " + this.authService.getToken() }, withCredentials: false  })
    //   .subscribe();

    return this.http.post("http://localhost:5000/api/movies", { Title: title, Rating: rating, Comment: comment },
      { headers: { "Authorization" : "Bearer " + this.authService.getToken() }, withCredentials: false  })
      .pipe(map((item: Movie) =>
          new Movie(
            item.id,
            item.title,
            item.comment,
            item.rating
          )));
  }

  setCreatedMovie(movie: Movie) : void {
    this.createdMovie = movie;
  }

  removeMovie(movie: Movie) : Observable<any> {
    return this.http.delete("http://localhost:5000/api/movies/" + movie.id,
      { headers: { "Authorization" : "Bearer " + this.authService.getToken() }, withCredentials: false });
  }

  updateMovie(movie: Movie) : void {
    const title = movie.title;
    const rating = movie.rating;
    const comment = movie.comment;

    this.http.put("http://localhost:5000/api/movies/" + movie.id, { title, rating, comment },
    { headers: { "Authorization" : "Bearer " + this.authService.getToken()}, withCredentials: false })
      .subscribe();
  }
}
