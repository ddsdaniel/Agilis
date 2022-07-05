import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReleasesListComponent } from './releases-list.component';

describe('ReleasesListComponent', () => {
  let component: ReleasesListComponent;
  let fixture: ComponentFixture<ReleasesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReleasesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReleasesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
