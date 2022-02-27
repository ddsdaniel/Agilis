import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RedefinirMinhaSenhaComponent } from './redefinir-minha-senha.component';

describe('RedefinirMinhaSenhaComponent', () => {
  let component: RedefinirMinhaSenhaComponent;
  let fixture: ComponentFixture<RedefinirMinhaSenhaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RedefinirMinhaSenhaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RedefinirMinhaSenhaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
