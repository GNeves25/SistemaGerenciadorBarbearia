import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { ServiceOffering } from '../../shared/models/service-offering.model';

@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrls: ['./service-list.component.scss']
})
export class ServiceListComponent implements OnInit {
  services$!: Observable<ServiceOffering[]>;

  constructor(private readonly apiService: ApiService) {}

  ngOnInit(): void {
    this.services$ = this.apiService.getServices();
  }
}
