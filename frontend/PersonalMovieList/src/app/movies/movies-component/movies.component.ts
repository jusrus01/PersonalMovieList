import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MoviesService } from '../../services/movies.service';
import { Movie } from '..//movie';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})

export class MoviesComponent implements OnInit {

  movies: Movie[];
  selectedMovie? : Movie;

  constructor(private moviesService: MoviesService,
    private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.moviesService.fetchMoviesFromApi()
      .subscribe(movies => { 
        this.movies = movies;
      });
  }

  ngDoCheck() {
    if(this.moviesService.createdMovie != null) {
      this.movies.push(this.moviesService.createdMovie);
      this.moviesService.createdMovie = null;
    }
  }

  getImageContent(movie: Movie) {
    // return this.sanitizer.bypassSecurityTrustUrl(movie.image);
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/png;base64, ' + movie.image);
  }

  updateMovie(movie: Movie) : void {
    this.moviesService.updateMovie(movie);
    this.selectMovie(movie);
  }

  removeMovie(movie: Movie) : void {
    const index = this.movies.indexOf(movie);
    if(index > -1) {
      this.movies.splice(index, 1);
    }
    this.moviesService.removeMovie(movie).subscribe(m => m);
  }

  selectMovie(movie: Movie) : void{
    if(this.selectedMovie == movie) {
      movie.comment = this.selectedMovie.comment;
      this.selectedMovie = null;
    } else {
      this.selectedMovie = movie;
    }
  }

  handleReaderLoaded(event) {
    const reader = event.target;
    console.log("BEFORE ", this.selectedMovie.image);
    this.selectedMovie.image = reader.result.split(',', 2)[1];
    console.log("AFTER ", this.selectedMovie.image);
  }
  
  onImageSelected(event) : void {
    const selectedImage = event.target.files[0];

    if(selectedImage) {

      const reader = new FileReader();
      reader.onload = this.handleReaderLoaded.bind(this);
      reader.readAsDataURL(selectedImage);
    }
  }

  
}
