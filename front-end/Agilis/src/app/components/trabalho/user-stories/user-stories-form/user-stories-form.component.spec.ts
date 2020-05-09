import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserStoriesFormComponent } from './user-stories-form.component';

describe('UserStoriesFormComponent', () => {
  let component: UserStoriesFormComponent;
  let fixture: ComponentFixture<UserStoriesFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserStoriesFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserStoriesFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
