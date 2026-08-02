import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatriculaForm } from './matricula-form';

describe('MatriculaForm', () => {
  let component: MatriculaForm;
  let fixture: ComponentFixture<MatriculaForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MatriculaForm],
    }).compileComponents();

    fixture = TestBed.createComponent(MatriculaForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
