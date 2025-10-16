import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { AppointmentSchedulerComponent } from './appointment-scheduler.component';

describe('AppointmentSchedulerComponent', () => {
  let component: AppointmentSchedulerComponent;
  let fixture: ComponentFixture<AppointmentSchedulerComponent>;
  let apiServiceSpy: jasmine.SpyObj<ApiService>;

  beforeEach(async () => {
    apiServiceSpy = jasmine.createSpyObj<ApiService>('ApiService', ['getBarbers', 'getServices', 'getAppointments', 'createAppointment']);
    apiServiceSpy.getBarbers.and.returnValue(of([]));
    apiServiceSpy.getServices.and.returnValue(of([]));
    apiServiceSpy.getAppointments.and.returnValue(of([]));
    apiServiceSpy.createAppointment.and.returnValue(of({} as any));

    await TestBed.configureTestingModule({
      imports: [CommonModule, ReactiveFormsModule],
      declarations: [AppointmentSchedulerComponent],
      providers: [{ provide: ApiService, useValue: apiServiceSpy }]
    }).compileComponents();

    fixture = TestBed.createComponent(AppointmentSchedulerComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
  });

  it('should not submit when form is invalid', () => {
    component.submit();
    expect(apiServiceSpy.createAppointment).not.toHaveBeenCalled();
  });

  it('should submit when form is valid', () => {
    component.appointmentForm.setValue({
      barberId: '1',
      serviceOfferingId: '1',
      customerName: 'John Doe',
      scheduledAt: '2024-01-01T10:00',
      durationMinutes: 60,
      notes: 'Teste'
    });

    component.submit();
    expect(apiServiceSpy.createAppointment).toHaveBeenCalled();
  });
});
