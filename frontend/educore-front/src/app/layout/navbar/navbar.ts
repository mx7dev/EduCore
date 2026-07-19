import { Component } from '@angular/core';
import { Button } from 'primeng/button';
import { Tooltip } from 'primeng/tooltip';

@Component({
  selector: 'app-navbar',
  imports: [Button, Tooltip],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar {}
