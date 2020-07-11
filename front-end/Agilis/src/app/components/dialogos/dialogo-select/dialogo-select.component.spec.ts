import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogoSelectComponent } from './dialogo-select.component';

describe('DialogoSelectComponent', () => {
  let component: DialogoSelectComponent;
  let fixture: ComponentFixture<DialogoSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogoSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogoSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
