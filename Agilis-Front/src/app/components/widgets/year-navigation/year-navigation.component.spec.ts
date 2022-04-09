import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YearNavigationComponent } from './year-navigation.component';

describe('YearNavigationComponent', () => {
  let component: YearNavigationComponent;
  let fixture: ComponentFixture<YearNavigationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YearNavigationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(YearNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
