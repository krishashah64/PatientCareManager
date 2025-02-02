import { Component, OnInit } from "@angular/core";
import { PatientsService } from "../patients.service";
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-patients',
  imports: [CommonModule, FormsModule], 
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})

export class PatientsComponent implements OnInit {
  patients: any[] = [];
  filteredPatients: any[] = [];
  searchQuery: string = '';
  isLoading: boolean = true;
  errorMessage: string | null = null;
  selectedPatient: any = null;
  paginatedPatients: any[] = [];
  
  // Pagination variables
  currentPage: number = 1;
  itemsPerPage: number = 5;
  totalPages: number = 0;

  // Sorting variables
  sortColumn: string = 'id';
  sortDirection: string = 'asc';

  constructor(private patientService: PatientsService) {}

  ngOnInit(): void {
    this.patientService.getPatients().subscribe({
      next: (data) => {
        this.patients = data.patients;
        this.filterPatients();
        this.isLoading = false;
        this.updatePagination();
      },
      error: (error) => {
        this.errorMessage = 'Error fetching patients. Please try again later.';
        this.isLoading = false;
      }
    });
  }

  // Search functionality
  onSearch() {
    this.currentPage = 1; // Reset to first page when performing a search
    this.filterPatients(); // Reapply the filter
  }

  // Sorting logic
  onSort(column: string) {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }

    this.filteredPatients.sort((a, b) => {
      const valueA = a[column];
      const valueB = b[column];

      if (valueA < valueB) return this.sortDirection === 'asc' ? -1 : 1;
      if (valueA > valueB) return this.sortDirection === 'asc' ? 1 : -1;
      return 0;
    });

    this.updatePagination();
  }

  // Update pagination based on filtered patients
  updatePagination() {
    this.totalPages = Math.ceil(this.filteredPatients.length / this.itemsPerPage);
    this.paginatedPatients = this.filteredPatients.slice(
      (this.currentPage - 1) * this.itemsPerPage,
      this.currentPage * this.itemsPerPage
    );
  }

  // Filter patients based on search query
  filterPatients(): void {
    const query = this.searchQuery.toLowerCase().trim();
    const filtered = this.patients.filter(patient =>
      patient.firstName.toLowerCase().includes(query) ||
      patient.lastName.toLowerCase().includes(query) ||
      patient.id.toString().includes(query)
    );

    this.filteredPatients = filtered;
    this.updatePagination(); // Reapply pagination after filtering
  }

  // Change page based on user input
  changePage(page: number) {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.updatePagination(); // Update patients list for the selected page
  }

  // Update recommendation status
  updateRecommendationStatus(patient: any, recommendationId: number, isCompleted: boolean) {
    this.patientService.updateRecommendationStatus(recommendationId, isCompleted)
      .subscribe({
        next: () => {
          console.log("Recommendation updated successfully");
          const recommendation = patient.recommendations.find((rec: any) => rec.id === recommendationId);
          if (recommendation) {
            recommendation.isCompleted = isCompleted;
          }
        },
        error: (err) => {
          console.error("Error updating recommendation", err);
        }
      });
  }

  selectPatient(patient: any): void {
    this.selectedPatient = patient;
    console.log('Selected Patient:', this.selectedPatient);
  }

  closeDetails(): void {
    this.selectedPatient = null;
  }
}
