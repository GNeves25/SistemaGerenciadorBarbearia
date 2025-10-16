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
  theme: 'light' | 'dark' = 'light';
  private readonly storageKey = 'app-theme';

  constructor(private readonly authService: AuthService) {
    this.isAuthenticated$ = this.authService.authState$;
    this.initializeTheme();
  }

  logout(): void {
    this.authService.logout();
  }

  toggleTheme(): void {
    this.theme = this.theme === 'light' ? 'dark' : 'light';
    this.applyTheme(this.theme);
    if (typeof window !== 'undefined') {
      window.localStorage.setItem(this.storageKey, this.theme);
    }
  }

  private initializeTheme(): void {
    if (typeof window !== 'undefined') {
      const stored = window.localStorage.getItem(this.storageKey);
      if (stored === 'light' || stored === 'dark') {
        this.theme = stored;
      }
    }

    this.applyTheme(this.theme);
  }

  private applyTheme(theme: 'light' | 'dark'): void {
    if (typeof document === 'undefined') {
      return;
    }

    const body = document.body;
    body.classList.remove('theme-light', 'theme-dark');
    body.classList.add(`theme-${theme}`);
  }
}
