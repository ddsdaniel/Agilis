import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserStoriesHomeComponent } from './user-stories-home.component';

describe('UserStoriesHomeComponent', () => {
  let component: UserStoriesHomeComponent;
  let fixture: ComponentFixture<UserStoriesHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserStoriesHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserStoriesHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
