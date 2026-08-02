import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfesorList } from './profesor-list';

describe('ProfesorList', () => {
  let component: ProfesorList;
  let fixture: ComponentFixture<ProfesorList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfesorList],
    }).compileComponents();

    fixture = TestBed.createComponent(ProfesorList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
