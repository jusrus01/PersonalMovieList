import {Component} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { MoviesService } from 'src/app/movies.service';

@Component({
  selector: 'movie-create-modal',
  templateUrl: './movie-create-modal.component.html'
})
export class MovieCreateModalComponent {
  
  closeResult = '';
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
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
  
  createMovie() : void {
    if(this.creationForm.valid) {
      var values = this.creationForm.value;
      this.moviesService.createMovie(values.title, values.comment, values.rating);
      this.creationForm.reset();
      this.modalService.dismissAll();
    }
  }
}