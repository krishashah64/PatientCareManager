import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms'; 
import { AuthService } from '../auth.service';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

interface LoginResponse {
  token: string;
}

@Component({
  selector: 'app-login',
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    if (!this.email || !this.password) {
      this.errorMessage = 'Please enter both username and password';
      return;
    }

    // Call the login method in AuthService
    this.authService.login(this.email, this.password).subscribe(
      (response: LoginResponse) => {
        // Store the JWT token in localStorage
        localStorage.setItem('token', response.token);
        this.authService.updateLoginState(true);
        this.router.navigate(['/patients']); // Redirect to the list of patients after successful login
      },
      (err: HttpErrorResponse) => {
        // Handle login error
        this.errorMessage = err.error?.message || 'Invalid email or password';
      }
    );
  }
}
