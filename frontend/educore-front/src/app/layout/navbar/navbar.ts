import { Component,inject  } from '@angular/core';
import { Button } from 'primeng/button';
import { Tooltip } from 'primeng/tooltip';
import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-navbar',
  imports: [Button, Tooltip],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar {
    private authService = inject(AuthService);

  get userName() {
    return this.authService.currentUser();
  }

  logout() {
    this.authService.logout();
  }

}
