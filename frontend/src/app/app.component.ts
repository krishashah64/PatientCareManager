import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PatientsComponent } from './patients/patients.component';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterModule, CommonModule, PatientsComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'PatientCareManager';
  isLoggedIn: boolean = false;


  constructor(private authService: AuthService, private router: Router) {}

  // Method to check login status on component initialization
  ngOnInit() {
    this.authService.loggedIn$.subscribe((loggedIn) => {
      this.isLoggedIn = loggedIn; // Update the login state dynamically
    });
  }


  logout(): void {
    this.authService.logout(); // Call logout method from AuthService
    this.isLoggedIn = false; // Update the login status
    this.router.navigate(['/login']);
  }
}
