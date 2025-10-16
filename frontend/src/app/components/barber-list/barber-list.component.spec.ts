import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CommonModule } from '@angular/common';
import { of } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { BarberListComponent } from './barber-list.component';

describe('BarberListComponent', () => {
  let component: BarberListComponent;
  let fixture: ComponentFixture<BarberListComponent>;
  let apiServiceSpy: jasmine.SpyObj<ApiService>;

  beforeEach(async () => {
    apiServiceSpy = jasmine.createSpyObj<ApiService>('ApiService', ['getBarbers']);
    apiServiceSpy.getBarbers.and.returnValue(of([]));

    await TestBed.configureTestingModule({
      imports: [CommonModule],
      declarations: [BarberListComponent],
      providers: [{ provide: ApiService, useValue: apiServiceSpy }]
    }).compileComponents();

    fixture = TestBed.createComponent(BarberListComponent);
    component = fixture.componentInstance;
  });

  it('should load barbers on init', () => {
    component.ngOnInit();
    expect(apiServiceSpy.getBarbers).toHaveBeenCalled();
  });
});
