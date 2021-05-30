import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieCreateModalComponent } from '../movie-create-modal/movie-create-modal.component';

describe('MovieCreateModalComponent', () => {
  let component: MovieCreateModalComponent;
  let fixture: ComponentFixture<MovieCreateModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MovieCreateModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MovieCreateModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  
  it('create modal component should have title field', () => {
    expect(fixture.nativeElement.querySelector('[data-test="create-title"]')).toBeTruthy();
  });
    
  it('create modal component should have image field', () => {
    expect(fixture.nativeElement.querySelector('[data-test="create-image"]')).toBeTruthy();
  });
    
  it('create modal component should have comment field', () => {
    expect(fixture.nativeElement.querySelector('[data-test="create-comment"]')).toBeTruthy();
  });
    
  it('create modal component should have rating field', () => {
    expect(fixture.nativeElement.querySelector('[data-test="create-rating"]')).toBeTruthy();
  });
    
  it('create modal component should have create button', () => {
    expect(fixture.nativeElement.querySelector('[data-test="create-button"]')).toBeTruthy();
  });

});
