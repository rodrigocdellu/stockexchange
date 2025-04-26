import { TestBed } from '@angular/core/testing';

import { CdbserviceService } from './cdbservice.service';

describe('CdbserviceService', () => {
    let service: CdbserviceService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(CdbserviceService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
