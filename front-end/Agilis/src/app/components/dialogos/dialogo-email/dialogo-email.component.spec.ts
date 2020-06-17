import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogoEmailComponent } from './dialogo-email.component';

describe('DialogoEmailComponent', () => {
  let component: DialogoEmailComponent;
  let fixture: ComponentFixture<DialogoEmailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogoEmailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogoEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
