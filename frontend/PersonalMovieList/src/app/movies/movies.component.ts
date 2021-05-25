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

  constructor(private moviesService: MoviesService) {
    this.movies = this.moviesService.fetchMovies();
  }

  ngOnInit(): void {
  }

  removeMovie(movie: Movie) : void {
    // might need to remove from maiin array too
    this.moviesService.removeMovie(movie);
  }

}
