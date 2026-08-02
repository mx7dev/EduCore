import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeccionForm } from './seccion-form';

describe('SeccionForm', () => {
  let component: SeccionForm;
  let fixture: ComponentFixture<SeccionForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SeccionForm],
    }).compileComponents();

    fixture = TestBed.createComponent(SeccionForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
