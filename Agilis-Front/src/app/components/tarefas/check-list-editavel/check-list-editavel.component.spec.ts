import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckListEditavelComponent } from './check-list-editavel.component';

describe('CheckListEditavelComponent', () => {
  let component: CheckListEditavelComponent;
  let fixture: ComponentFixture<CheckListEditavelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CheckListEditavelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckListEditavelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
