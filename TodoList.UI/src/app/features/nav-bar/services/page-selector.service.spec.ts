import { TestBed } from '@angular/core/testing';

import { PageSelectorService } from './page-selector.service';

describe('PageSelectorService', () => {
  let service: PageSelectorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PageSelectorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
