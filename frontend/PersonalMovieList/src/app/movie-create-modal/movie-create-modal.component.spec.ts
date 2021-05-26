import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieCreateModalComponent } from './movie-create-modal.component';

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
});
