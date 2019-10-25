import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClientModule } from '@angular/common/http';
import { Injector } from '@angular/core';

import { StatProviderService } from './stat-provider.service';

describe('StatProviderService', () => {
  let injector: Injector;
  let httpMock: HttpTestingController;
  let statService: StatProviderService;
  beforeEach(() => 
  {
    injector = TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
        HttpClientTestingModule,
      ],
      providers: [
        StatProviderService,
      ],
    });
    httpMock = injector.get(HttpTestingController);
    statService = injector.get(StatProviderService);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should work', () => {
    // let response;
    // let url = 'http://localhost:5000/api/v1.0/stat/estimation-graph';

    // statService.getEstimationGraphData( 100, 50 )
    //   .subscribe(res => {
    //     response = res;
    //   });

    // const req = httpMock.expectOne(url);
    // req.flush(response);
  });
});
