import { Component, OnInit } from '@angular/core';
import { MoviesService } from '../movies.service';
import { Movie } from './movie';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})

export class MoviesComponent implements OnInit {

  movies: Movie[];
  selectedMovie? : Movie;

  constructor(private moviesService: MoviesService) {
  }

  ngOnInit(): void {
    this.moviesService.fetchMoviesFromApi()
      .subscribe(movies => this.movies = movies);
  }

  removeMovie(movie: Movie) : void {
    this.moviesService.removeMovie(movie);
  }

  selectMovie(movie: Movie) : void{
    if(this.selectedMovie == movie) {
      movie.comment = this.selectedMovie.comment;
      this.selectedMovie = null;
    } else {
      this.selectedMovie = movie;
    }
  }

}
