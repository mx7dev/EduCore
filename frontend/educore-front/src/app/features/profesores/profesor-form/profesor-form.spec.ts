import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfesorForm } from './profesor-form';

describe('ProfesorForm', () => {
  let component: ProfesorForm;
  let fixture: ComponentFixture<ProfesorForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfesorForm],
    }).compileComponents();

    fixture = TestBed.createComponent(ProfesorForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
