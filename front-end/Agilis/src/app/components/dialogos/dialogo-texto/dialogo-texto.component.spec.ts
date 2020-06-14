import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogoTextoComponent } from './dialogo-texto.component';

describe('DialogoTextoComponent', () => {
  let component: DialogoTextoComponent;
  let fixture: ComponentFixture<DialogoTextoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogoTextoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogoTextoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
