import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';
import { Barber } from '../../shared/models/barber.model';

@Component({
  selector: 'app-barber-list',
  templateUrl: './barber-list.component.html',
  styleUrls: ['./barber-list.component.scss']
})
export class BarberListComponent implements OnInit {
  barbers$!: Observable<Barber[]>;

  constructor(private readonly apiService: ApiService) {}

  ngOnInit(): void {
    this.barbers$ = this.apiService.getBarbers();
  }
}
