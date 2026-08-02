import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatriculaList } from './matricula-list';

describe('MatriculaList', () => {
  let component: MatriculaList;
  let fixture: ComponentFixture<MatriculaList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MatriculaList],
    }).compileComponents();

    fixture = TestBed.createComponent(MatriculaList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
