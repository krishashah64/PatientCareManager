import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app/app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './app/auth.interceptor';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes), // Provide routing configuration
    provideAnimations(), // Enable animations
    provideHttpClient(), // Provide HTTP client
    {
      provide: HTTP_INTERCEPTORS, // Use the token HTTP_INTERCEPTORS
      useClass: AuthInterceptor, // Register your AuthInterceptor
      multi: true, // This ensures multiple interceptors can be added if needed
    },
  ],
}).catch((err) => console.error(err));

