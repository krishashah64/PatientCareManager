import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { PatientsComponent } from './patients/patients.component';
import { AuthGuard } from './auth.guard';

export const appRoutes: Routes = [
    { 
      path: '', 
      redirectTo: '/login', 
      pathMatch: 'full' 
    },


    { path: 'login', component: LoginComponent }, // Login route

    { 
      path: 'patients', 
      component: PatientsComponent, 
      // canActivate: [AuthGuard],
      data: { roles: ['admin', 'healthcareprofessional'] }  
    },

  ];