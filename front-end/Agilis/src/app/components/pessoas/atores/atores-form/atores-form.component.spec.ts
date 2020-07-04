import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtoresFormComponent } from './atores-form.component';

describe('AtoresFormComponent', () => {
  let component: AtoresFormComponent;
  let fixture: ComponentFixture<AtoresFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtoresFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtoresFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
