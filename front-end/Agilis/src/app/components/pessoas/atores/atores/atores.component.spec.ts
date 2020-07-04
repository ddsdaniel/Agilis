import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AtoresComponent } from './atores.component';

describe('AtoresComponent', () => {
  let component: AtoresComponent;
  let fixture: ComponentFixture<AtoresComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AtoresComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AtoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
