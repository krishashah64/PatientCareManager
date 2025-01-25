import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
// import { ApiService } from '../api.service';


@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})


export class HomeComponent {
  constructor(private http: HttpClient, private router: Router) {}
  // message = 'This is a Standalone Component!';
  // apiMessage: string = "";

  // constructor(private apiService: ApiService) {}

  // ngOnInit(): void {
  //   this.apiService.getData().subscribe(
  //     (data) => {
  //       this.apiMessage = data.message;
  //     },
  //     (error) => {
  //       console.error('Error fetching data from the API', error);
  //     }
  //   );
  // }



}
