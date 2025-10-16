export interface Appointment {
  id: string;
  barberId: string;
  serviceOfferingId: string;
  customerName: string;
  scheduledAt: string;
  durationMinutes: number;
  notes?: string;
}

export interface CreateAppointmentRequest {
  barberId: string;
  serviceOfferingId: string;
  customerName: string;
  scheduledAt: string;
  durationMinutes: number;
  notes?: string;
}
