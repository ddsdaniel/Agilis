import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TarefasFormComponent } from './tarefas-form.component';

describe('TarefasFormComponent', () => {
  let component: TarefasFormComponent;
  let fixture: ComponentFixture<TarefasFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TarefasFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TarefasFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
