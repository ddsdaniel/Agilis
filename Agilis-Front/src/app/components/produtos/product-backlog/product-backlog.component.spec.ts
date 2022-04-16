import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductBacklogComponent } from './product-backlog.component';

describe('ProductBacklogComponent', () => {
  let component: ProductBacklogComponent;
  let fixture: ComponentFixture<ProductBacklogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductBacklogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductBacklogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
