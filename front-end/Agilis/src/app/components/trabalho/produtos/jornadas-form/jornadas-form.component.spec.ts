import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JornadasFormComponent } from './jornadas-form.component';

describe('JornadasFormComponent', () => {
  let component: JornadasFormComponent;
  let fixture: ComponentFixture<JornadasFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JornadasFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JornadasFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
