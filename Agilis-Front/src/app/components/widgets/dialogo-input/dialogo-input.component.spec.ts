import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogoInputComponent } from './dialogo-input.component';

describe('DialogoInputComponent', () => {
  let component: DialogoInputComponent;
  let fixture: ComponentFixture<DialogoInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogoInputComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogoInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
