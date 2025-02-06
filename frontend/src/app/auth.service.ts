import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators'; 

interface LoginResponse {
  token: string;
  user: { role: string };
}

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  private apiUrl = 'https://localhost:7151/auth/login'; // Adjust to your API URL
  private loggedInSubject = new BehaviorSubject<boolean>(this.isLoggedIn());
  private userRole: string | null = this.getRole(); // Store role in memory
  

  // Observable to track login state
  loggedIn$ = this.loggedInSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  login(email: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.apiUrl, { email, password }).pipe(
      map((response) => {
        if (response.token) {
          localStorage.setItem('token', response.token);

          // Decode token and store role securely
          const decodedToken = this.decodeToken(response.token);
          this.userRole = decodedToken?.role || null;
          if (this.userRole) {
            localStorage.setItem('role', response.user.role);
          }
          console.log(this.userRole);
          this.loggedInSubject.next(true);
        }
        return response;
      }),
      catchError((error) => {
        console.error('Login failed:', error);
        throw error;
      })
    );
  }

  logout() {
    this.http.post('https://localhost:7151/auth/logout', {}).subscribe(
      () => {
        localStorage.removeItem('token'); //clear token
        localStorage.removeItem('role');  // Clear role
        this.loggedInSubject.next(false); // Emit updated login state
        this.router.navigate(['/login']); // Redirect to login
      },
      (error) => {
        console.error('Logout failed:', error);
      }
    );
  }

  // Check if the user is authenticated by looking at local storage
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  getRole(): string | null {
    if (!this.userRole) {
      this.userRole = localStorage.getItem('role');
    }
    return this.userRole;
  }


  // Decode JWT token to get user info (including role)
  private decodeToken(token: string): any {
    try {
      const payload = token.split('.')[1];
      return JSON.parse(atob(payload)); // Decode base64 payload
    } catch (error) {
      console.error('Invalid token', error);
      return null;
    }
  }

   // Call this whenever the login state changes
  updateLoginState(isLoggedIn: boolean) {
    this.loggedInSubject.next(isLoggedIn);
  }
}
