import { Component, OnInit } from '@angular/core';
import { MOVIES } from '../mock/mock-movies';
import { Movie } from './movie';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {

  constructor() { }

  movies: Movie[];

  ngOnInit(): void {

    this.movies = MOVIES;

  }

}