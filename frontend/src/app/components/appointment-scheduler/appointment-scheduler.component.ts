import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService } from '../../core/services/api.service';
import { Appointment, CreateAppointmentRequest } from '../../shared/models/appointment.model';
import { Barber } from '../../shared/models/barber.model';
import { ServiceOffering } from '../../shared/models/service-offering.model';

@Component({
  selector: 'app-appointment-scheduler',
  templateUrl: './appointment-scheduler.component.html',
  styleUrls: ['./appointment-scheduler.component.scss']
})
export class AppointmentSchedulerComponent implements OnInit {
  barbers$!: Observable<Barber[]>;
  services$!: Observable<ServiceOffering[]>;
  appointments$!: Observable<Appointment[]>;
  upcomingAppointments$!: Observable<Appointment[]>;

  appointmentForm = this.fb.group({
    barberId: ['', Validators.required],
    serviceOfferingId: ['', Validators.required],
    customerName: ['', Validators.required],
    scheduledAt: ['', Validators.required],
    durationMinutes: [60, [Validators.required, Validators.min(15)]],
    notes: ['']
  });

  feedbackMessage = '';

  constructor(private readonly fb: FormBuilder, private readonly apiService: ApiService) {}

  ngOnInit(): void {
    this.barbers$ = this.apiService.getBarbers();
    this.services$ = this.apiService.getServices();
    this.appointments$ = this.apiService.getAppointments();
    this.upcomingAppointments$ = this.appointments$.pipe(
      map(items =>
        [...items]
          .sort((a, b) => new Date(a.scheduledAt).getTime() - new Date(b.scheduledAt).getTime())
          .slice(0, 5)
      )
    );
  }

  submit(): void {
    if (this.appointmentForm.invalid) {
      this.appointmentForm.markAllAsTouched();
      return;
    }

    const payload = this.appointmentForm.getRawValue() as CreateAppointmentRequest;
    this.apiService.createAppointment(payload).subscribe({
      next: () => {
        this.feedbackMessage = 'Agendamento criado com sucesso!';
        this.appointmentForm.reset({ durationMinutes: 60 });
        this.refreshAppointments();
      },
      error: () => {
        this.feedbackMessage = 'Não foi possível criar o agendamento. Tente novamente mais tarde.';
      }
    });
  }

  private refreshAppointments(): void {
    this.appointments$ = this.apiService.getAppointments();
    this.upcomingAppointments$ = this.appointments$.pipe(
      map(items =>
        [...items]
          .sort((a, b) => new Date(a.scheduledAt).getTime() - new Date(b.scheduledAt).getTime())
          .slice(0, 5)
      )
    );
  }
}
