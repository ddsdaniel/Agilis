import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StoryMappingComponent } from './story-mapping.component';

describe('StoryMappingComponent', () => {
  let component: StoryMappingComponent;
  let fixture: ComponentFixture<StoryMappingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StoryMappingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StoryMappingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
