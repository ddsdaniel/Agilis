import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlterarMinhaSenhaComponent } from './alterar-minha-senha.component';

describe('AlterarMinhaSenhaComponent', () => {
  let component: AlterarMinhaSenhaComponent;
  let fixture: ComponentFixture<AlterarMinhaSenhaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AlterarMinhaSenhaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AlterarMinhaSenhaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
