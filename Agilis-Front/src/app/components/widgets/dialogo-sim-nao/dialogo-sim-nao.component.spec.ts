import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogoSimNaoComponent } from './dialogo-sim-nao.component';

describe('DialogoSimNaoComponent', () => {
  let component: DialogoSimNaoComponent;
  let fixture: ComponentFixture<DialogoSimNaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogoSimNaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogoSimNaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
