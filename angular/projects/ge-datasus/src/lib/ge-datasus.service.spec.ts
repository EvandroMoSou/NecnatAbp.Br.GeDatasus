import { TestBed } from '@angular/core/testing';
import { GeDatasusService } from './services/ge-datasus.service';
import { RestService } from '@abp/ng.core';

describe('GeDatasusService', () => {
  let service: GeDatasusService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(GeDatasusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
