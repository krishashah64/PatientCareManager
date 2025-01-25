import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router'; // Import RouterModule for routing support
import { FormsModule } from '@angular/forms'; 
import { AuthService } from '../auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    if (!this.username || !this.password) {
      this.errorMessage = 'Please enter both username and password';
      return;
    }

    // Call the login method in AuthService
    this.authService.login(this.username, this.password).subscribe(
      (response) => {
        // Store the JWT token in localStorage
        localStorage.setItem('token', response.token);
        this.authService.updateLoginState(true);
        this.router.navigate(['/patients']); // Redirect to the list of patients after successful login
      },
      (error) => {
        // Handle login error
        this.errorMessage = 'Invalid username or password';
      }
    );
  }
}
