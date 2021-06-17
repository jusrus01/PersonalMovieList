import {Component} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { MoviesService } from '../../services/movies.service';

@Component({
  selector: 'movie-create-modal',
  templateUrl: './movie-create-modal.component.html',
  styleUrls: [ './movie-create-modal.component.css' ]
})
export class MovieCreateModalComponent {
  
  creationForm = this.formBuilder.group({
    title: ['', Validators.required],
    image: '',
    comment: '',
    rating: ['', Validators.required]
  });

  constructor(private modalService: NgbModal,
    private moviesService: MoviesService,
    private formBuilder: FormBuilder) {}

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      }, (reason) => {
      });
  }

  createMovie() : void {
    if(this.creationForm.valid) {
      var values = this.creationForm.value;

      this.moviesService.createMovie(values.title, values.comment, values.rating)
        .subscribe(movie => this.moviesService.setCreatedMovie(movie));
        
      this.creationForm.reset();
      this.modalService.dismissAll();
    }
  }
}