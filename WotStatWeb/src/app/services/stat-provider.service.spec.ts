import { TestBed } from '@angular/core/testing';

import { StatProviderService } from './stat-provider.service';

describe('StatProviderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StatProviderService = TestBed.get(StatProviderService);
    expect(service).toBeTruthy();
  });
});
