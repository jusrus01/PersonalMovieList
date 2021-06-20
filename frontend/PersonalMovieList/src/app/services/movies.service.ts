import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from '../movies/movie';
import { map } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  private movies: Movie[];
  public createdMovie : Movie;

  constructor(private http: HttpClient, private authService: AuthService,
      private router: Router) { 
    this.createdMovie = null;
  }

  fetchMoviesFromApi() : Observable<Movie[]> {
    return this.http.get<Movie[]>("http://localhost:5000/api/movies")
      .pipe(map((data: any[]) => data.map((item: Movie) => {
          return new Movie(
            item.id,
            item.title,
            item.comment,
            item.rating,
            item.image
          )}))
      );
  }

  createMovie(title: string, comment: string, rating: number, image) : Observable<Movie> {
    if(rating <= 0 || rating > 5) {
      rating = 1;
    }

    return this.http.post("http://localhost:5000/api/movies", { Title: title, Rating: rating, Comment: comment, ImageBase64 : image })
      .pipe(map((item: Movie) =>
          new Movie(
            item.id,
            item.title,
            item.comment,
            item.rating,
            item.image
          )));
  }

  setCreatedMovie(movie: Movie) : void {
    this.createdMovie = movie;
  }

  removeMovie(movie: Movie) : Observable<any> {
    return this.http.delete("http://localhost:5000/api/movies/" + movie.id);
  }

  updateMovie(movie: Movie) : void {
    const title = movie.title;
    const rating = movie.rating;
    const comment = movie.comment;
    const imageBase64 = movie.image;

    this.http.put("http://localhost:5000/api/movies/" + movie.id, { title, rating, comment, imageBase64 })
      .subscribe();
  }
}
