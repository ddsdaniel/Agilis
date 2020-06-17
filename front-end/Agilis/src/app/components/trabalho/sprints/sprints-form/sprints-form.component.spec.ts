import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SprintsFormComponent } from './sprints-form.component';

describe('SprintsFormComponent', () => {
  let component: SprintsFormComponent;
  let fixture: ComponentFixture<SprintsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SprintsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SprintsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
