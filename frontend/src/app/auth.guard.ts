import { Injectable } from '@angular/core';
import { CanActivate,ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    const userRole = this.authService.getRole(); // Get user role from AuthService
    const requiredRoles: string[] = route.data['roles']; // Get required roles from route

    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']); // Redirect if not logged in
      return false;
    }

     // If no roles are required, allow access
     if (!requiredRoles) {
      return true;
    }

    // If user doesn't have a valid role, deny access
    if (!requiredRoles.includes(userRole || '')) {
      this.router.navigate(['/unauthorized']); // Redirect to unauthorized page if no access
      return false;
    }

    return true; // Allow access if role matches
  }
}
