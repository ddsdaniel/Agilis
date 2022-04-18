import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeaturesFormComponent } from './features-form.component';

describe('FeaturesFormComponent', () => {
  let component: FeaturesFormComponent;
  let fixture: ComponentFixture<FeaturesFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FeaturesFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FeaturesFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
