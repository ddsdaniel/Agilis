import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EpicosFormComponent } from './epicos-form.component';

describe('EpicosFormComponent', () => {
  let component: EpicosFormComponent;
  let fixture: ComponentFixture<EpicosFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EpicosFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EpicosFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
