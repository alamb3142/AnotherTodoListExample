import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListsPageComponent } from './lists-page.component';

describe('ListsPageComponent', () => {
  let component: ListsPageComponent;
  let fixture: ComponentFixture<ListsPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ListsPageComponent]
    });
    fixture = TestBed.createComponent(ListsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
