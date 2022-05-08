import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpanEditavelComponent } from './span-editavel.component';

describe('SpanEditavelComponent', () => {
  let component: SpanEditavelComponent;
  let fixture: ComponentFixture<SpanEditavelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpanEditavelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SpanEditavelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
