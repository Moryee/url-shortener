import { TestBed } from '@angular/core/testing';

import { ShortUrlsService } from './short-urls.service';

describe('ShortUrlsService', () => {
  let service: ShortUrlsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShortUrlsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
