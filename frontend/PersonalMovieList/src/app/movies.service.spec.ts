import { TestBed } from '@angular/core/testing';
import { MOVIES } from './mock/mock-movies';

import { MoviesService } from './movies.service';
import { Movie } from './movies/movie';

describe('MoviesService', () => {
  let service: MoviesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MoviesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('deletion function', () => {

    let movies : Movie[];
    let service : MoviesService;
    
    beforeEach(() => {
      service = new MoviesService();
    });

    it('should remove movie from the array', () => {
      const movies = service.fetchMovies();
      const removedMovie = movies[0];
      service.removeMovie(removedMovie);
      expect(service.movies.includes(removedMovie)).toBeFalse();
    });
  })

});
