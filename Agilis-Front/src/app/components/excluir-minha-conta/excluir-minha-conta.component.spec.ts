import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExcluirMinhaContaComponent } from './excluir-minha-conta.component';

describe('ExcluirMinhaContaComponent', () => {
  let component: ExcluirMinhaContaComponent;
  let fixture: ComponentFixture<ExcluirMinhaContaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExcluirMinhaContaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExcluirMinhaContaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
