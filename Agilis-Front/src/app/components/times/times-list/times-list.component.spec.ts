import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesListComponent } from './times-list.component';

describe('TimesListComponent', () => {
  let component: TimesListComponent;
  let fixture: ComponentFixture<TimesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
