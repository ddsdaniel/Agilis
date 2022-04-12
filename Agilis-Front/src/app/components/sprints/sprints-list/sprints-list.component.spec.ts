import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SprintsListComponent } from './sprints-list.component';

describe('SprintsListComponent', () => {
  let component: SprintsListComponent;
  let fixture: ComponentFixture<SprintsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SprintsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SprintsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
