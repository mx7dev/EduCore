import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeccionList } from './seccion-list';

describe('SeccionList', () => {
  let component: SeccionList;
  let fixture: ComponentFixture<SeccionList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SeccionList],
    }).compileComponents();

    fixture = TestBed.createComponent(SeccionList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
