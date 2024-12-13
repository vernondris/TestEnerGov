import { TestBed } from '@angular/core/testing';

import { ManageEmployeeInformationService } from './manage-employee-information.service';

describe('ManageEmployeeInformationService', () => {
  let service: ManageEmployeeInformationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ManageEmployeeInformationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
