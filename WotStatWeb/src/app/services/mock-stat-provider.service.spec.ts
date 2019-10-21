import { TestBed } from '@angular/core/testing';

import { MockStatProviderService } from './mock-stat-provider.service';

describe('MockStatProviderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MockStatProviderService = TestBed.get(MockStatProviderService);
    expect(service).toBeTruthy();
  });
});
