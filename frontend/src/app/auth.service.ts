// import { Injectable } from '@angular/core';

// @Injectable({
//   providedIn: 'root'
// })
// export class AuthService {

//   constructor() { }
// }

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  private apiUrl = 'https://localhost:7151/auth/login'; // Adjust to your API URL
  private loggedInSubject = new BehaviorSubject<boolean>(this.isLoggedIn());

  // Observable to track login state
  loggedIn$ = this.loggedInSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  login(username: string, password: string) {
    return this.http.post<{ token: string }>(`${this.apiUrl}`, { username, password });
  }

  // logout() {
  //   localStorage.removeItem('token');
  //   this.router.navigate(['/login']);
  // }

  logout() {
    this.http.post('https://localhost:7151/auth/logout', {}).subscribe(
      () => {
        localStorage.removeItem('token'); // Clear token
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

   // Call this whenever the login state changes
  updateLoginState(isLoggedIn: boolean) {
    this.loggedInSubject.next(isLoggedIn);
  }
}
