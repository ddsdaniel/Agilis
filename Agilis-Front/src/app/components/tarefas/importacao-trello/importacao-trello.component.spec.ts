import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportacaoTrelloComponent } from './importacao-trello.component';

describe('ImportacaoTrelloComponent', () => {
  let component: ImportacaoTrelloComponent;
  let fixture: ComponentFixture<ImportacaoTrelloComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportacaoTrelloComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportacaoTrelloComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
