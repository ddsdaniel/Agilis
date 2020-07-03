import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EpicosComponent } from './epicos.component';

describe('EpicosComponent', () => {
  let component: EpicosComponent;
  let fixture: ComponentFixture<EpicosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EpicosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EpicosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
