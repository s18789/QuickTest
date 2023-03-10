import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamsResultsComponent } from './exams-results.component';

describe('ExamsResultsComponent', () => {
  let component: ExamsResultsComponent;
  let fixture: ComponentFixture<ExamsResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExamsResultsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExamsResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
