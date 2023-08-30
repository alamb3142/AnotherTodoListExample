import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListOverviewComponent } from './list-overview.component';

describe('ListOverviewComponent', () => {
  let component: ListOverviewComponent;
  let fixture: ComponentFixture<ListOverviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ListOverviewComponent]
    });
    fixture = TestBed.createComponent(ListOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
