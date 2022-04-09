import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesFormComponent } from './times-form.component';

describe('TimesFormComponent', () => {
  let component: TimesFormComponent;
  let fixture: ComponentFixture<TimesFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimesFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
