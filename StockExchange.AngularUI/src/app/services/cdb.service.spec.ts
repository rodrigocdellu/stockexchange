import { TestBed } from '@angular/core/testing';

import { CdbService } from './cdb.service';

describe('CdbserviceService', () => {
    let service: CdbService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(CdbService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
