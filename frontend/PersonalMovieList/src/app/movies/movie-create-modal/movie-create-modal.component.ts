import {Component} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MoviesService } from '../../services/movies.service';

@Component({
  selector: 'movie-create-modal',
  templateUrl: './movie-create-modal.component.html',
  styleUrls: [ './movie-create-modal.component.css' ]
})
export class MovieCreateModalComponent {
  
  selectedImage? : string;

  creationForm = this.formBuilder.group({
    title: ['', Validators.required],
    comment: '',
    rating: ['', Validators.required],
  });

  constructor(private modalService: NgbModal,
    private moviesService: MoviesService,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.selectedImage = null;
  }

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      }, (reason) => {
      });
  }

  createMovie() : void {
    if(this.creationForm.valid) {
      const values = this.creationForm.value;

      var imageWithoutPrefix : string;
      if(this.selectedImage == null) {
        imageWithoutPrefix = '';
      } else {
        imageWithoutPrefix = this.selectedImage.split(',', 2)[1];
      }

      this.moviesService.createMovie(values.title, values.comment, values.rating, imageWithoutPrefix)
        .subscribe(movie => this.moviesService.setCreatedMovie(movie));
        
      this.creationForm.reset();
      this.modalService.dismissAll();
    }
  }

  handleReaderLoaded(event) {
    const reader = event.target;
    this.selectedImage = reader.result;
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