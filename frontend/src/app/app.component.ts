import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Barbershop Manager';

  isAuthenticated$: Observable<boolean>;

  constructor(private readonly authService: AuthService) {
    this.isAuthenticated$ = this.authService.authState$;
  }

  logout(): void {
    this.authService.logout();
  }
}
