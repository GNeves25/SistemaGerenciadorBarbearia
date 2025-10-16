import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CommonModule } from '@angular/common';
import { of } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { ServiceListComponent } from './service-list.component';

describe('ServiceListComponent', () => {
  let component: ServiceListComponent;
  let fixture: ComponentFixture<ServiceListComponent>;
  let apiServiceSpy: jasmine.SpyObj<ApiService>;

  beforeEach(async () => {
    apiServiceSpy = jasmine.createSpyObj<ApiService>('ApiService', ['getServices']);
    apiServiceSpy.getServices.and.returnValue(of([]));

    await TestBed.configureTestingModule({
      imports: [CommonModule],
      declarations: [ServiceListComponent],
      providers: [{ provide: ApiService, useValue: apiServiceSpy }]
    }).compileComponents();

    fixture = TestBed.createComponent(ServiceListComponent);
    component = fixture.componentInstance;
  });

  it('should load services on init', () => {
    component.ngOnInit();
    expect(apiServiceSpy.getServices).toHaveBeenCalled();
  });
});
