import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class PatientsService {
  private apiUrl = 'https://localhost:7151/patients'; // API URL
  constructor(private http: HttpClient) { }

  getPatients(): Observable<any> {
    return this.http.get<any[]>(this.apiUrl).pipe(
      // Log the response data in the service
      tap(data => console.log('Patients data fetched:', data))
    );
  }
}
