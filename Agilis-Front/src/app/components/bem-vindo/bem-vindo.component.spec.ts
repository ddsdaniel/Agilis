import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { BemVindoComponent } from './bem-vindo.component';

describe('BemVindoComponent', () => {
  let component: BemVindoComponent;
  let fixture: ComponentFixture<BemVindoComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ BemVindoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BemVindoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
