import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { PatientsService } from '../patients.service';

@Component({
  selector: 'app-patients',
  imports: [CommonModule, FormsModule],
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
// export class PatientsComponent {
//   patients = [
//     { id: 1, name: 'John Doe', age: 35, recommendations: 'Allergy Check', isComplete: false },
//     { id: 2, name: 'Jane Smith', age: 42, recommendations: 'Screening', isComplete: false },
//     { id: 3, name: 'Emily Johnson', age: 28, recommendations: 'Allergy Check', isComplete: false },
//     { id: 4, name: 'Michael Brown', age: 50, recommendations: 'Follow-up', isComplete: false   },
//   ];

//   searchQuery: string = '';
//   filteredPatients = [...this.patients]; // Start with the full list

//   onSearch() {
//     const query = this.searchQuery.toLowerCase().trim();
//     this.filteredPatients = this.patients.filter(
//       patient => 
//         patient.name.toLowerCase().includes(query) || 
//         patient.id.toString().includes(query)
//     );
//   }
// }

export class PatientsComponent implements OnInit {
  patients: any[] = [];

  constructor(private patientService: PatientsService) {}

  ngOnInit(): void {
    this.patientService.getPatients().subscribe(data => {
      this.patients = data;
    });
  }
}