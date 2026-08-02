import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradoList } from './grado-list';

describe('GradoList', () => {
  let component: GradoList;
  let fixture: ComponentFixture<GradoList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GradoList],
    }).compileComponents();

    fixture = TestBed.createComponent(GradoList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
