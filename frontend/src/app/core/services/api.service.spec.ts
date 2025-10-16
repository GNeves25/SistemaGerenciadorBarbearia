import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { environment } from '../../../environments/environment';
import { ApiService } from './api.service';

describe('ApiService', () => {
  let service: ApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });

    service = TestBed.inject(ApiService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should call the appointments endpoint when creating an appointment', () => {
    const payload = {
      barberId: '1',
      serviceOfferingId: '1',
      customerName: 'John Doe',
      scheduledAt: '2024-01-01T10:00',
      durationMinutes: 60,
      notes: 'Teste'
    };

    service.createAppointment(payload).subscribe();
    const req = httpMock.expectOne(`${environment.apiUrl}/appointments`);
    expect(req.request.method).toBe('POST');
    req.flush({});
  });
});
